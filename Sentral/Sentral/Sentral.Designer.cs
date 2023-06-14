namespace Sentral
{
    partial class SentralGUI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.framsideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_ChangeDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Stop = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label_PortNumber = new System.Windows.Forms.Label();
            this.textBox_PortNumber = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Alarms = new System.Windows.Forms.RichTextBox();
            this.label_Alarms = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Status = new System.Windows.Forms.TextBox();
            this.label_Debug = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.framsideToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1048, 30);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // framsideToolStripMenuItem
            // 
            this.framsideToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.button_ChangeDatabase});
            this.framsideToolStripMenuItem.Name = "framsideToolStripMenuItem";
            this.framsideToolStripMenuItem.Size = new System.Drawing.Size(59, 24);
            this.framsideToolStripMenuItem.Text = "Meny";
            // 
            // button_ChangeDatabase
            // 
            this.button_ChangeDatabase.Name = "button_ChangeDatabase";
            this.button_ChangeDatabase.Size = new System.Drawing.Size(202, 26);
            this.button_ChangeDatabase.Text = "Endre Brukarinfo";
            this.button_ChangeDatabase.Click += new System.EventHandler(this.button_ChangeDatabase_Click);
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(3, 283);
            this.button_Start.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(173, 31);
            this.button_Start.TabIndex = 2;
            this.button_Start.Text = "Start Server";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Stop
            // 
            this.button_Stop.Location = new System.Drawing.Point(182, 283);
            this.button_Stop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button_Stop.Name = "button_Stop";
            this.button_Stop.Size = new System.Drawing.Size(173, 31);
            this.button_Stop.TabIndex = 3;
            this.button_Stop.Text = "Stop Server";
            this.button_Stop.UseVisualStyleBackColor = true;
            this.button_Stop.Click += new System.EventHandler(this.button_Stop_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.64088F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.35912F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 36);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1034, 671);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.button_Start, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.button_Stop, 1, 3);
            this.tableLayoutPanel3.Controls.Add(this.label_PortNumber, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.textBox_PortNumber, 1, 2);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(671, 339);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(359, 328);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // label_PortNumber
            // 
            this.label_PortNumber.AutoSize = true;
            this.label_PortNumber.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_PortNumber.Location = new System.Drawing.Point(83, 232);
            this.label_PortNumber.Name = "label_PortNumber";
            this.label_PortNumber.Size = new System.Drawing.Size(93, 47);
            this.label_PortNumber.TabIndex = 4;
            this.label_PortNumber.Text = "Port Number";
            this.label_PortNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_PortNumber
            // 
            this.textBox_PortNumber.Location = new System.Drawing.Point(182, 236);
            this.textBox_PortNumber.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_PortNumber.Name = "textBox_PortNumber";
            this.textBox_PortNumber.Size = new System.Drawing.Size(172, 27);
            this.textBox_PortNumber.TabIndex = 5;
            this.textBox_PortNumber.Text = "9050";
            this.textBox_PortNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 98.18457F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.815431F));
            this.tableLayoutPanel4.Controls.Add(this.textBox_Alarms, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label_Alarms, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.76596F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.23404F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(661, 329);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // textBox_Alarms
            // 
            this.textBox_Alarms.Location = new System.Drawing.Point(3, 45);
            this.textBox_Alarms.Name = "textBox_Alarms";
            this.textBox_Alarms.ReadOnly = true;
            this.textBox_Alarms.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.textBox_Alarms.Size = new System.Drawing.Size(643, 281);
            this.textBox_Alarms.TabIndex = 0;
            this.textBox_Alarms.Text = "";
            // 
            // label_Alarms
            // 
            this.label_Alarms.AutoSize = true;
            this.label_Alarms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Alarms.Location = new System.Drawing.Point(3, 0);
            this.label_Alarms.Name = "label_Alarms";
            this.label_Alarms.Size = new System.Drawing.Size(643, 42);
            this.label_Alarms.TabIndex = 1;
            this.label_Alarms.Text = "Alarms";
            this.label_Alarms.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 98.33585F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.664145F));
            this.tableLayoutPanel5.Controls.Add(this.textBox_Status, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label_Debug, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 338);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.0303F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.9697F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(661, 330);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // textBox_Status
            // 
            this.textBox_Status.Location = new System.Drawing.Point(3, 46);
            this.textBox_Status.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox_Status.Multiline = true;
            this.textBox_Status.Name = "textBox_Status";
            this.textBox_Status.ReadOnly = true;
            this.textBox_Status.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Status.Size = new System.Drawing.Size(644, 279);
            this.textBox_Status.TabIndex = 2;
            // 
            // label_Debug
            // 
            this.label_Debug.AutoSize = true;
            this.label_Debug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_Debug.Location = new System.Drawing.Point(3, 0);
            this.label_Debug.Name = "label_Debug";
            this.label_Debug.Size = new System.Drawing.Size(644, 42);
            this.label_Debug.TabIndex = 3;
            this.label_Debug.Text = "Debug Window";
            this.label_Debug.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SentralGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(1048, 707);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SentralGUI";
            this.Text = "Sentral_GUI";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem framsideToolStripMenuItem;
        private ToolStripMenuItem button_ChangeDatabase;
        private Button button_Start;
        private Button button_Stop;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel3;
        private TextBox textBox_Status;
        private Label label_PortNumber;
        private TextBox textBox_PortNumber;
        private TableLayoutPanel tableLayoutPanel4;
        private RichTextBox textBox_Alarms;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label_Alarms;
        private Label label_Debug;
    }
}