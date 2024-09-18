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
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void frmHome_Load(object sender, EventArgs e)
        {
            QuanLySQL.KetNoi();
        }

        public void AddControlsToPanel(Control c)
        {
            c.Dock = DockStyle.Fill;
            pHome.Controls.Clear();
            pHome.Controls.Add(c);
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            ucChinh ucChinh = new ucChinh();
            AddControlsToPanel(ucChinh);
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            QuanLySQL.NgatKetNoi();
            Application.Exit(); 
        }

        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            ucHangHoa ucHang = new ucHangHoa();
            AddControlsToPanel(ucHang);
        }

        
        private void btnHome_Click(object sender, EventArgs e)
        {
            ucHome ucHome = new ucHome();
            AddControlsToPanel(ucHome);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            ucBillInfo ucBillInfo = new ucBillInfo();
            AddControlsToPanel(ucBillInfo);
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            ucTaiKhoan ucTK = new ucTaiKhoan();
            AddControlsToPanel(ucTK);
        }
    }
}
