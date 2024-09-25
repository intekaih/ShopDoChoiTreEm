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
    public partial class frmKhachHang : Form
    {
        public event EventHandler DataChanged;

        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            QuanLySQL.KetNoi();
            LoadDataToDGV();
            TrangTri.BoGocPanel(pSua, 15);
            TrangTri.BoGocPanel(pThem, 15);
            TrangTri.BoGocPanel(pThoat, 15);
            TrangTri.BoGocPanel(pXoa, 15);

        }

        private void LoadDataToDGV()
        {
            dgvKhachHang.Rows.Clear();
            string query = "select * from KhachHang where enable = 1";
            DataTable khachHang = QuanLySQL.XuatDLTuSQL(query);

            foreach (DataRow row in khachHang.Rows)
            {
                int rowIndex = dgvKhachHang.Rows.Add();
                dgvKhachHang.Rows[rowIndex].Cells["ID"].Value = row["ID"];
                dgvKhachHang.Rows[rowIndex].Cells["HoTen"].Value = row["HoTen"];
                dgvKhachHang.Rows[rowIndex].Cells["DienThoai"].Value = row["DienThoai"];
                dgvKhachHang.Rows[rowIndex].Cells["DiaChi"].Value = row["DiaChi"];

            }
        }

        private void pXoa_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectRow = dgvKhachHang.SelectedRows[0];

            string id = selectRow.Cells["ID"].Value.ToString();

            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Chọn dữ liệu trước khi xóa!", "Thông báo!");
                return;
            }

            string queryDl = $"UPDATE KhachHang SET Enable = 0 WHERE ID = '{id}'";
            QuanLySQL.NhapDLVaoSQL(queryDl);
            lbThongBao.Text = "Successfully!";
            LoadDataToDGV();
            DataChanged?.Invoke(this, EventArgs.Empty);
            MacDinh();

        }

        private void pSua_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectRow = dgvKhachHang.SelectedRows[0];

            string id = selectRow.Cells["ID"].Value.ToString();

            DataTable dt = QuanLySQL.XuatDLTuSQL($"select * from KhachHang where ID = '{id}'");

            string hoTen = txtHoTen.Text.ToString();
            string dienThoai = txtDienThoai.Text.ToString();
            string diaChi = txtDiaChi.Text.ToString();

            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Không được để dữ liệu trống!", "Thông báo!");
                return;
            }

            string queryUd = "Update KhachHang set " +
                    $"HoTen = N'{hoTen}', " +
                    $"DienThoai = '{dienThoai}', " +
                    $"DiaChi = N'{diaChi}'" +
                    $"Where ID = '{id}'";
            QuanLySQL.NhapDLVaoSQL(queryUd);
            lbThongBao.Text = "Successfully!";
            LoadDataToDGV();
            DataChanged?.Invoke(this, EventArgs.Empty);
            MacDinh();
        }


        private void pThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTen.Text))
            {
                MessageBox.Show("Không được để dữ liệu trống!", "Thông báo!");
                return;
            }

            string queryIs = $"insert into KhachHang (HoTen, DienThoai, DiaChi)" +
                $"Values (N'{txtHoTen.Text.ToString()}', '{txtDienThoai.Text.ToString()}', N'{txtDiaChi.Text.ToString()}')";
            QuanLySQL.NhapDLVaoSQL(queryIs);
            lbThongBao.Text = "Successfully!";
            LoadDataToDGV();
            DataChanged?.Invoke(this, EventArgs.Empty);
            MacDinh();
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectRow = dgvKhachHang.SelectedRows[0];

            string id = selectRow.Cells["ID"].Value.ToString();

            DataTable dt = QuanLySQL.XuatDLTuSQL($"select * from KhachHang where ID = '{id}'");

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                txtHoTen.Text = row["HoTen"].ToString();
                txtDienThoai.Text = row["DienThoai"].ToString();
                txtDiaChi.Text = row["DiaChi"].ToString();
            }
            lbThongBao.Text = "";
        }

        private void MacDinh()
        {
            txtHoTen.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();
            txtHoTen.Focus();
        }

        private void frmKhachHang_Click(object sender, EventArgs e)
        {
            lbThongBao.Text = "";
            MacDinh();
        }

        private void pThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
