namespace F3D2F3DEX
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ConvertALLDLs_Button = new System.Windows.Forms.Button();
            this.SaveROM_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CustomDL_Button = new System.Windows.Forms.Button();
            this.TxtFileTextBox = new System.Windows.Forms.TextBox();
            this.OpenTXT_Button = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(79, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(97, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "108A10";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(135, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OpenROM";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OpenROM_Button_Click);
            // 
            // ConvertALLDLs_Button
            // 
            this.ConvertALLDLs_Button.Location = new System.Drawing.Point(62, 48);
            this.ConvertALLDLs_Button.Name = "ConvertALLDLs_Button";
            this.ConvertALLDLs_Button.Size = new System.Drawing.Size(125, 23);
            this.ConvertALLDLs_Button.TabIndex = 2;
            this.ConvertALLDLs_Button.Text = "Convert ALL DLs";
            this.ConvertALLDLs_Button.UseVisualStyleBackColor = true;
            this.ConvertALLDLs_Button.Click += new System.EventHandler(this.ConvertALLDLs_Button_Click);
            // 
            // SaveROM_Button
            // 
            this.SaveROM_Button.Location = new System.Drawing.Point(88, 154);
            this.SaveROM_Button.Name = "SaveROM_Button";
            this.SaveROM_Button.Size = new System.Drawing.Size(75, 23);
            this.SaveROM_Button.TabIndex = 3;
            this.SaveROM_Button.Text = "Save ROM";
            this.SaveROM_Button.UseVisualStyleBackColor = true;
            this.SaveROM_Button.Click += new System.EventHandler(this.SaveROM_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Level Script Starting Addr";
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(8, 24);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(122, 20);
            this.textBox4.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(-3, 50);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(244, 101);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.ConvertALLDLs_Button);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(236, 75);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CustomDL_Button);
            this.tabPage2.Controls.Add(this.TxtFileTextBox);
            this.tabPage2.Controls.Add(this.OpenTXT_Button);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(236, 75);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "GeoLayouts/DLs";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CustomDL_Button
            // 
            this.CustomDL_Button.Location = new System.Drawing.Point(64, 38);
            this.CustomDL_Button.Margin = new System.Windows.Forms.Padding(2);
            this.CustomDL_Button.Name = "CustomDL_Button";
            this.CustomDL_Button.Size = new System.Drawing.Size(119, 34);
            this.CustomDL_Button.TabIndex = 13;
            this.CustomDL_Button.Text = "Convert GeoLayouts and DLs";
            this.CustomDL_Button.UseVisualStyleBackColor = true;
            this.CustomDL_Button.Click += new System.EventHandler(this.CustomDL_Button_Click);
            // 
            // TxtFileTextBox
            // 
            this.TxtFileTextBox.Enabled = false;
            this.TxtFileTextBox.Location = new System.Drawing.Point(19, 14);
            this.TxtFileTextBox.Name = "TxtFileTextBox";
            this.TxtFileTextBox.Size = new System.Drawing.Size(122, 20);
            this.TxtFileTextBox.TabIndex = 12;
            // 
            // OpenTXT_Button
            // 
            this.OpenTXT_Button.Location = new System.Drawing.Point(146, 11);
            this.OpenTXT_Button.Name = "OpenTXT_Button";
            this.OpenTXT_Button.Size = new System.Drawing.Size(75, 23);
            this.OpenTXT_Button.TabIndex = 11;
            this.OpenTXT_Button.Text = "Open TXT";
            this.OpenTXT_Button.UseVisualStyleBackColor = true;
            this.OpenTXT_Button.Click += new System.EventHandler(this.OpenTXT_Button_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 184);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.SaveROM_Button);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "F3D2F3DEX";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ConvertALLDLs_Button;
        private System.Windows.Forms.Button SaveROM_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button CustomDL_Button;
        private System.Windows.Forms.TextBox TxtFileTextBox;
        private System.Windows.Forms.Button OpenTXT_Button;
    }
}

