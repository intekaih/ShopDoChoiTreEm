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
    public partial class Form1 : Form
    {
        private TabControl tabControl1;
        private Panel panelNoiDung;

        public Form1()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            // Tạo và cấu hình TabControl
            tabControl1 = new TabControl();
            tabControl1.Location = new Point(30, 10); // Vị trí của TabControl
            tabControl1.Size = new Size(200, 400); // Kích thước của TabControl
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            // Tạo các TabPage
            TabPage tabPage1 = new TabPage("Tab 1");
            TabPage tabPage2 = new TabPage("Tab 2");
            tabControl1.TabPages.Add(tabPage1);
            tabControl1.TabPages.Add(tabPage2);

            // Tạo và cấu hình Panel để hiển thị nội dung
            panelNoiDung = new Panel();
            panelNoiDung.Location = new Point(220, 10); // Vị trí của Panel
            panelNoiDung.Size = new Size(400, 400);
            panelNoiDung.BackColor = Color.Pink;  // Kích thước của Panel

            // Thêm TabControl và Panel vào Form
            this.Controls.Add(tabControl1);
            this.Controls.Add(panelNoiDung);

            // Hiển thị nội dung của tab đầu tiên
            HienThiNoiDungTab(0);
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hiển thị nội dung tab tương ứng trong panel
            HienThiNoiDungTab(tabControl1.SelectedIndex);
        }

        private void HienThiNoiDungTab(int tabIndex)
        {
            panelNoiDung.Controls.Clear(); // Xóa nội dung cũ

            Label lblNoiDung = new Label();
            lblNoiDung.Dock = DockStyle.Fill;
            lblNoiDung.TextAlign = ContentAlignment.MiddleCenter;
            lblNoiDung.Font = new Font("Times New Roman", 14);

            switch (tabIndex)
            {
                case 0:
                    lblNoiDung.Text = "Nội dung của Tab 1";
                    break;
                case 1:
                    lblNoiDung.Text = "Nội dung của Tab 2";
                    break;
            }

            panelNoiDung.Controls.Add(lblNoiDung);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, EventArgs e)
        {

        }
    }
}
