using System;
using System.IO;
using System.Windows.Forms;

namespace SimpleRsaActivator.SerialKeyMaker
{
    public partial class TrialPurchaseWindow : Form
    {
        private readonly string _publicKeyPath;
        private readonly string _serialFileLocation;
        private readonly RSASerialKey _rsaSerialKey;
        private readonly string _machineKey;

        public TrialPurchaseWindow(string description, string publicKeyPath, string serialFile = null)
        {
            InitializeComponent();
            if (SerialKeyMaker.Activated.IsActivated)
            {
                this.Close();
            }

            _publicKeyPath = publicKeyPath ?? Environment.CurrentDirectory + "\\Public.key";
            _serialFileLocation = serialFile ?? Environment.CurrentDirectory + "\\serial.txt";
            _machineKey = MachineFingerPrint.Value();
            _rsaSerialKey = new RSASerialKey(_publicKeyPath, null);
            if (File.Exists(_serialFileLocation))
            {
                var serial = File.ReadAllText(_serialFileLocation);
                if (Verify(serial))
                {
                    SerialKeyMaker.Activated.IsActivated = true;
                    this.Close();
                    return;
                }
            }
            
            this.Description.Text = description;
            MachineKeyTextBox.Text = _machineKey;
            this.TopMost = true;
            this.Show();
      
        }

        private bool Verify(string code)
        {
            return _rsaSerialKey.VerifyData(_machineKey, code);
        }

        private void ActivateButton_Click(object sender, EventArgs e)
        {
            var serial = ActivationCodeTextbox.Text;
            if (Verify(serial))
            {
                File.WriteAllText(_serialFileLocation,serial );
                SerialKeyMaker.Activated.IsActivated = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("The activation code entered is not valid for this machine please try again", "Error Code Not Valid", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}