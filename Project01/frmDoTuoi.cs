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
    public partial class frmDoTuoi : Form
    {
        public event EventHandler DataChanged;
        BindingSource bs = new BindingSource();

        public frmDoTuoi()
        {
            InitializeComponent();
        }

        private void frmDoTuoi_Load(object sender, EventArgs e)
        {
            QuanLySQL.KetNoi();
            LoadData();
        }

        public void LoadData()
        {
            // Lấy dữ liệu từ bảng DoTuoi
            string query = "SELECT ID, MoTa From DoTuoi";
            DataTable dt = QuanLySQL.XuatDLTuSQL(query);

            // Cài đặt BindingSource với DataTable
            bs.DataSource = dt;

            // Gán BindingSource cho ListBox
            lboDoTuoi.DataSource = bs;
            lboDoTuoi.DisplayMember = "MoTa"; // Hiển thị cột Ten trong ListBox
            lboDoTuoi.ValueMember = "ID"; // Đặt cột ID làm giá trị của ListBox
            lboDoTuoi.SelectedItem = null;
            MacDinh();


        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin nhập vào
            if (string.IsNullOrEmpty(txtDoTuoi.Text))
            {
                MessageBox.Show("Độ tuổi không được để trống.");
                return;
            }

            // Lấy thông tin từ các TextBox
            string DoTuoi = txtDoTuoi.Text;

            // Câu lệnh SQL để thêm loại hàng
            string query = $"INSERT INTO DoTuoi (MoTa) VALUES ('{DoTuoi}')";

            

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
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Chọn độ tuổi hàng để sửa.");
                return;
            }

            if (string.IsNullOrEmpty(txtDoTuoi.Text))
            {
                MessageBox.Show("Độ tuổi không được để trống.");
                return;
            }

            // Lấy thông tin từ các TextBox
            string iD = txtID.Text;
            string DoTuoi = txtDoTuoi.Text;

            // Câu lệnh SQL để sửa thông tin loại hàng
            string query = $"UPDATE DoTuoi SET MoTa = '{DoTuoi}' WHERE ID = '{iD}'";

           

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
            if (string.IsNullOrEmpty(txtID.Text))
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
            string iD = txtID.Text;

            // Câu lệnh SQL để xóa loại hàng
            string query = $"DELETE FROM DoTuoi WHERE ID = '{iD}'";

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
            txtID.Clear();
            txtDoTuoi.Clear();
            txtDoTuoi.Focus();
        }

        private void lboDoTuoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lboDoTuoi.SelectedItem != null)
            {
                // Lấy đối tượng được chọn từ BindingSource
                DataRowView selectedRow = (DataRowView)lboDoTuoi.SelectedItem;

                // Cập nhật các ô nhập liệu với thông tin từ đối tượng đã chọn
                txtID.Text = selectedRow["ID"].ToString();
                txtDoTuoi.Text = selectedRow["MoTa"].ToString();
            }
        }
    }
}
