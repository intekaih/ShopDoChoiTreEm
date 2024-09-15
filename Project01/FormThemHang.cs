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
    public partial class frmThemHang : Form
    {
        public event EventHandler DataChanged;
        BindingSource Loai = new BindingSource();
        BindingSource DoTuoi = new BindingSource();
        BindingSource HangSX = new BindingSource();
        BindingSource XuatXu = new BindingSource();
        string anhMD = @"F:\Học Tập\ShopDoChoiTreEm\Picture\AnhMacDinh.jpg";



        public frmThemHang()
        {
            InitializeComponent();
            
        }


        private void FormNhapHang_Load(object sender, EventArgs e)
        {
            QuanLySQL.KetNoi();
            LoadData();
            //MacDinh();
        }

        

        private void pbThemLoai_Click(object sender, EventArgs e)
        {
            frmLoaiHang formLoaiHang = new frmLoaiHang();
            formLoaiHang.FormClosed += new FormClosedEventHandler(frmThemLoaiDong);
            formLoaiHang.ShowDialog();
        }


        private void frmThemLoaiDong(object sender, FormClosedEventArgs e)
        {
            LoadDataTocbLoaiHang();
        }

        //hàm load
        public void LoadData()
        {
            LoadDataTocbLoaiHang();
            LoadDataTocbDoTuoi();
            LoadDataTocbHangSX();
            LoadDataTocbXuatXu();
        }

        public void LoadDataTocbLoaiHang()
        {
            // Lấy dữ liệu từ bảng QuanLyLoaiHang
            string query = "SELECT ID, Ten FROM LoaiSP";
            DataTable dt = QuanLySQL.XuatDLTuSQL(query);

            // Cài đặt BindingSource với DataTable
            Loai.DataSource = dt;

            // Gán BindingSource cho ListBox
            cbLoaiHang.DataSource = Loai;
            cbLoaiHang.DisplayMember = "Ten";
            cbLoaiHang.ValueMember = "ID";
        }

        private void LoadDataTocbDoTuoi()
        {
            // Lấy dữ liệu từ bảng QuanLyLoaiHang
            string query = "SELECT ID, Ten FROM DoTuoi";
            DataTable dt = QuanLySQL.XuatDLTuSQL(query);

            // Cài đặt BindingSource với DataTable
            DoTuoi.DataSource = dt;

            // Gán BindingSource cho ListBox
            cbDoTuoi.DataSource = DoTuoi;
            cbDoTuoi.DisplayMember = "Ten";
            cbDoTuoi.ValueMember = "ID";
        }

        private void LoadDataTocbHangSX()
        {
            // Lấy dữ liệu từ bảng QuanLyLoaiHang
            string query = "SELECT ID, Ten FROM HangSX";
            DataTable dt = QuanLySQL.XuatDLTuSQL(query);

            // Cài đặt BindingSource với DataTable
            HangSX.DataSource = dt;

            // Gán BindingSource cho ListBox
            cbHangSX.DataSource = HangSX;
            cbHangSX.DisplayMember = "Ten";
            cbHangSX.ValueMember = "ID";
        }

        private void LoadDataTocbXuatXu()
        {
            // Lấy dữ liệu từ bảng QuanLyLoaiHang
            string query = "SELECT ID, Ten FROM XuatXu";
            DataTable dt = QuanLySQL.XuatDLTuSQL(query);

            // Cài đặt BindingSource với DataTable
            XuatXu.DataSource = dt;

            // Gán BindingSource cho ListBox
            cbXuatXu.DataSource = XuatXu;
            cbXuatXu.DisplayMember = "Ten";
            cbXuatXu.ValueMember = "ID";
        }
        //Them Hinh
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
        //XyLybntThem
        private void btnThem_Click(object sender, EventArgs e)
        {
            string bangDL = "SanPham";

            // Lấy dữ liệu từ các trường
            string tenHang = txtTenHang.Text;
            string giaNhap = txtGiaNhap.Text;
            string giaBan = txtGiaBan.Text;
            string soLuong = txtSoLuong.Text;
            int loaiID = Convert.ToInt32(cbLoaiHang.SelectedValue); // Lấy giá trị (ID) của loại hàng
            int hangID = QuanLySQL.KTVaThemGT("HangSX", "Ten", cbHangSX.Text);
            int xuatxuID = QuanLySQL.KTVaThemGT("XuatXu", "Ten", cbXuatXu.Text);
            int dotuoiID = QuanLySQL.KTVaThemGT("DoTuoi", "Ten", cbDoTuoi.Text);
            string moTa = txtNote.Text;
            bool enable = cboTrangThaiBan.Checked;

            // Lấy đường dẫn của hình ảnh
            string anhPath = string.Empty;
            if (pbHinhSP.Image != null)
            {
                anhPath = pbHinhSP.ImageLocation;
            }

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(tenHang) || string.IsNullOrEmpty(giaNhap) || string.IsNullOrEmpty(giaBan) || string.IsNullOrEmpty(soLuong))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            // Kiểm tra xem đồ chơi đã tồn tại chưa
            if (QuanLySQL.KTtontai("SanPham", tenHang, loaiID))
            {
                MessageBox.Show("Đồ chơi đã tồn tại");
                return;
            }

            string insertQuery = $"INSERT INTO [{bangDL}] (Ten, LoaiID, HangID, XuatXuID, DoTuoiID, GiaNhap, GiaBan, Ton, MoTa, HinhAnhURL, Enable) " +
                                 $"VALUES (N'{tenHang}', {loaiID}, {hangID}, {xuatxuID}, {dotuoiID}, {giaNhap}, {giaBan}, {soLuong}, N'{moTa}', N'{anhPath}', {(enable ? 1 : 0)})";

            QuanLySQL.NhapDLVaoSQL(insertQuery);

            MessageBox.Show("Thêm hàng hóa thành công!");
            DataChanged?.Invoke(this, EventArgs.Empty);
            MacDinh();


        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin nhập vào và sản phẩm được chọn
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Chọn sản phẩm để sửa.");
                return;
            }

            // Lấy thông tin từ các TextBox
            int id = Convert.ToInt32(txtID.Text);
            string tenHang = txtTenHang.Text;
            decimal giaNhap = Convert.ToDecimal(txtGiaNhap.Text);
            decimal giaBan = Convert.ToDecimal(txtGiaBan.Text);
            int soLuong = Convert.ToInt32(txtSoLuong.Text);
            int loaiID = Convert.ToInt32(cbLoaiHang.SelectedValue); // Lấy giá trị (ID) của loại hàng
            int hangID = Convert.ToInt32(cbHangSX.SelectedValue);
            int xuatxuID = Convert.ToInt32(cbXuatXu.SelectedValue);
            int dotuoiID = Convert.ToInt32(cbDoTuoi.SelectedValue);
            string moTa = txtNote.Text;
            bool enable = cboTrangThaiBan.Checked;

            // Lấy đường dẫn của hình ảnh
            string anhPath = string.Empty;
            if (pbHinhSP.Image != null)
            {
                anhPath = pbHinhSP.ImageLocation;
            }

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(tenHang) || string.IsNullOrEmpty(txtGiaNhap.Text) || string.IsNullOrEmpty(txtGiaBan.Text) || string.IsNullOrEmpty(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            string updateQuery = $"UPDATE SanPham SET " +
                        $"Ten = N'{tenHang}', " +
                        $"LoaiID = {loaiID}, " +
                        $"HangID = {hangID}, " +
                        $"XuatXuID = {xuatxuID}, " +
                        $"DoTuoiID = {dotuoiID}, " +
                        $"GiaNhap = {giaNhap}, " +
                        $"GiaBan = {giaBan}, " +
                        $"Ton = {soLuong}, " +
                        $"MoTa = N'{moTa}', " +
                        $"HinhAnhURL = N'{anhPath}', " +
                        $"Enable = {(enable ? 1 : 0)} " +
                        $"WHERE ID = {id}";

            QuanLySQL.NhapDLVaoSQL(updateQuery);
            // Cập nhật dữ liệu và thông báo
            MessageBox.Show("Cập nhật hàng hóa thành công!");
            DataChanged?.Invoke(this, EventArgs.Empty);
            MacDinh();
        }


        //chỉ cho phép nhập số
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


        public void HienThiBtnSua()
        {
            btnSua.Visible = true;
            btnThem.Visible = false;
        }

        public void MacDinh()
        {
            // Xóa dữ liệu trong các TextBox
            txtTenHang.Text = string.Empty;
            txtGiaNhap.Text = string.Empty;
            txtGiaBan.Text = string.Empty;
            txtSoLuong.Text = string.Empty;
            txtNote.Text = string.Empty;

            //combobox index = -1
            cbXuatXu.SelectedIndex = -1;
            cbLoaiHang.SelectedIndex = -1;
            cbDoTuoi.SelectedIndex = -1;
            cbHangSX.SelectedIndex = -1;

            // Đặt trạng thái ban đầu cho CheckBox
            cboTrangThaiBan.Checked = true;

            // Xóa hình ảnh trong PictureBox
            pbHinhSP.Image = Image.FromFile(anhMD);

            // Đặt lại tiêu điểm vào TextBox đầu tiên (nếu cần)
            txtTenHang.Focus();
        }

        private void txtGiaNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtMaHH_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}

