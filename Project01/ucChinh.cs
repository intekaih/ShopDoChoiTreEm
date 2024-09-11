using Project01.Class;
using Project01.Login;
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
    public partial class ucChinh : UserControl
    {
        public ucChinh()
        {
            InitializeComponent();
        }

        // Biến toàn cục hoặc tĩnh để lưu trữ giá trị
        private int sttSP = 1;
        

        private void ucChinh_Load(object sender, EventArgs e)
        {
            VeBoGoc();
            dtpNgayLapHD.Value = DateTime.Now;
        }

        private void VeBoGoc()
        {
            VeBoGocPanel.BoGocPanel(pHang, 30);
            VeBoGocPanel.BoGocPanel(pBill, 50);
            VeBoGocPanel.BoGocPanel(pHoaDon, 30);
            VeBoGocPanel.BoGocPanel(pInfoKhach, 30);
        }

        private void UpdateTongGiaHHDon()
        {
            // Khởi tạo biến tổng
            decimal tongGia = 0;

            // Lặp qua tất cả các control trong FlowLayoutPanel
            foreach (Control control in flpBangHoaDon.Controls)
            {
                // Kiểm tra nếu control là một Panel
                if (control is Panel panel)
                {
                    // Lặp qua tất cả các control trong Panel
                    foreach (Control innerControl in panel.Controls)
                    {
                        // Kiểm tra nếu control là Label với tên "lbTongGia1SPDon"
                        if (innerControl is Label label && label.Name == "lbTongGia1SPDon")
                        {
                            // Thêm giá trị của label vào tổng
                            if (decimal.TryParse(label.Text, out decimal gia))
                            {
                                tongGia += gia;
                            }
                        }
                    }
                }
            }

            // Cập nhật giá trị của lbTongGiaHHDon
            lbTongGiaHHDon.Text = tongGia.ToString("F0"); // Định dạng với 2 chữ số thập phân
        }


        private void HandlePanelSelection()
        {
            if (selectedPanel == null) return;

            // Tạo một Panel mới
            Panel newPanel = new Panel();
            newPanel.Size = new Size(725, 91); // Đặt kích thước cho panel mới
            newPanel.BorderStyle = BorderStyle.None; // Đặt kiểu viền cho panel mới
            newPanel.BackColor = Color.White; // Đặt màu nền cho panel mới
            newPanel.Margin = new Padding(0, 0, 0, 10); // Margin: trái, trên, phải, dưới
            VeBoGocPanel.BoGocPanel(newPanel, 30);


            // Lấy các giá trị từ panel được chọn
            Label lbGiaSP = selectedPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lbGiaSP" || l.Name == "lbGiaSP1");
            Label lbTenSP = selectedPanel.Controls.OfType<Label>().FirstOrDefault(l => l.Name == "lbTenSP" || l.Name == "lbTenSP1");

            // Thêm Label lbSttSP vào panel mới
            Label lbSttSP = new Label();
            lbSttSP.AutoSize = true;
            lbSttSP.Name = "lbSttSP"; // Đặt tên cho label
            lbSttSP.Text = sttSP.ToString(); // Nội dung của label là giá trị hiện tại của sttSP
            lbSttSP.Location = new Point(13, 13); // Vị trí của label bên trong panel
            newPanel.Controls.Add(lbSttSP); // Thêm label vào panel

            // Thêm lbTongGia1SPDon
            Label lbTongGia1SPDon = new Label();
            lbTongGia1SPDon.AutoSize = true;
            lbTongGia1SPDon.Name = "lbTongGia1SPDon"; // Đặt tên cho label
            lbTongGia1SPDon.Text = lbGiaSP?.Text; // Nội dung của label là giá trị hiện tại của lbGiaSP
            lbTongGia1SPDon.Location = new Point(579, 49); // Vị trí của label bên trong panel
            newPanel.Controls.Add(lbTongGia1SPDon); // Thêm label vào panel

            // Thêm lbTenSPDon và căn giữa trong panel mới
            Label lbTenSPDon = new Label();
            lbTenSPDon.AutoSize = true;
            lbTenSPDon.Name = "lbTenSPDon";
            lbTenSPDon.Text = lbTenSP?.Text; // Nội dung của label là giá trị hiện tại của lbTenSP
            lbTenSPDon.TextAlign = ContentAlignment.MiddleCenter; // Căn giữa văn bản
            lbTenSPDon.Location = new Point(247, 13); // Vị trí của label bên trong panel, căn giữa ngang
            newPanel.Controls.Add(lbTenSPDon);

            // Thêm TextBox txtSoLuong1SPDon
            TextBox txtSoLuong1SPDon = new TextBox();
            txtSoLuong1SPDon.Name = "txtSoLuong1SPDon";
            txtSoLuong1SPDon.Size = new Size(76, 29); // Đặt kích thước cho TextBox
            txtSoLuong1SPDon.Location = new Point(125, 46); // Đặt vị trí cho TextBox
            newPanel.Controls.Add(txtSoLuong1SPDon); // Thêm TextBox vào panel

            // Thêm TextBox txtGia1SP
            TextBox txtGia1SP = new TextBox();
            txtGia1SP.Name = "txtGia1SP";
            txtGia1SP.Size = new Size(100, 29); // Đặt kích thước cho TextBox
            txtGia1SP.Location = new Point(352, 46); // Đặt vị trí cho TextBox
            txtGia1SP.AutoSize = true; // Tự động điều chỉnh size
            newPanel.Controls.Add(txtGia1SP); // Thêm TextBox vào panel

            // Thêm Label lbMaSPDon
            Label lbMaSPDon = new Label();
            lbMaSPDon.AutoSize = true;
            lbMaSPDon.Name = "lbMaSPDon"; // Đặt tên cho label
            lbMaSPDon.Text = "Mã SP"; // Nội dung của label, bạn có thể thay đổi
            lbMaSPDon.Size = new Size(59, 21); // Đặt kích thước cho label
            lbMaSPDon.Location = new Point(122, 13); // Vị trí của label bên trong panel
            newPanel.Controls.Add(lbMaSPDon); // Thêm label vào panel

            // Thêm PictureBox pbXoaSPDon
            PictureBox pbXoaSPDon = new PictureBox();
            pbXoaSPDon.Name = "pbXoaSPDon"; // Đặt tên cho PictureBox
            pbXoaSPDon.Size = new Size(20, 20); // Đặt kích thước cho PictureBox
            pbXoaSPDon.Location = new Point(41, 13); // Đặt vị trí cho PictureBox
            pbXoaSPDon.Image = Image.FromFile(@"C:\Users\Tienk\Downloads\trash.png"); // Đặt hình ảnh
            pbXoaSPDon.SizeMode = PictureBoxSizeMode.StretchImage; // Căn chỉnh hình ảnh theo kích thước PictureBox
            newPanel.Controls.Add(pbXoaSPDon); // Thêm PictureBox vào panel





            // Thêm panel mới vào FlowLayoutPanel
            flpBangHoaDon.Controls.Add(newPanel);

            // Cập nhật tổng giá trị của lbTongGia1SPDon
            UpdateTongGiaHHDon();

            // Tăng giá trị sttSP cho lần thêm panel tiếp theo
            sttSP++;
        }

        private Panel selectedPanel = null; // Biến để lưu trữ panel được chọn

        private void pThuocTinhHH_Click(object sender, EventArgs e)
        {
            // Gán panel được chọn vào biến selectedPanel
            selectedPanel = pThuocTinhHH;

            // Thực hiện hành động khi chọn panel pThuocTinhHH
            HandlePanelSelection();
        }
        private void pThuocTinhHH1_Click(object sender, EventArgs e)
        {
            // Gán panel được chọn vào biến selectedPanel
            selectedPanel = pThuocTinhHH1;

            // Thực hiện hành động khi chọn panel pThuocTinhHH2
            HandlePanelSelection();
        }



       

        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        
    }
}
