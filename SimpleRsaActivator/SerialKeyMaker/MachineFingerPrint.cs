using System;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace SimpleRsaActivator.SerialKeyMaker
{

    public class MachineFingerPrint
    {
        private static string fingerPrint = string.Empty;

        public static string Value(string uniqueText)
        {
            if (string.IsNullOrEmpty(fingerPrint))
            {
                // Could go more complex but i think cpu with motherboard details and machine name is enough also if any of these change one can argue its a new machine
                // where as some one adding a network card would change a mac address but could be argued the old key should still work
                var uniqueString = $"{uniqueText} Machine Name:{Environment.MachineName}{Environment.NewLine}CPU >> {cpuId()}{Environment.NewLine}BASE >> {baseId()}{Environment.NewLine}";
                fingerPrint = GetHash(uniqueString);
            }

            return fingerPrint;
        }

        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }

        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int) b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char) (n2 - 10 + (int) 'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char) (n1 - 10 + (int) 'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }

            return s;
        }

        #region Original Device ID Getting Code
        
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            var mc = new ManagementClass(wmiClass);
            var moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }

            return result;
        }

        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }

                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }

            return retVal;
        }
        

        //Motherboard ID
        private static string baseId()
        {
            return
                $"{identifier("Win32_BaseBoard", "Model")} {identifier("Win32_BaseBoard", "Manufacturer")} {identifier("Win32_BaseBoard", "Name")} {identifier("Win32_BaseBoard", "SerialNumber")}";
        }
        

        #endregion
    }
}