namespace SysDBTools.UI
{
    partial class Main
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
            this.tfLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // tfLog
            // 
            this.tfLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tfLog.Font = new System.Drawing.Font("Tahoma", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tfLog.Location = new System.Drawing.Point(0, 0);
            this.tfLog.Name = "tfLog";
            this.tfLog.ReadOnly = true;
            this.tfLog.Size = new System.Drawing.Size(380, 203);
            this.tfLog.TabIndex = 0;
            this.tfLog.Text = "";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 203);
            this.ControlBox = false;
            this.Controls.Add(this.tfLog);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DBTools - Sistema de backup - SysNorte Tecnologia";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox tfLog;
    }
}