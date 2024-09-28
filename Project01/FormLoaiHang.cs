using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Project01.Class;

namespace Project01
{
    public partial class frmLoaiHang : Form
    {

        public event EventHandler DataChanged;
        BindingSource bs = new BindingSource();

        public frmLoaiHang()
        {
            InitializeComponent();
        }

        private void FormLoaiHang_Load(object sender, EventArgs e)
        {
            QuanLySQL.KetNoi();
            LoadData();

        }

        public void LoadData()
        {
            // Lấy dữ liệu từ bảng QuanLyLoaiHang
            string query = "SELECT ID, Ten From LoaiSP";
            DataTable dt = QuanLySQL.XuatDLTuSQL(query);

            // Cài đặt BindingSource với DataTable
            bs.DataSource = dt;

            // Gán BindingSource cho ListBox
            lboDSLoai.DataSource = bs;
            lboDSLoai.DisplayMember = "Ten"; // Hiển thị cột Ten trong ListBox
            lboDSLoai.ValueMember = "ID"; // Đặt cột ID làm giá trị của ListBox
            lboDSLoai.SelectedItem = null;
            MacDinh();


        }

        private void lboDSLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lboDSLoai.SelectedItem != null)
            {
                // Lấy đối tượng được chọn từ BindingSource
                DataRowView selectedRow = (DataRowView)lboDSLoai.SelectedItem;

                // Cập nhật các ô nhập liệu với thông tin từ đối tượng đã chọn
                txtMaLoai.Text = selectedRow["ID"].ToString();
                txtTenLoai.Text = selectedRow["Ten"].ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin nhập vào
            if (string.IsNullOrEmpty(txtTenLoai.Text))
            {
                MessageBox.Show("Tên loại không được để trống.");
                return;
            }

            // Lấy thông tin từ các TextBox
            string tenLoai = txtTenLoai.Text;

            // Câu lệnh SQL để thêm loại hàng
            string query = $"INSERT INTO QuanLyLoaiHang (Ten) VALUES ('{tenLoai}')";

            

            // Gọi phương thức để thực thi câu lệnh SQL
            QuanLySQL.NhapDLVaoSQL(query);
            //Thong Bao thay doi data
            DataChanged?.Invoke(this, EventArgs.Empty);

            // Cập nhật ListBox
            LoadData();

            // Xóa thông tin nhập liệu
            MacDinh();
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin nhập vào và loại hàng được chọn
            if (string.IsNullOrEmpty(txtMaLoai.Text))
            {
                MessageBox.Show("Chọn loại hàng để sửa.");
                return;
            }

            if (string.IsNullOrEmpty(txtTenLoai.Text))
            {
                MessageBox.Show("Tên loại không được để trống.");
                return;
            }

            // Lấy thông tin từ các TextBox
            string maLoai = txtMaLoai.Text;
            string tenLoai = txtTenLoai.Text;

            // Câu lệnh SQL để sửa thông tin loại hàng
            string query = $"UPDATE QuanLyLoaiHang SET Ten = '{tenLoai}' WHERE ID = '{maLoai}'";

            

            // Gọi phương thức để thực thi câu lệnh SQL
            QuanLySQL.NhapDLVaoSQL(query);

            DataChanged?.Invoke(this, EventArgs.Empty);

            // Cập nhật ListBox
            LoadData();

            // Xóa thông tin nhập liệu
            MacDinh();
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin loại hàng được chọn
            if (string.IsNullOrEmpty(txtMaLoai.Text))
            {
                MessageBox.Show("Chọn loại hàng để xóa.");
                return;
            }

            // Xác nhận việc xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa loại hàng này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            // Lấy mã loại hàng từ TextBox
            string maLoai = txtMaLoai.Text;

            // Câu lệnh SQL để xóa loại hàng
            string query = $"DELETE FROM QuanLyLoaiHang WHERE ID = '{maLoai}'";

            // Gọi phương thức để thực thi câu lệnh SQL
            QuanLySQL.NhapDLVaoSQL(query);

            DataChanged?.Invoke(this, EventArgs.Empty);

            // Cập nhật ListBox
            LoadData();

            // Xóa thông tin nhập liệu
            MacDinh();
        }


        private void MacDinh()
        {
            txtMaLoai.Clear();
            txtTenLoai.Clear();
            txtTenLoai.Focus();
        }

        private void FormLoaiHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
