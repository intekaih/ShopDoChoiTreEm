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
    public partial class ucPhieuNhapHang : UserControl
    {
        public ucPhieuNhapHang()
        {
            InitializeComponent();
        }

       
        //XuLy khi flpNhapHang được kích thì hiển thị ucNhapHang
        private void flpNhapHang_Click(object sender, EventArgs e)
        {

            // Tìm Form chính (frmHome) và gọi phương thức để thêm ucNhapHang
            Form mainForm = this.FindForm();
            if (mainForm is frmHome frmHome)
            {
                // Tạo đối tượng ucNhapHang và gọi phương thức AddControlsToPanel
                ucNhapHang ucNhapHang = new ucNhapHang();
                frmHome.AddControlsToPanel(ucNhapHang);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            flpNhapHang_Click(sender, e);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            flpNhapHang_Click(sender, e);

        }



        private void dgvDataHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ucPhieuNhapHang_Load(object sender, EventArgs e)
        {
            VeBoGocPanel.BoGocPanel(flpNhaCungCap, 30);
            VeBoGocPanel.BoGocPanel(flpTime, 30);
            VeBoGocPanel.BoGocPanel(flpTinhTrang, 30);
            VeBoGocPanel.BoGocPanel(pTimKiemPNH, 40);
            VeBoGocPanel.BoGocPanel(flpNhapHang, 10);

        }
    }
}
