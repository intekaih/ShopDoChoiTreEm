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
    public partial class ucTaiKhoan : UserControl
    {
        BindingSource bs = new BindingSource();

        public ucTaiKhoan()
        {
            InitializeComponent();
        }

        private void ucTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadTKToDGV();
        }

        private void LoadTKToDGV()
        {
            string query = "select * from TaiKhoan where enable = 1";
            DataTable dt = QuanLySQL.XuatDLTuSQL(query);

            //bs.DataSource = dt;

            dgvTaiKhoan.DataSource = dt;
        }
    }
}
