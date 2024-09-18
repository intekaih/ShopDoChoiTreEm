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
    public partial class ucBillInfo : UserControl
    {
        public ucBillInfo()
        {
            InitializeComponent();
        }



        private void ucBillInfo_Load(object sender, EventArgs e)
        {
            VeBoGoc();
            QuanLySQL.KetNoi();
            LoadDTToDGV();

        }

        private void VeBoGoc()
        {
            VeBoGocPanel.BoGocPanel(flpThemDon, 10);
            VeBoGocPanel.BoGocPanel(flpTrangThaiTT, 30);
            VeBoGocPanel.BoGocPanel(flpKhachHang, 30);
            VeBoGocPanel.BoGocPanel(flpThoiGian, 30);
            VeBoGocPanel.BoGocPanel(pTimKiemHD, 40);
        }

        private void LoadDTToDGV()
        {
            string query = "select * from HoaDon where enable = 1";
            DataTable dt = QuanLySQL.XuatDLTuSQL(query);

            dgvBillInfo.DataSource = dt;
        }

        private void TEsst()
        {
            if (dgvBillInfo.SelectedRows.Count > 0)
            {
                DataGridViewRow rowSelect = dgvBillInfo.SelectedRows[0];

                string maHoaDon = rowSelect.Cells["MaHoaDon"].Value.ToString();
                string query = $"SELECT * FROM HoaDon WHERE ID = '{maHoaDon}'";

                DataTable dataTable = QuanLySQL.XuatDLTuSQL(query);

                if (dataTable.Rows.Count > 0)
                {
                    // Lấy dữ liệu từ SQL
                    DataRow row = dataTable.Rows[0];

                    MessageBox.Show(row["ID"].ToString());
                }

            }
        }
        private void flpThemDon_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
