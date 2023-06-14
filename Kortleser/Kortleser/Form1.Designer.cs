namespace Kortleser
{
    partial class Kortleser_GUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.pb_Closed = new System.Windows.Forms.PictureBox();
            this.pb_Open = new System.Windows.Forms.PictureBox();
            this.pb_Alarm = new System.Windows.Forms.PictureBox();
            this.cbPorts = new System.Windows.Forms.ComboBox();
            this.tbPin = new System.Windows.Forms.TextBox();
            this.tbID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.bwCom = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerDoorOpeningTimer = new System.Windows.Forms.Timer(this.components);
            this.lbSimSim = new System.Windows.Forms.ListBox();
            this.lbSentral = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbResetAlarm = new System.Windows.Forms.CheckBox();
            this.pb_DoorState = new System.Windows.Forms.PictureBox();
            this.cbNode = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbFromSentral = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this._disconnectButton = new System.Windows.Forms.Button();
            this._connectButton = new System.Windows.Forms.Button();
            this._portTextBox = new System.Windows.Forms.TextBox();
            this._ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this._statusTextBox = new System.Windows.Forms.TextBox();
            this.cbAppID = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbSentralString = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Closed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Open)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Alarm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DoorState)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(151, 207);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(67, 48);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(225, 207);
            this.btnStop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(67, 48);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stopp";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // pb_Closed
            // 
            this.pb_Closed.InitialImage = global::Kortleser.Properties.Resources.LED_Tom_V2;
            this.pb_Closed.Location = new System.Drawing.Point(272, 39);
            this.pb_Closed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pb_Closed.Name = "pb_Closed";
            this.pb_Closed.Size = new System.Drawing.Size(33, 31);
            this.pb_Closed.TabIndex = 2;
            this.pb_Closed.TabStop = false;
            // 
            // pb_Open
            // 
            this.pb_Open.InitialImage = global::Kortleser.Properties.Resources.LED_Tom_V2;
            this.pb_Open.Location = new System.Drawing.Point(272, 77);
            this.pb_Open.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pb_Open.Name = "pb_Open";
            this.pb_Open.Size = new System.Drawing.Size(33, 31);
            this.pb_Open.TabIndex = 3;
            this.pb_Open.TabStop = false;
            // 
            // pb_Alarm
            // 
            this.pb_Alarm.InitialImage = global::Kortleser.Properties.Resources.LED_Tom_V2;
            this.pb_Alarm.Location = new System.Drawing.Point(272, 116);
            this.pb_Alarm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pb_Alarm.Name = "pb_Alarm";
            this.pb_Alarm.Size = new System.Drawing.Size(33, 31);
            this.pb_Alarm.TabIndex = 4;
            this.pb_Alarm.TabStop = false;
            // 
            // cbPorts
            // 
            this.cbPorts.FormattingEnabled = true;
            this.cbPorts.Location = new System.Drawing.Point(19, 213);
            this.cbPorts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbPorts.Name = "cbPorts";
            this.cbPorts.Size = new System.Drawing.Size(114, 28);
            this.cbPorts.TabIndex = 3;
            // 
            // tbPin
            // 
            this.tbPin.Enabled = false;
            this.tbPin.Location = new System.Drawing.Point(19, 116);
            this.tbPin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbPin.MaxLength = 4;
            this.tbPin.Name = "tbPin";
            this.tbPin.PasswordChar = '*';
            this.tbPin.Size = new System.Drawing.Size(114, 27);
            this.tbPin.TabIndex = 2;
            // 
            // tbID
            // 
            this.tbID.Enabled = false;
            this.tbID.Location = new System.Drawing.Point(19, 44);
            this.tbID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbID.MaxLength = 4;
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(114, 27);
            this.tbID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Låst:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "Alarm:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(207, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Åpen:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Kortnummer:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "PIN Kode:";
            // 
            // bwCom
            // 
            this.bwCom.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCom_DoWork);
            this.bwCom.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCom_RunWorkerCompleted);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerDoorOpeningTimer
            // 
            this.timerDoorOpeningTimer.Interval = 1000;
            this.timerDoorOpeningTimer.Tick += new System.EventHandler(this.timerDoorOpeningTimer_Tick);
            // 
            // lbSimSim
            // 
            this.lbSimSim.FormattingEnabled = true;
            this.lbSimSim.ItemHeight = 20;
            this.lbSimSim.Location = new System.Drawing.Point(19, 307);
            this.lbSimSim.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbSimSim.Name = "lbSimSim";
            this.lbSimSim.Size = new System.Drawing.Size(468, 464);
            this.lbSimSim.TabIndex = 9;
            // 
            // lbSentral
            // 
            this.lbSentral.FormattingEnabled = true;
            this.lbSentral.ItemHeight = 20;
            this.lbSentral.Location = new System.Drawing.Point(510, 347);
            this.lbSentral.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbSentral.Name = "lbSentral";
            this.lbSentral.Size = new System.Drawing.Size(459, 164);
            this.lbSentral.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Fra SimSim:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(510, 271);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "Til Sentral:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(357, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 20);
            this.label9.TabIndex = 10;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(809, 525);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(175, 24);
            this.checkBox1.TabIndex = 11;
            this.checkBox1.Text = "Kort godkjent(debug)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(189, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 20);
            this.label10.TabIndex = 8;
            this.label10.Text = "Dørstatus:";
            // 
            // cbResetAlarm
            // 
            this.cbResetAlarm.AutoSize = true;
            this.cbResetAlarm.Location = new System.Drawing.Point(312, 121);
            this.cbResetAlarm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbResetAlarm.Name = "cbResetAlarm";
            this.cbResetAlarm.Size = new System.Drawing.Size(111, 24);
            this.cbResetAlarm.TabIndex = 12;
            this.cbResetAlarm.Text = "Reset Alarm";
            this.cbResetAlarm.UseVisualStyleBackColor = true;
            // 
            // pb_DoorState
            // 
            this.pb_DoorState.InitialImage = global::Kortleser.Properties.Resources.Door_Closed;
            this.pb_DoorState.Location = new System.Drawing.Point(449, 19);
            this.pb_DoorState.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pb_DoorState.Name = "pb_DoorState";
            this.pb_DoorState.Size = new System.Drawing.Size(155, 141);
            this.pb_DoorState.TabIndex = 13;
            this.pb_DoorState.TabStop = false;
            // 
            // cbNode
            // 
            this.cbNode.FormattingEnabled = true;
            this.cbNode.Location = new System.Drawing.Point(449, 216);
            this.cbNode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbNode.Name = "cbNode";
            this.cbNode.Size = new System.Drawing.Size(114, 28);
            this.cbNode.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(449, 189);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Dørnummer:";
            // 
            // lbFromSentral
            // 
            this.lbFromSentral.FormattingEnabled = true;
            this.lbFromSentral.ItemHeight = 20;
            this.lbFromSentral.Location = new System.Drawing.Point(510, 567);
            this.lbFromSentral.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbFromSentral.Name = "lbFromSentral";
            this.lbFromSentral.Size = new System.Drawing.Size(459, 204);
            this.lbFromSentral.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(510, 531);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 20);
            this.label11.TabIndex = 10;
            this.label11.Text = "Fra Sentral:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(19, 189);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 20);
            this.label12.TabIndex = 8;
            this.label12.Text = "COM-Port:";
            // 
            // _disconnectButton
            // 
            this._disconnectButton.Location = new System.Drawing.Point(831, 91);
            this._disconnectButton.Name = "_disconnectButton";
            this._disconnectButton.Size = new System.Drawing.Size(138, 29);
            this._disconnectButton.TabIndex = 16;
            this._disconnectButton.Text = "Koble Fra Sentral";
            this._disconnectButton.UseVisualStyleBackColor = true;
            this._disconnectButton.Click += new System.EventHandler(this._disconnectButton_Click);
            // 
            // _connectButton
            // 
            this._connectButton.Location = new System.Drawing.Point(682, 91);
            this._connectButton.Name = "_connectButton";
            this._connectButton.Size = new System.Drawing.Size(138, 29);
            this._connectButton.TabIndex = 17;
            this._connectButton.Text = "Koble til Sentral";
            this._connectButton.UseVisualStyleBackColor = true;
            this._connectButton.Click += new System.EventHandler(this._connectButton_Click);
            // 
            // _portTextBox
            // 
            this._portTextBox.Location = new System.Drawing.Point(831, 52);
            this._portTextBox.Name = "_portTextBox";
            this._portTextBox.Size = new System.Drawing.Size(138, 27);
            this._portTextBox.TabIndex = 5;
            this._portTextBox.Text = "9050";
            // 
            // _ipAddressTextBox
            // 
            this._ipAddressTextBox.Location = new System.Drawing.Point(831, 11);
            this._ipAddressTextBox.Name = "_ipAddressTextBox";
            this._ipAddressTextBox.Size = new System.Drawing.Size(138, 27);
            this._ipAddressTextBox.TabIndex = 4;
            this._ipAddressTextBox.Text = "127.0.0.1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(741, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 20);
            this.label13.TabIndex = 8;
            this.label13.Text = "IP Adresse:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(782, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(38, 20);
            this.label14.TabIndex = 8;
            this.label14.Text = "Port:";
            // 
            // _statusTextBox
            // 
            this._statusTextBox.Location = new System.Drawing.Point(623, 133);
            this._statusTextBox.Name = "_statusTextBox";
            this._statusTextBox.Size = new System.Drawing.Size(346, 27);
            this._statusTextBox.TabIndex = 20;
            // 
            // cbAppID
            // 
            this.cbAppID.FormattingEnabled = true;
            this.cbAppID.Location = new System.Drawing.Point(831, 217);
            this.cbAppID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbAppID.Name = "cbAppID";
            this.cbAppID.Size = new System.Drawing.Size(138, 28);
            this.cbAppID.TabIndex = 7;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(831, 189);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(90, 20);
            this.label15.TabIndex = 8;
            this.label15.Text = "Kortleser ID:";
            // 
            // tbSentralString
            // 
            this.tbSentralString.Location = new System.Drawing.Point(510, 310);
            this.tbSentralString.Name = "tbSentralString";
            this.tbSentralString.ReadOnly = true;
            this.tbSentralString.Size = new System.Drawing.Size(459, 27);
            this.tbSentralString.TabIndex = 21;
            // 
            // Kortleser_GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 792);
            this.Controls.Add(this.tbSentralString);
            this.Controls.Add(this.cbAppID);
            this.Controls.Add(this._statusTextBox);
            this.Controls.Add(this._ipAddressTextBox);
            this.Controls.Add(this._portTextBox);
            this.Controls.Add(this._connectButton);
            this.Controls.Add(this._disconnectButton);
            this.Controls.Add(this.lbFromSentral);
            this.Controls.Add(this.cbNode);
            this.Controls.Add(this.pb_DoorState);
            this.Controls.Add(this.cbResetAlarm);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbSentral);
            this.Controls.Add(this.lbSimSim);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.tbPin);
            this.Controls.Add(this.cbPorts);
            this.Controls.Add(this.pb_Alarm);
            this.Controls.Add(this.pb_Open);
            this.Controls.Add(this.pb_Closed);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Kortleser_GUI";
            this.Text = "Kortleser";
            this.Load += new System.EventHandler(this.Kortleser_GUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Closed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Open)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Alarm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DoorState)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnStart;
        private Button btnStop;
        private PictureBox pb_Closed;
        private PictureBox pb_Open;
        private PictureBox pb_Alarm;
        private ComboBox cbPorts;
        private TextBox tbPin;
        private TextBox tbID;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private System.ComponentModel.BackgroundWorker bwCom;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerDoorOpeningTimer;
        private ListBox lbSimSim;
        private ListBox lbSentral;
        private Label label7;
        private Label label8;
        private Label label9;
        private CheckBox checkBox1;
        private Label label10;
        private CheckBox cbResetAlarm;
        private PictureBox pb_DoorState;
        private ComboBox cbNode;
        private Label label6;
        private ListBox lbFromSentral;
        private Label label11;
        private Label label12;
        private Button _disconnectButton;
        private Button _connectButton;
        private TextBox _portTextBox;
        private TextBox _ipAddressTextBox;
        private Label label13;
        private Label label14;
        private TextBox _statusTextBox;
        private ComboBox cbAppID;
        private Label label15;
        private TextBox tbSentralString;
    }
}