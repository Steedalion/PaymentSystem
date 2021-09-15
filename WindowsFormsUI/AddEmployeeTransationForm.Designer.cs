using System.ComponentModel;

namespace WindowsFormsUI
{
    partial class AddEmployeeTransationForm
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
            this.submitButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.hourlyRadioButton = new System.Windows.Forms.RadioButton();
            this.salariedRadioButton = new System.Windows.Forms.RadioButton();
            this.commisionedRadioButton = new System.Windows.Forms.RadioButton();
            this.HourlyRateTextBox = new System.Windows.Forms.TextBox();
            this.SalariedSalaryTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.CommisionedSalaryBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.CommsionedRateBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.empIDTb = new System.Windows.Forms.TextBox();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(367, 231);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 1;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(275, 231);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // hourlyRadioButton
            // 
            this.hourlyRadioButton.Location = new System.Drawing.Point(275, 22);
            this.hourlyRadioButton.Name = "hourlyRadioButton";
            this.hourlyRadioButton.Size = new System.Drawing.Size(104, 24);
            this.hourlyRadioButton.TabIndex = 3;
            this.hourlyRadioButton.TabStop = true;
            this.hourlyRadioButton.Text = "Hourly";
            this.hourlyRadioButton.UseVisualStyleBackColor = true;
            this.hourlyRadioButton.CheckedChanged += new System.EventHandler(this.hourlyRadioButton_CheckedChanged);
            // 
            // salariedRadioButton
            // 
            this.salariedRadioButton.Location = new System.Drawing.Point(275, 80);
            this.salariedRadioButton.Name = "salariedRadioButton";
            this.salariedRadioButton.Size = new System.Drawing.Size(76, 24);
            this.salariedRadioButton.TabIndex = 3;
            this.salariedRadioButton.TabStop = true;
            this.salariedRadioButton.Text = "Salaried";
            this.salariedRadioButton.UseVisualStyleBackColor = true;
            this.salariedRadioButton.CheckedChanged += new System.EventHandler(this.salariedRadioButton_CheckedChanged);
            // 
            // commisionedRadioButton
            // 
            this.commisionedRadioButton.Location = new System.Drawing.Point(275, 135);
            this.commisionedRadioButton.Name = "commisionedRadioButton";
            this.commisionedRadioButton.Size = new System.Drawing.Size(88, 24);
            this.commisionedRadioButton.TabIndex = 3;
            this.commisionedRadioButton.TabStop = true;
            this.commisionedRadioButton.Text = "Commisioned";
            this.commisionedRadioButton.UseVisualStyleBackColor = true;
            this.commisionedRadioButton.CheckedChanged += new System.EventHandler(this.commisionedRadioButton_CheckedChanged);
            // 
            // HourlyRateTextBox
            // 
            this.HourlyRateTextBox.Location = new System.Drawing.Point(363, 26);
            this.HourlyRateTextBox.Name = "HourlyRateTextBox";
            this.HourlyRateTextBox.Size = new System.Drawing.Size(79, 20);
            this.HourlyRateTextBox.TabIndex = 1;
            this.HourlyRateTextBox.TextChanged += new System.EventHandler(this.HourlyRateTextBox_TextChanged);
            // 
            // SalariedSalaryTextBox
            // 
            this.SalariedSalaryTextBox.Location = new System.Drawing.Point(363, 80);
            this.SalariedSalaryTextBox.Name = "SalariedSalaryTextBox";
            this.SalariedSalaryTextBox.Size = new System.Drawing.Size(79, 20);
            this.SalariedSalaryTextBox.TabIndex = 1;
            this.SalariedSalaryTextBox.TextChanged += new System.EventHandler(this.SalariedSalaryTextBox_TextChanged);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(363, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Rate";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(363, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "Salary";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(363, 181);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "Salary";
            // 
            // CommisionedSalaryBox
            // 
            this.CommisionedSalaryBox.Location = new System.Drawing.Point(363, 192);
            this.CommisionedSalaryBox.Name = "CommisionedSalaryBox";
            this.CommisionedSalaryBox.Size = new System.Drawing.Size(79, 20);
            this.CommisionedSalaryBox.TabIndex = 1;
            this.CommisionedSalaryBox.TextChanged += new System.EventHandler(this.CommisionedSalaryBox_TextChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(363, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 23);
            this.label7.TabIndex = 0;
            this.label7.Text = "Rate";
            // 
            // CommsionedRateBox
            // 
            this.CommsionedRateBox.Location = new System.Drawing.Point(363, 149);
            this.CommsionedRateBox.Name = "CommsionedRateBox";
            this.CommsionedRateBox.Size = new System.Drawing.Size(79, 20);
            this.CommsionedRateBox.TabIndex = 1;
            this.CommsionedRateBox.TextChanged += new System.EventHandler(this.CommsionedRateBox_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "EmpId";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Name";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(20, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Address";
            // 
            // empIDTb
            // 
            this.empIDTb.Location = new System.Drawing.Point(121, 16);
            this.empIDTb.Name = "empIDTb";
            this.empIDTb.Size = new System.Drawing.Size(116, 20);
            this.empIDTb.TabIndex = 1;
            this.empIDTb.TextChanged += new System.EventHandler(this.empIdUpdated);
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(121, 54);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(116, 20);
            this.nameBox.TabIndex = 1;
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(121, 91);
            this.addressBox.Multiline = true;
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(116, 73);
            this.addressBox.TabIndex = 1;
            this.addressBox.TextChanged += new System.EventHandler(this.addressBox_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.addressBox);
            this.panel1.Controls.Add(this.nameBox);
            this.panel1.Controls.Add(this.empIDTb);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 192);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // AddEmployeeTransationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(460, 272);
            this.Controls.Add(this.commisionedRadioButton);
            this.Controls.Add(this.salariedRadioButton);
            this.Controls.Add(this.CommisionedSalaryBox);
            this.Controls.Add(this.SalariedSalaryTextBox);
            this.Controls.Add(this.CommsionedRateBox);
            this.Controls.Add(this.HourlyRateTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hourlyRadioButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "AddEmployeeTransationForm";
            this.Load += new System.EventHandler(this.AddEmployeeTransationForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox CommisionedSalaryBox;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox CommsionedRateBox;

        private System.Windows.Forms.Label label5;

        private System.Windows.Forms.Label label4;

        public System.Windows.Forms.TextBox SalariedSalaryTextBox;

        public System.Windows.Forms.TextBox HourlyRateTextBox;

        public System.Windows.Forms.RadioButton salariedRadioButton;
        public System.Windows.Forms.RadioButton commisionedRadioButton;

        public System.Windows.Forms.RadioButton hourlyRadioButton;

        public System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button cancelButton;

        public System.Windows.Forms.TextBox empIDTb;

        public System.Windows.Forms.TextBox nameBox;
        public System.Windows.Forms.TextBox addressBox;

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;

        #endregion
    }
}