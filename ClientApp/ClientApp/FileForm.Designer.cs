namespace ClientApp
{
    partial class FileForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewFileName = new System.Windows.Forms.TextBox();
            this.txtNewFilePath = new System.Windows.Forms.TextBox();
            this.btnConfirmNewFileDetails = new System.Windows.Forms.Button();
            this.btnCancelNewFileDetails = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "File path:";
            // 
            // txtNewFileName
            // 
            this.txtNewFileName.Location = new System.Drawing.Point(73, 68);
            this.txtNewFileName.Name = "txtNewFileName";
            this.txtNewFileName.Size = new System.Drawing.Size(199, 20);
            this.txtNewFileName.TabIndex = 2;
            // 
            // txtNewFilePath
            // 
            this.txtNewFilePath.Location = new System.Drawing.Point(73, 110);
            this.txtNewFilePath.Name = "txtNewFilePath";
            this.txtNewFilePath.Size = new System.Drawing.Size(199, 20);
            this.txtNewFilePath.TabIndex = 3;
            // 
            // btnConfirmNewFileDetails
            // 
            this.btnConfirmNewFileDetails.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnConfirmNewFileDetails.Location = new System.Drawing.Point(73, 167);
            this.btnConfirmNewFileDetails.Name = "btnConfirmNewFileDetails";
            this.btnConfirmNewFileDetails.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmNewFileDetails.TabIndex = 4;
            this.btnConfirmNewFileDetails.Text = "Ok";
            this.btnConfirmNewFileDetails.UseVisualStyleBackColor = true;
            this.btnConfirmNewFileDetails.Click += new System.EventHandler(this.btnConfirmNewFileDetails_Click);
            // 
            // btnCancelNewFileDetails
            // 
            this.btnCancelNewFileDetails.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelNewFileDetails.Location = new System.Drawing.Point(197, 167);
            this.btnCancelNewFileDetails.Name = "btnCancelNewFileDetails";
            this.btnCancelNewFileDetails.Size = new System.Drawing.Size(75, 23);
            this.btnCancelNewFileDetails.TabIndex = 5;
            this.btnCancelNewFileDetails.Text = "Cancel";
            this.btnCancelNewFileDetails.UseVisualStyleBackColor = true;
            // 
            // FileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnCancelNewFileDetails);
            this.Controls.Add(this.btnConfirmNewFileDetails);
            this.Controls.Add(this.txtNewFilePath);
            this.Controls.Add(this.txtNewFileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FileForm";
            this.Text = "FileForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewFileName;
        private System.Windows.Forms.TextBox txtNewFilePath;
        private System.Windows.Forms.Button btnConfirmNewFileDetails;
        private System.Windows.Forms.Button btnCancelNewFileDetails;
    }
}