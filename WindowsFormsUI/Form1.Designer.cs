namespace WindowsFormsUI
{
    partial class Form1
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
            this.addEmployeeButton = new System.Windows.Forms.Button();
            this.pendingTransactions = new System.Windows.Forms.ListBox();
            this.runTransactions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addEmployeeButton
            // 
            this.addEmployeeButton.Location = new System.Drawing.Point(65, 44);
            this.addEmployeeButton.Name = "addEmployeeButton";
            this.addEmployeeButton.Size = new System.Drawing.Size(106, 23);
            this.addEmployeeButton.TabIndex = 0;
            this.addEmployeeButton.Text = "Add Employee";
            this.addEmployeeButton.UseVisualStyleBackColor = true;
            // 
            // pendingTransactions
            // 
            this.pendingTransactions.FormattingEnabled = true;
            this.pendingTransactions.Location = new System.Drawing.Point(65, 82);
            this.pendingTransactions.Name = "pendingTransactions";
            this.pendingTransactions.Size = new System.Drawing.Size(327, 56);
            this.pendingTransactions.TabIndex = 1;
            // 
            // runTransactions
            // 
            this.runTransactions.Location = new System.Drawing.Point(65, 147);
            this.runTransactions.Name = "runTransactions";
            this.runTransactions.Size = new System.Drawing.Size(131, 23);
            this.runTransactions.TabIndex = 2;
            this.runTransactions.Text = "Run Transactions";
            this.runTransactions.UseVisualStyleBackColor = true;
            this.runTransactions.Click += new System.EventHandler(this.runTransactions_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(531, 310);
            this.Controls.Add(this.runTransactions);
            this.Controls.Add(this.pendingTransactions);
            this.Controls.Add(this.addEmployeeButton);
            this.Location = new System.Drawing.Point(15, 15);
            this.Name = "Form1";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button addEmployeeButton;

        private System.Windows.Forms.ListBox pendingTransactions;

        private System.Windows.Forms.Button runTransactions;

        private System.Windows.Forms.ListBox listBox1;

        private System.Windows.Forms.Button button1;

        private System.Windows.Forms.Label label1;

        #endregion
    }
}