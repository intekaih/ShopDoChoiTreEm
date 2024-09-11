using Project01.Class;
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
    public partial class frmHangSX : Form
    {
        public event EventHandler DataChanged;
        BindingSource bs = new BindingSource();
        public frmHangSX()
        {
            InitializeComponent();
        }

        private void frmHangSX_Load(object sender, EventArgs e)
        {
            QuanLySQL.KetNoi();
            LoadData();
            Lienket();
            VeBoGocPanel.BoGocPanel(pHangSX, 30);         
        }
        // Liên kết các TextBox với BindingSource, BS với dgv
        private void Lienket()
        {
            txtID.DataBindings.Add("Text", bs, "ID");
            txtHangSX.DataBindings.Add("Text", bs, "Ten");
            txtXuatXu.DataBindings.Add("Text", bs, "XuatXu");
            dgvHangSX.Columns["ID"].DataPropertyName = "ID"; 
            dgvHangSX.Columns["HangSX"].DataPropertyName = "Ten"; 
            dgvHangSX.Columns["XuatXu"].DataPropertyName = "XuatXu"; 
        }

        public void LoadData()
        {
            //dgvHangSX.Rows.Clear();

            // Nạp dữ liệu vào DataGridView từ SQL
            DataTable dataTable = QuanLySQL.XuatDLTuSQL("SELECT ID, Ten, XuatXu FROM HangSX where Enable = 1");

            
            bs.DataSource = dataTable;

            // Gán BindingSource cho DataGridView
            dgvHangSX.DataSource = bs;
            
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin nhập vào
            if (string.IsNullOrEmpty(txtHangSX.Text))
            {
                MessageBox.Show("Tên loại không được để trống.");
                return;
            }

            // Lấy thông tin từ các TextBox
            string HangSX = txtHangSX.Text;

            // Câu lệnh SQL để thêm loại hàng
            string query = $"INSERT INTO HangSX (Ten, XuatXu) VALUES (N'{HangSX}', N'{txtXuatXu.Text}')";



            // Gọi phương thức để thực thi câu lệnh SQL
            QuanLySQL.NhapDLVaoSQL(query);
            //Thong Bao thay doi data
            DataChanged?.Invoke(this, EventArgs.Empty);

            // Cập nhật ListBox
            LoadData();

           
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin nhập vào và loại hàng được chọn
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Chọn hãng sản xuất để sửa.");
                return;
            }

            if (string.IsNullOrEmpty(txtHangSX.Text))
            {
                MessageBox.Show("Hãng sản xuất không được để trống.");
                return;
            }


            // Lấy thông tin từ các TextBox
            string iD = txtID.Text;
            string HangSX = txtHangSX.Text;
            string XuatXu = txtXuatXu.Text;

            // Câu lệnh SQL để sửa thông tin loại hàng
            string query = $"UPDATE HangSX SET Ten = N'{HangSX}', XuatXu = N'{XuatXu}' WHERE ID = '{iD}'";

            // Gọi phương thức để thực thi câu lệnh SQL
            QuanLySQL.NhapDLVaoSQL(query);

            DataChanged?.Invoke(this, EventArgs.Empty);

            // Cập nhật ListBox
            LoadData();

           
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra thông tin loại hàng được chọn
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Chọn hãng sản xuất để xóa.");
                return;
            }

            // Xác nhận việc xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hãng sản xuất này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
            if (result == DialogResult.No)
            {
                return;
            }

            // Lấy mã loại hàng từ TextBox
            string iD = txtID.Text;

            // Câu lệnh SQL để xóa loại hàng
            string query = $"UPDATE HangSX\r\nSET Enable = 0\r\nWHERE ID = '{iD}'";

            // Gọi phương thức để thực thi câu lệnh SQL
            QuanLySQL.NhapDLVaoSQL(query);

            DataChanged?.Invoke(this, EventArgs.Empty);

            // Cập nhật ListBox
            LoadData();

            
        }


        private void MacDinh()
        {
            txtID.Clear();
            txtHangSX.Clear();
            txtXuatXu.Clear ();
            txtHangSX.Focus();
        }

        private void dgvHangSX_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
