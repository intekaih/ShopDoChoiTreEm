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
            VeBoGocPanel.BoGocPanel(flpThemDon, 10);
            VeBoGocPanel.BoGocPanel(flpTrangThaiTT, 30);
            VeBoGocPanel.BoGocPanel(flpKhachHang, 30);
            VeBoGocPanel.BoGocPanel(flpThoiGian, 30);
            VeBoGocPanel.BoGocPanel(pTimKiemHD, 40);

        }

        private void flpThemDon_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
