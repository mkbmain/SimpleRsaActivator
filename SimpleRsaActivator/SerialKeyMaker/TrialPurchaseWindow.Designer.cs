using System.ComponentModel;

namespace SimpleRsaActivator.SerialKeyMaker
{
    partial class TrialPurchaseWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Description = new System.Windows.Forms.Label();
            this.MachineKeyLabel = new System.Windows.Forms.Label();
            this.MachineKeyTextBox = new System.Windows.Forms.TextBox();
            this.ActivationCodeLabel = new System.Windows.Forms.Label();
            this.ActivationCodeTextbox = new System.Windows.Forms.TextBox();
            this.ActivateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Description
            // 
            this.Description.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Description.Location = new System.Drawing.Point(12, 9);
            this.Description.Name = "Description";
            this.Description.Size = new System.Drawing.Size(420, 150);
            this.Description.TabIndex = 0;
            // 
            // MachineKeyLabel
            // 
            this.MachineKeyLabel.Location = new System.Drawing.Point(12, 176);
            this.MachineKeyLabel.Name = "MachineKeyLabel";
            this.MachineKeyLabel.Size = new System.Drawing.Size(100, 17);
            this.MachineKeyLabel.TabIndex = 1;
            this.MachineKeyLabel.Text = "Machine Key:";
            this.MachineKeyLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // MachineKeyTextBox
            // 
            this.MachineKeyTextBox.Enabled = false;
            this.MachineKeyTextBox.Location = new System.Drawing.Point(112, 173);
            this.MachineKeyTextBox.Name = "MachineKeyTextBox";
            this.MachineKeyTextBox.Size = new System.Drawing.Size(318, 20);
            this.MachineKeyTextBox.TabIndex = 2;
            // 
            // ActivationCodeLabel
            // 
            this.ActivationCodeLabel.Location = new System.Drawing.Point(12, 220);
            this.ActivationCodeLabel.Name = "ActivationCodeLabel";
            this.ActivationCodeLabel.Size = new System.Drawing.Size(100, 18);
            this.ActivationCodeLabel.TabIndex = 3;
            this.ActivationCodeLabel.Text = "Activation Code:";
            this.ActivationCodeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ActivationCodeTextbox
            // 
            this.ActivationCodeTextbox.Location = new System.Drawing.Point(114, 221);
            this.ActivationCodeTextbox.Multiline = true;
            this.ActivationCodeTextbox.Name = "ActivationCodeTextbox";
            this.ActivationCodeTextbox.Size = new System.Drawing.Size(317, 71);
            this.ActivationCodeTextbox.TabIndex = 4;
            // 
            // ActivateButton
            // 
            this.ActivateButton.Location = new System.Drawing.Point(354, 298);
            this.ActivateButton.Name = "ActivateButton";
            this.ActivateButton.Size = new System.Drawing.Size(78, 27);
            this.ActivateButton.TabIndex = 5;
            this.ActivateButton.Text = "Activate";
            this.ActivateButton.UseVisualStyleBackColor = true;
            this.ActivateButton.Click += new System.EventHandler(this.ActivateButton_Click);
            // 
            // TrialPurchaseWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 331);
            this.Controls.Add(this.ActivateButton);
            this.Controls.Add(this.ActivationCodeTextbox);
            this.Controls.Add(this.ActivationCodeLabel);
            this.Controls.Add(this.MachineKeyTextBox);
            this.Controls.Add(this.MachineKeyLabel);
            this.Controls.Add(this.Description);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TrialPurchaseWindow";
            this.Text = "BackUpArchiver Trial";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button ActivateButton;

        private System.Windows.Forms.TextBox ActivationCodeTextbox;

        private System.Windows.Forms.Label ActivationCodeLabel;

        private System.Windows.Forms.Label MachineKeyLabel;
        private System.Windows.Forms.TextBox MachineKeyTextBox;

        private System.Windows.Forms.Label Description;

        #endregion
    }
}