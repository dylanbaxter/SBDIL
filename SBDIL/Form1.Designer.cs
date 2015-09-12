namespace SBDIR
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.lblDevice = new System.Windows.Forms.Label();
            this.comboMicrophone = new System.Windows.Forms.ComboBox();
            this.cbToggleRecord = new System.Windows.Forms.CheckBox();
            this.btnPhoneCall = new System.Windows.Forms.Button();
            this.numericThreshold = new System.Windows.Forms.NumericUpDown();
            this.lblThreshold = new System.Windows.Forms.Label();
            this.textboxLog = new System.Windows.Forms.TextBox();
            this.lblPerpName = new System.Windows.Forms.Label();
            this.groupBoxPerp = new System.Windows.Forms.GroupBox();
            this.perpAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.perpName = new System.Windows.Forms.TextBox();
            this.lblDb = new System.Windows.Forms.Label();
            this.vuMeter = new KA.Audio.VuMeter();
            ((System.ComponentModel.ISupportInitialize)(this.numericThreshold)).BeginInit();
            this.groupBoxPerp.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevice.Location = new System.Drawing.Point(12, 98);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(66, 13);
            this.lblDevice.TabIndex = 0;
            this.lblDevice.Text = "Microphone:";
            // 
            // comboMicrophone
            // 
            this.comboMicrophone.FormattingEnabled = true;
            this.comboMicrophone.Location = new System.Drawing.Point(84, 94);
            this.comboMicrophone.Name = "comboMicrophone";
            this.comboMicrophone.Size = new System.Drawing.Size(284, 21);
            this.comboMicrophone.TabIndex = 1;
            // 
            // cbToggleRecord
            // 
            this.cbToggleRecord.Appearance = System.Windows.Forms.Appearance.Button;
            this.cbToggleRecord.AutoSize = true;
            this.cbToggleRecord.Image = global::SBDIR.Properties.Resources.button_record;
            this.cbToggleRecord.Location = new System.Drawing.Point(50, 160);
            this.cbToggleRecord.Name = "cbToggleRecord";
            this.cbToggleRecord.Size = new System.Drawing.Size(28, 28);
            this.cbToggleRecord.TabIndex = 3;
            this.cbToggleRecord.UseVisualStyleBackColor = true;
            this.cbToggleRecord.CheckedChanged += new System.EventHandler(this.cbToggleRecord_CheckedChanged);
            // 
            // btnPhoneCall
            // 
            this.btnPhoneCall.Location = new System.Drawing.Point(3, 406);
            this.btnPhoneCall.Name = "btnPhoneCall";
            this.btnPhoneCall.Size = new System.Drawing.Size(75, 23);
            this.btnPhoneCall.TabIndex = 5;
            this.btnPhoneCall.Text = "Call the fuzz!";
            this.btnPhoneCall.UseVisualStyleBackColor = true;
            // 
            // numericThreshold
            // 
            this.numericThreshold.Location = new System.Drawing.Point(83, 130);
            this.numericThreshold.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.numericThreshold.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericThreshold.Name = "numericThreshold";
            this.numericThreshold.Size = new System.Drawing.Size(46, 20);
            this.numericThreshold.TabIndex = 9;
            this.numericThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericThreshold.Value = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            // 
            // lblThreshold
            // 
            this.lblThreshold.AutoSize = true;
            this.lblThreshold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThreshold.Location = new System.Drawing.Point(20, 133);
            this.lblThreshold.Name = "lblThreshold";
            this.lblThreshold.Size = new System.Drawing.Size(57, 13);
            this.lblThreshold.TabIndex = 10;
            this.lblThreshold.Text = "Threshold:";
            // 
            // textboxLog
            // 
            this.textboxLog.Location = new System.Drawing.Point(84, 160);
            this.textboxLog.Multiline = true;
            this.textboxLog.Name = "textboxLog";
            this.textboxLog.Size = new System.Drawing.Size(528, 269);
            this.textboxLog.TabIndex = 11;
            // 
            // lblPerpName
            // 
            this.lblPerpName.AutoSize = true;
            this.lblPerpName.Location = new System.Drawing.Point(403, 30);
            this.lblPerpName.Name = "lblPerpName";
            this.lblPerpName.Size = new System.Drawing.Size(38, 13);
            this.lblPerpName.TabIndex = 12;
            this.lblPerpName.Text = "Name:";
            // 
            // groupBoxPerp
            // 
            this.groupBoxPerp.Controls.Add(this.perpAddress);
            this.groupBoxPerp.Controls.Add(this.label1);
            this.groupBoxPerp.Controls.Add(this.perpName);
            this.groupBoxPerp.Controls.Add(this.lblPerpName);
            this.groupBoxPerp.Location = new System.Drawing.Point(15, 12);
            this.groupBoxPerp.Name = "groupBoxPerp";
            this.groupBoxPerp.Size = new System.Drawing.Size(595, 63);
            this.groupBoxPerp.TabIndex = 13;
            this.groupBoxPerp.TabStop = false;
            this.groupBoxPerp.Text = "Perpetrator Info";
            // 
            // perpAddress
            // 
            this.perpAddress.Location = new System.Drawing.Point(69, 27);
            this.perpAddress.Name = "perpAddress";
            this.perpAddress.Size = new System.Drawing.Size(313, 20);
            this.perpAddress.TabIndex = 15;
            this.perpAddress.Text = "6106 S. Arbor Ln.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Address:";
            // 
            // perpName
            // 
            this.perpName.Location = new System.Drawing.Point(448, 27);
            this.perpName.Name = "perpName";
            this.perpName.Size = new System.Drawing.Size(131, 20);
            this.perpName.TabIndex = 13;
            this.perpName.Text = "Scott Martin";
            // 
            // lblDb
            // 
            this.lblDb.AutoSize = true;
            this.lblDb.Location = new System.Drawing.Point(135, 133);
            this.lblDb.Name = "lblDb";
            this.lblDb.Size = new System.Drawing.Size(20, 13);
            this.lblDb.TabIndex = 14;
            this.lblDb.Text = "dB";
            // 
            // vuMeter
            // 
            this.vuMeter.AnalogMeter = false;
            this.vuMeter.DialBackground = System.Drawing.Color.White;
            this.vuMeter.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter.Led1Count = 6;
            this.vuMeter.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter.Led2Count = 6;
            this.vuMeter.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter.Led3Count = 4;
            this.vuMeter.LedSize = new System.Drawing.Size(6, 14);
            this.vuMeter.LedSpace = 3;
            this.vuMeter.Level = 0;
            this.vuMeter.LevelMax = 100;
            this.vuMeter.Location = new System.Drawing.Point(161, 130);
            this.vuMeter.MeterScale = KA.Audio.MeterScale.Analog;
            this.vuMeter.Name = "vuMeter";
            this.vuMeter.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter.PeakHold = true;
            this.vuMeter.Peakms = 1000;
            this.vuMeter.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter.ShowDialOnly = false;
            this.vuMeter.ShowLedPeak = false;
            this.vuMeter.ShowTextInDial = false;
            this.vuMeter.Size = new System.Drawing.Size(147, 20);
            this.vuMeter.TabIndex = 15;
            this.vuMeter.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter.UseLedLight = false;
            this.vuMeter.VerticalBar = false;
            this.vuMeter.VuText = "VU";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.vuMeter);
            this.Controls.Add(this.lblDb);
            this.Controls.Add(this.groupBoxPerp);
            this.Controls.Add(this.textboxLog);
            this.Controls.Add(this.lblThreshold);
            this.Controls.Add(this.numericThreshold);
            this.Controls.Add(this.btnPhoneCall);
            this.Controls.Add(this.cbToggleRecord);
            this.Controls.Add(this.comboMicrophone);
            this.Controls.Add(this.lblDevice);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "Scott\'s Barking Dog Insanity Reporter";
            ((System.ComponentModel.ISupportInitialize)(this.numericThreshold)).EndInit();
            this.groupBoxPerp.ResumeLayout(false);
            this.groupBoxPerp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.ComboBox comboMicrophone;
        //private System.Windows.Forms.ProgressBar progressMicLevel;
        private System.Windows.Forms.CheckBox cbToggleRecord;
        private System.Windows.Forms.Button btnPhoneCall;
        private System.Windows.Forms.NumericUpDown numericThreshold;
        private System.Windows.Forms.Label lblThreshold;
        private System.Windows.Forms.TextBox textboxLog;
        private System.Windows.Forms.Label lblPerpName;
        private System.Windows.Forms.GroupBox groupBoxPerp;
        private System.Windows.Forms.TextBox perpAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox perpName;
        private System.Windows.Forms.Label lblDb;
        private KA.Audio.VuMeter vuMeter;
    }
}

