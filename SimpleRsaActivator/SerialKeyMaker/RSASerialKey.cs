using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SimpleRsaActivator.SerialKeyMaker
{
    class RSASerialKey
    {
         string _publicKeyPath; 
         string _privateKeyPath; 

         public RSASerialKey(string pubKeyPath, string priKeyPath)
         {
             _privateKeyPath = priKeyPath;
             _publicKeyPath = pubKeyPath;
         }
         
        public void MakeKey()
        {
            //lets take a new CSP with a new 2048 bit rsa key pair
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);

            //how to get the private key
            RSAParameters privKey = csp.ExportParameters(true);

            //and the public key ...
            RSAParameters pubKey = csp.ExportParameters(false);
            //converting the public key into a string representation
            string pubKeyString;
            {
                //we need some buffer
                var sw = new StringWriter();
                //we need a serializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //serialize the key into the stream
                xs.Serialize(sw, pubKey);
                //get the string from the stream
                pubKeyString = sw.ToString();
                File.WriteAllText(_publicKeyPath, pubKeyString);
            }
            string privKeyString;
            {
                //we need some buffer
                var sw = new StringWriter();
                //we need a serializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //serialize the key into the stream
                xs.Serialize(sw, privKey);
                //get the string from the stream
                privKeyString = sw.ToString();
                File.WriteAllText(_privateKeyPath, privKeyString);
            }
        }
        
        public string EncryptText(string text)
        {
            //converting the public key into a string representation
            string pubKeyString;
            {
                using (StreamReader reader = new StreamReader(_publicKeyPath))
                {
                    pubKeyString = reader.ReadToEnd();
                }
            }
            //get a stream from the string
            var sr = new StringReader(pubKeyString);

            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));


            //get the object back from the stream
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters((RSAParameters) xs.Deserialize(sr));

            byte[] bytesPlainTextData = Encoding.ASCII.GetBytes(text);

            //apply pkcs#1.5 padding and encrypt our data 
            var bytesCipherText = csp.Encrypt(bytesPlainTextData, false);
            //we might want a string representation of our cypher text... base64 will do
            string encryptedText = Convert.ToBase64String(bytesCipherText);
            return encryptedText;
        }
        public string DecryptText(string text)
        {
            //we want to decrypt, therefore we need a csp and load our private key
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            string privKeyString;
            {
                privKeyString = File.ReadAllText(_privateKeyPath);
                //get a stream from the string
                var sr = new StringReader(privKeyString);
                //we need a deserializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //get the object back from the stream
                RSAParameters privKey = (RSAParameters) xs.Deserialize(sr);
                csp.ImportParameters(privKey);
            }
            byte[] bytesCipherText = Convert.FromBase64String(text);

            //decrypt and strip pkcs#1.5 padding
            byte[] bytesPlainTextData = csp.Decrypt(bytesCipherText, false);


            return Encoding.ASCII.GetString(bytesPlainTextData, 0, bytesPlainTextData.Length);
        }
        public string SignData(string message)
        {
            var privKeyString = File.ReadAllText(_privateKeyPath);
            //get a stream from the string
            var sr = new StringReader(privKeyString);
            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //get the object back from the stream

            return SignData(message, (RSAParameters) xs.Deserialize(sr));
        }
        public string SignData(string message, RSAParameters privateKey)
        {
            //// The array to store the signed message in bytes
            byte[] signedBytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                //// Write the message to a byte array using UTF8 as the encoding.
                var encoder = new UTF8Encoding();
                byte[] originalData = encoder.GetBytes(message);

                try
                {
                    //// Import the private key used for signing the message
                    rsa.ImportParameters(privateKey);

                    //// Sign the data, using SHA512 as the hashing algorithm 
                    signedBytes = rsa.SignData(originalData, CryptoConfig.MapNameToOID("SHA512"));
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
                finally
                {
                    //// Set the keycontainer to be cleared when rsa is garbage collected.
                    rsa.PersistKeyInCsp = false;
                }
            }

            //// Convert the a base64 string before returning
            return Convert.ToBase64String(signedBytes);
        }
        public bool VerifyData(string message, string signedMessage)
        {
            //converting the public key into a string representation
            string pubKeyString;
            {
                using (StreamReader reader = new StreamReader(_publicKeyPath))
                {
                    pubKeyString = reader.ReadToEnd();
                }
            }
            //get a stream from the string
            var sr = new StringReader(pubKeyString);

            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

            return VerifyData(message, signedMessage, (RSAParameters) xs.Deserialize(sr));
        }
        public bool VerifyData(string originalMessage, string signedMessage, RSAParameters publicKey)
        {
            bool success = false;
            using (var rsa = new RSACryptoServiceProvider())
            {
                var encoder = new UTF8Encoding();
                byte[] bytesToVerify = encoder.GetBytes(originalMessage);

                byte[] signedBytes = Convert.FromBase64String(signedMessage);
                try
                {
                    rsa.ImportParameters(publicKey);

                    SHA512Managed Hash = new SHA512Managed();

                    byte[] hashedData = Hash.ComputeHash(signedBytes);

                    success = rsa.VerifyData(bytesToVerify, CryptoConfig.MapNameToOID("SHA512"), signedBytes);
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }

            return success;
        }

    }
}