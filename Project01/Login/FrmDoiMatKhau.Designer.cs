namespace Project01.Login
{
    partial class FrmDoiMatKhau
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
            this.MatKhauCu = new System.Windows.Forms.Label();
            this.MatKhauMoi = new System.Windows.Forms.Label();
            this.XacNhanLaiMatKhau = new System.Windows.Forms.Label();
            this.txt_MatKhauCu = new System.Windows.Forms.TextBox();
            this.txt_MatKhauMoi = new System.Windows.Forms.TextBox();
            this.txt_XacNhanLaiMatKhau = new System.Windows.Forms.TextBox();
            this.btn_ThayDoi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MatKhauCu
            // 
            this.MatKhauCu.AutoSize = true;
            this.MatKhauCu.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MatKhauCu.Location = new System.Drawing.Point(161, 69);
            this.MatKhauCu.Name = "MatKhauCu";
            this.MatKhauCu.Size = new System.Drawing.Size(171, 26);
            this.MatKhauCu.TabIndex = 0;
            this.MatKhauCu.Text = "MẬT KHẨU CŨ:";
            this.MatKhauCu.Click += new System.EventHandler(this.MatKhauCu_Click);
            // 
            // MatKhauMoi
            // 
            this.MatKhauMoi.AutoSize = true;
            this.MatKhauMoi.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MatKhauMoi.Location = new System.Drawing.Point(161, 131);
            this.MatKhauMoi.Name = "MatKhauMoi";
            this.MatKhauMoi.Size = new System.Drawing.Size(185, 26);
            this.MatKhauMoi.TabIndex = 0;
            this.MatKhauMoi.Text = "MẬT KHẨU MỚI:";
            this.MatKhauMoi.Click += new System.EventHandler(this.MatKhauCu_Click);
            // 
            // XacNhanLaiMatKhau
            // 
            this.XacNhanLaiMatKhau.AutoSize = true;
            this.XacNhanLaiMatKhau.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.XacNhanLaiMatKhau.Location = new System.Drawing.Point(57, 197);
            this.XacNhanLaiMatKhau.Name = "XacNhanLaiMatKhau";
            this.XacNhanLaiMatKhau.Size = new System.Drawing.Size(301, 26);
            this.XacNhanLaiMatKhau.TabIndex = 0;
            this.XacNhanLaiMatKhau.Text = "XÁC NHẬN LẠI MẬT KHẨU:";
            this.XacNhanLaiMatKhau.Click += new System.EventHandler(this.MatKhauCu_Click);
            // 
            // txt_MatKhauCu
            // 
            this.txt_MatKhauCu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MatKhauCu.Location = new System.Drawing.Point(399, 69);
            this.txt_MatKhauCu.Name = "txt_MatKhauCu";
            this.txt_MatKhauCu.Size = new System.Drawing.Size(265, 30);
            this.txt_MatKhauCu.TabIndex = 1;
            // 
            // txt_MatKhauMoi
            // 
            this.txt_MatKhauMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MatKhauMoi.Location = new System.Drawing.Point(399, 135);
            this.txt_MatKhauMoi.Name = "txt_MatKhauMoi";
            this.txt_MatKhauMoi.Size = new System.Drawing.Size(265, 30);
            this.txt_MatKhauMoi.TabIndex = 1;
            // 
            // txt_XacNhanLaiMatKhau
            // 
            this.txt_XacNhanLaiMatKhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_XacNhanLaiMatKhau.Location = new System.Drawing.Point(399, 201);
            this.txt_XacNhanLaiMatKhau.Name = "txt_XacNhanLaiMatKhau";
            this.txt_XacNhanLaiMatKhau.Size = new System.Drawing.Size(265, 30);
            this.txt_XacNhanLaiMatKhau.TabIndex = 1;
            // 
            // btn_ThayDoi
            // 
            this.btn_ThayDoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(78)))), ((int)(((byte)(95)))));
            this.btn_ThayDoi.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ThayDoi.ForeColor = System.Drawing.Color.White;
            this.btn_ThayDoi.Location = new System.Drawing.Point(319, 288);
            this.btn_ThayDoi.Name = "btn_ThayDoi";
            this.btn_ThayDoi.Size = new System.Drawing.Size(115, 46);
            this.btn_ThayDoi.TabIndex = 2;
            this.btn_ThayDoi.Text = "THAY ĐỔI";
            this.btn_ThayDoi.UseVisualStyleBackColor = false;
            this.btn_ThayDoi.Click += new System.EventHandler(this.btn_ThayDoi_Click);
            // 
            // FrmDoiMatKhau
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_ThayDoi);
            this.Controls.Add(this.txt_XacNhanLaiMatKhau);
            this.Controls.Add(this.txt_MatKhauMoi);
            this.Controls.Add(this.txt_MatKhauCu);
            this.Controls.Add(this.XacNhanLaiMatKhau);
            this.Controls.Add(this.MatKhauMoi);
            this.Controls.Add(this.MatKhauCu);
            this.Name = "FrmDoiMatKhau";
            this.Text = "FrmDoiMatKhau";
            this.Load += new System.EventHandler(this.FrmDoiMatKhau_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MatKhauCu;
        private System.Windows.Forms.Label MatKhauMoi;
        private System.Windows.Forms.Label XacNhanLaiMatKhau;
        private System.Windows.Forms.TextBox txt_MatKhauCu;
        private System.Windows.Forms.TextBox txt_MatKhauMoi;
        private System.Windows.Forms.TextBox txt_XacNhanLaiMatKhau;
        private System.Windows.Forms.Button btn_ThayDoi;
    }
}