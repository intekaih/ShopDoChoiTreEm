namespace Project01
{
    partial class ucHome
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pHome = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pHome
            // 
            this.pHome.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pHome.Location = new System.Drawing.Point(0, 0);
            this.pHome.Name = "pHome";
            this.pHome.Size = new System.Drawing.Size(1920, 1032);
            this.pHome.TabIndex = 4;
            // 
            // ucHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pHome);
            this.Name = "ucHome";
            this.Size = new System.Drawing.Size(1920, 1032);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pHome;
    }
}
