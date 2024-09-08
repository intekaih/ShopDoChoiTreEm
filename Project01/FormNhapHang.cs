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

namespace Project01
{
    public partial class frmNhapHang : Form
    {
        BindingSource bs = new BindingSource();

        public frmNhapHang()
        {
            InitializeComponent();

           
        }


        private void FormNhapHang_Load(object sender, EventArgs e)
        {
            QuanLySQL.KetNoi();
            txtTenHang.Focus();
            LoadData();
        }

        private void pbThemHang_Click(object sender, EventArgs e)
        {
            FormLoaiHang formLoaiHang = new FormLoaiHang();
            formLoaiHang.FormClosed += new FormClosedEventHandler(FormLoaiHang_FormClosed);
            formLoaiHang.ShowDialog();
        }

        private void FormLoaiHang_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Tải lại dữ liệu khi FormLoaiHang đóng
            LoadData();
        }



        private void LoadData()
        {
            // Lấy dữ liệu từ bảng QuanLyLoaiHang
            string query = "SELECT MaLoai, TenLoai, GhiChu FROM QuanLyLoaiHang";
            DataTable dt = QuanLySQL.XuatDLTuSQL(query);

            // Cài đặt BindingSource với DataTable
            bs.DataSource = dt;

            // Gán BindingSource cho ListBox
            cbLoaiHang.DataSource = bs;
            cbLoaiHang.DisplayMember = "TenLoai"; // Hiển thị cột TenLoai trong ListBox
            cbLoaiHang.ValueMember = "MaLoai"; // Đặt cột MaLoai làm giá trị của ListBox
        }

        private void pbHinhSP_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"; // Định dạng hình ảnh hỗ trợ
                openFileDialog.Title = "Chọn hình ảnh";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Hiển thị hình ảnh trong PictureBox
                    pbHinhSP.Image = Image.FromFile(openFileDialog.FileName);
                    pbHinhSP.ImageLocation = openFileDialog.FileName;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các trường
            string tenHang = txtTenHang.Text;
            string giaNhap = txtGiaNhap.Text;
            string giaBan = txtGiaBan.Text;
            string soLuong = txtSoLuong.Text;
            int maLoaiHang = Convert.ToInt32(cbLoaiHang.SelectedValue); // Lấy giá trị (MaLoai) của loại hàng
            string moTa = txtNote.Text;
            bool trangThai = cbTrangThaiBan.Checked;

            // Lấy đường dẫn của hình ảnh
            string duongDanAnh = string.Empty;
            if (pbHinhSP.Image != null)
            {
                duongDanAnh = pbHinhSP.ImageLocation;

            }

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(tenHang) || string.IsNullOrEmpty(giaNhap) || string.IsNullOrEmpty(giaBan) || string.IsNullOrEmpty(soLuong))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Kiểm tra xem hàng hóa đã tồn tại chưa
            QuanLySQL.KetNoi(); // Đảm bảo rằng bạn đã kết nối tới cơ sở dữ liệu
            if (QuanLySQL.KiemTraHangHoaTonTai(tenHang, maLoaiHang))
            {
                MessageBox.Show("Đồ chơi đã tồn tại");
                return;
            }

            // Tạo câu lệnh SQL để thêm dữ liệu vào bảng QuanLyHang
            string insertQuery = "INSERT INTO QuanLyHang (TenHang, GiaNhap, GiaBan, SoLuong, MaLoaiHang, GhiChu, TrangThai, DuongDanAnh) " +
                                 $"VALUES (N'{tenHang}', {giaNhap}, {giaBan}, {soLuong}, {maLoaiHang}, N'{moTa}', {(trangThai ? 1 : 0)}, N'{duongDanAnh}')";

            // Thực thi câu lệnh SQL để thêm bản ghi mới
            QuanLySQL.NhapDLVaoSQL(insertQuery);

            MessageBox.Show("Thêm hàng hóa thành công!");
        }

        private void txtGiaNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtGiaBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true; //Chỉ nhập sô
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnThoatFNH_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

