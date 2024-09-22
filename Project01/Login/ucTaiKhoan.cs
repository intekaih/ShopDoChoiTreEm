using DocumentFormat.OpenXml.Math;
using Project01.Class;
using Project01.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Project01
{
    public partial class ucTaiKhoan : Form
    {
        BindingSource bs = new BindingSource();

        public ucTaiKhoan()
        {
            InitializeComponent();
        }
      

        private void ucTaiKhoan_Load(object sender, EventArgs e)
        {
            //TODO: khong can ket noi
            QuanLySQL.KetNoi();
            LoadTKToDGV();
        }

        private void LoadTKToDGV()
        {
            string query = "select * from TaiKhoan where enable = 1";
            DataTable dt = QuanLySQL.XuatDLTuSQL(query);

            //bs.DataSource = dt;

            dgvTaiKhoan.DataSource = dt;
        }

        private bool CheckID()
        {
            string query = $"select * from Taikhoan where TenDangNhap = '{txtTenDangNhap}'";
            var data = QuanLySQL.LayTruongDuLieu(query);

            if (data == txtTenDangNhap.Text)
            {
                return true;
            }
            return false;
        }


        private void dgvTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTenDangNhap.Text = dgvTaiKhoan.SelectedCells[1].Value.ToString();
            txtHoTen.Text = dgvTaiKhoan.SelectedCells[3].Value.ToString();
            txtSoDienThoai.Text = dgvTaiKhoan.SelectedCells[6].Value.ToString();
            cb0VaiTro.Text = dgvTaiKhoan.SelectedCells[4].Value.ToString();
            btn_Xoa.Enabled = true;
            btn_CapNhat.Enabled = true;

        }

        private void dgvTaiKhoan_Click(object sender, EventArgs e)
        {
            var debug = dgvTaiKhoan;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void btn_them_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "" || txtHoTen.Text == "" || txtSoDienThoai.Text == "" || CheckID() == true)
            {
                MessageBox.Show("Thêm Thất Bại");
            }
            else
            {
                string sql = $"insert into TaiKhoan(TenDangNhap, MatKhau,  HoTen, VaiTro, Enable, SDT) values (N'{txtTenDangNhap.Text}',N'{txtTenDangNhap.Text}', N'{txtHoTen.Text}','{cb0VaiTro.Text}',1, '{txtSoDienThoai.Text}' )";
                QuanLySQL.NhapDLVaoSQL(sql);
                MessageBox.Show("Thêm Thành Công");
                LoadTKToDGV();
            }
        }

        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "" || txtHoTen.Text == "" || txtSoDienThoai.Text == "")
            {
                MessageBox.Show("Cập nhật thất bại, vui lòng điền đầy đủ thông tin.");
            }
            else
            {
                // Sử dụng câu lệnh UPDATE để cập nhật thông tin
                string sql = $"UPDATE TaiKhoan " +
                             $"SET MatKhau = N'{txtTenDangNhap.Text}', HoTen = N'{txtHoTen.Text}', " +
                             $"VaiTro = N'{cb0VaiTro.Text}', Enable = 1, SDT = '{txtSoDienThoai.Text}' " +
                             $"WHERE TenDangNhap = N'{txtTenDangNhap.Text}'";
                

                QuanLySQL.NhapDLVaoSQL(sql);
                MessageBox.Show("Cập nhật thành công!");
                LoadTKToDGV();
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count == 0) return;

            int idDelete = (int)dgvTaiKhoan.SelectedCells[0].Value;
            DialogResult dialogResult = MessageBox.Show("     BẠN CÓ CHẮC CHẮN  ", "  XÓA", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                // Sửa câu lệnh SQL với tên cột chính xác
                string query = $"DELETE FROM TaiKhoan WHERE ID = {idDelete}"; // Thay 'TenDangNhap' bằng tên cột chính xác
                QuanLySQL.NhapDLVaoSQL(query);
                LoadTKToDGV();
            }
        }

        private void btn_TaoMoi_Click(object sender, EventArgs e)
        {
            TaoMoiText();
        }
        private void TaoMoiText()
        {
            txtTenDangNhap.Text = "";
           txtHoTen.Text = "";
            txtSoDienThoai.Text = "";
            cb0VaiTro.SelectedIndex = -1;
            btn_Xoa.Enabled = false;
            btn_CapNhat.Enabled = false;
        }

        private void btn_DoiMatKhau_Click(object sender, EventArgs e)
        {
            using (FrmDoiMatKhau changePasswordForm = new FrmDoiMatKhau())
            {
                changePasswordForm.TenDangNhap = dgvTaiKhoan.SelectedCells[0].Value.ToString();
                changePasswordForm.ShowDialog();
            }
        }

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }
    } 

}

