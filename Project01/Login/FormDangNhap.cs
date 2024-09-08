using Project01.Class;
using System;
using System.Windows.Forms;

namespace Project01.Login
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string taiKhoan = txtTenDangNhap.Text;
            string matKhau = txtMatKhau.Text;

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu.");
                return;
            }

            QuanLySQL.KetNoi(); // Kết nối tới SQL Server

            bool isValid = QuanLySQL.KiemTraDangNhap(taiKhoan, matKhau);

            if (isValid)
            {

                // Thực hiện hành động sau khi đăng nhập thành công, ví dụ mở form chính
                this.Hide();
                frmHome mainForm = new frmHome();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác.");
            }

            QuanLySQL.NgatKetNoi(); // Ngắt kết nối khỏi SQL Server
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
