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
    public partial class ucNhapHang : UserControl
    {
        public ucNhapHang()
        {
            InitializeComponent();
        }

        private void ucNhapHang_Load(object sender, EventArgs e)
        {
            VeBoGocPanel.BoGocPanel(pHang, 30);
            VeBoGocPanel.BoGocPanel(flpHoaDon, 30);
            VeBoGocPanel.BoGocPanel(flpInfoPhieu, 30);


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pHoaDon_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void flpInfoPhieu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pbBack_Click(object sender, EventArgs e)
        {
            Form mainForm = this.FindForm();
            if (mainForm is frmHome frmHome)
            {
                ucPhieuNhapHang ucPhieuNhapHang = new ucPhieuNhapHang();
                frmHome.AddControlsToPanel(ucPhieuNhapHang);
            }
        }
    }
}
