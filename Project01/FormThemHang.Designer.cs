namespace Project01
{
    partial class frmThemHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThemHang));
            this.btnThem = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.txtTenHang = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.pbHinhSP = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.cboTrangThaiBan = new System.Windows.Forms.CheckBox();
            this.cbLoaiHang = new System.Windows.Forms.ComboBox();
            this.pbThemLoai = new System.Windows.Forms.PictureBox();
            this.btnSua = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDoTuoi = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbXuatXu = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbHangSX = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbHinhSP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThemLoai)).BeginInit();
            this.SuspendLayout();
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(176)))), ((int)(((byte)(211)))));
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(291, 394);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(108, 33);
            this.btnThem.TabIndex = 8;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(176)))), ((int)(((byte)(211)))));
            this.btnThoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ForeColor = System.Drawing.Color.Transparent;
            this.btnThoat.Location = new System.Drawing.Point(533, 394);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(108, 33);
            this.btnThoat.TabIndex = 9;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // txtTenHang
            // 
            this.txtTenHang.Location = new System.Drawing.Point(156, 165);
            this.txtTenHang.Name = "txtTenHang";
            this.txtTenHang.Size = new System.Drawing.Size(303, 29);
            this.txtTenHang.TabIndex = 0;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(156, 121);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(303, 29);
            this.txtID.TabIndex = 2;
            this.txtID.TabStop = false;
            this.txtID.TextChanged += new System.EventHandler(this.txtMaHH_TextChanged);
            // 
            // pbHinhSP
            // 
            this.pbHinhSP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbHinhSP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbHinhSP.Image = ((System.Drawing.Image)(resources.GetObject("pbHinhSP.Image")));
            this.pbHinhSP.Location = new System.Drawing.Point(201, 14);
            this.pbHinhSP.Name = "pbHinhSP";
            this.pbHinhSP.Size = new System.Drawing.Size(94, 82);
            this.pbHinhSP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbHinhSP.TabIndex = 1;
            this.pbHinhSP.TabStop = false;
            this.pbHinhSP.Click += new System.EventHandler(this.pbHinhSP_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 344);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 21);
            this.label9.TabIndex = 0;
            this.label9.Text = "Mô tả sản phẩm:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 80);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "Hình ảnh sản phẩm:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 212);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Loại Hàng";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 168);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tên hàng:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 124);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã Hàng Hóa:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(519, 256);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "Số lượng tồn kho:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(519, 212);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 21);
            this.label8.TabIndex = 0;
            this.label8.Text = "Giá bán:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(519, 168);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "Giá nhập:";
            // 
            // txtGiaNhap
            // 
            this.txtGiaNhap.Location = new System.Drawing.Point(673, 165);
            this.txtGiaNhap.Name = "txtGiaNhap";
            this.txtGiaNhap.Size = new System.Drawing.Size(251, 29);
            this.txtGiaNhap.TabIndex = 3;
            this.txtGiaNhap.TextChanged += new System.EventHandler(this.txtGiaNhap_TextChanged);
            this.txtGiaNhap.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGiaNhap_KeyPress);
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Location = new System.Drawing.Point(673, 209);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.Size = new System.Drawing.Size(251, 29);
            this.txtGiaBan.TabIndex = 4;
            this.txtGiaBan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGiaBan_KeyPress);
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(673, 253);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(251, 29);
            this.txtSoLuong.TabIndex = 5;
            this.txtSoLuong.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSoLuong_KeyPress);
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(156, 341);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(768, 29);
            this.txtNote.TabIndex = 6;
            // 
            // cboTrangThaiBan
            // 
            this.cboTrangThaiBan.AutoSize = true;
            this.cboTrangThaiBan.Checked = true;
            this.cboTrangThaiBan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboTrangThaiBan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboTrangThaiBan.Location = new System.Drawing.Point(523, 120);
            this.cboTrangThaiBan.Name = "cboTrangThaiBan";
            this.cboTrangThaiBan.Size = new System.Drawing.Size(156, 25);
            this.cboTrangThaiBan.TabIndex = 7;
            this.cboTrangThaiBan.Text = "Đang kinh doanh";
            this.cboTrangThaiBan.UseVisualStyleBackColor = true;
            // 
            // cbLoaiHang
            // 
            this.cbLoaiHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoaiHang.FormattingEnabled = true;
            this.cbLoaiHang.Location = new System.Drawing.Point(156, 209);
            this.cbLoaiHang.Name = "cbLoaiHang";
            this.cbLoaiHang.Size = new System.Drawing.Size(303, 28);
            this.cbLoaiHang.TabIndex = 1;
            // 
            // pbThemLoai
            // 
            this.pbThemLoai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbThemLoai.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pbThemLoai.ErrorImage")));
            this.pbThemLoai.Image = ((System.Drawing.Image)(resources.GetObject("pbThemLoai.Image")));
            this.pbThemLoai.Location = new System.Drawing.Point(466, 211);
            this.pbThemLoai.Name = "pbThemLoai";
            this.pbThemLoai.Size = new System.Drawing.Size(24, 24);
            this.pbThemLoai.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbThemLoai.TabIndex = 35;
            this.pbThemLoai.TabStop = false;
            this.pbThemLoai.Click += new System.EventHandler(this.pbThemLoai_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(176)))), ((int)(((byte)(211)))));
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(291, 394);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(108, 33);
            this.btnSua.TabIndex = 8;
            this.btnSua.Text = "Lưu";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Visible = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(519, 300);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "Xuất xứ";
            // 
            // cbDoTuoi
            // 
            this.cbDoTuoi.BackColor = System.Drawing.SystemColors.Window;
            this.cbDoTuoi.FormattingEnabled = true;
            this.cbDoTuoi.Location = new System.Drawing.Point(156, 253);
            this.cbDoTuoi.Name = "cbDoTuoi";
            this.cbDoTuoi.Size = new System.Drawing.Size(303, 28);
            this.cbDoTuoi.Sorted = true;
            this.cbDoTuoi.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 256);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 21);
            this.label11.TabIndex = 0;
            this.label11.Text = "Độ Tuổi ";
            // 
            // cbXuatXu
            // 
            this.cbXuatXu.FormattingEnabled = true;
            this.cbXuatXu.Location = new System.Drawing.Point(673, 297);
            this.cbXuatXu.Name = "cbXuatXu";
            this.cbXuatXu.Size = new System.Drawing.Size(251, 28);
            this.cbXuatXu.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 300);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 21);
            this.label10.TabIndex = 0;
            this.label10.Text = "Hãng Sản Xuất";
            // 
            // cbHangSX
            // 
            this.cbHangSX.FormattingEnabled = true;
            this.cbHangSX.Location = new System.Drawing.Point(156, 297);
            this.cbHangSX.Name = "cbHangSX";
            this.cbHangSX.Size = new System.Drawing.Size(303, 28);
            this.cbHangSX.TabIndex = 1;
            // 
            // frmThemHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 454);
            this.Controls.Add(this.pbThemLoai);
            this.Controls.Add(this.cbHangSX);
            this.Controls.Add(this.cbXuatXu);
            this.Controls.Add(this.cbDoTuoi);
            this.Controls.Add(this.cbLoaiHang);
            this.Controls.Add(this.cboTrangThaiBan);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.txtGiaBan);
            this.Controls.Add(this.txtTenHang);
            this.Controls.Add(this.txtGiaNhap);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.pbHinhSP);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmThemHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmThemHang";
            this.Load += new System.EventHandler(this.FormNhapHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbHinhSP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbThemLoai)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnThoat;
        public System.Windows.Forms.TextBox txtTenHang;
        public System.Windows.Forms.TextBox txtID;
        public System.Windows.Forms.PictureBox pbHinhSP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txtGiaNhap;
        public System.Windows.Forms.TextBox txtGiaBan;
        public System.Windows.Forms.TextBox txtSoLuong;
        public System.Windows.Forms.TextBox txtNote;
        public System.Windows.Forms.CheckBox cboTrangThaiBan;
        public System.Windows.Forms.ComboBox cbLoaiHang;
        private System.Windows.Forms.PictureBox pbThemLoai;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cbDoTuoi;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.ComboBox cbXuatXu;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.ComboBox cbHangSX;
    }
}