using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2021.PowerPoint.Comment;
using DocumentFormat.OpenXml.Wordprocessing;
using Project01.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Project01.Login
{
    public partial class FrmDoiMatKhau : Form


    {
        public string ID { get; set; }
        public string TenDangNhap { get; set; }
        public FrmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void FrmDoiMatKhau_Load(object sender, EventArgs e)
        {

        }

        private void MatKhauCu_Click(object sender, EventArgs e)
        {

        }

        private void btn_ThayDoi_Click(object sender, EventArgs e)
        {


            string MatKhauCu = txt_MatKhauCu.Text;
            string MatKhauMoi = txt_MatKhauMoi.Text;
            string  XacNhanLaiMatKhau= txt_XacNhanLaiMatKhau.Text;

            if (string.IsNullOrEmpty(MatKhauCu) || string.IsNullOrEmpty(MatKhauMoi) || string.IsNullOrEmpty(XacNhanLaiMatKhau))
            {
                MessageBox.Show("Please fill all info");
                return;
            }

            if (MatKhauMoi != XacNhanLaiMatKhau)
            {
                MessageBox.Show("New password and confirm password are not the same");
                return;
            }
            string sql;
            sql = $"SELECT * FROM TaiKhoan where ID= '{ID}'";
            var datatbl = QuanLySQL.GetDataToTable(sql);
            if (datatbl.Rows.Count == 0)
            {
                MessageBox.Show("Mật khẩu không chính xác, vui lòng thử lại!");
                return;
            }

            string sqlChangePass = $"update TaiKhoan set MatKhau= N'{MatKhauMoi}' Where ID = '{ID}'";
            QuanLySQL.RunSQL(sqlChangePass);
            
            MessageBox.Show("Đổi mật khẩu  thành công");

            this.Close();
        }

        private void btn_reset_password_Click(object sender, EventArgs e)
        {
            string sqlChangePass = $"update TaiKhoan set MatKhau= N'{TenDangNhap}' Where ID = '{ID}'";
            QuanLySQL.RunSQL(sqlChangePass);

            MessageBox.Show("Tạo lại mật khẩu thành công. Mật khẩu mới trùng với tên đăng nhập");
        }
    }
    }
