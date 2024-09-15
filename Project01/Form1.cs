using Project01.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Project01
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Panel> listCacSPDon = new Dictionary<string, Panel>();

        public int sttSP = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSP();
        }

        private void LoadSP()
        {
            // Kết nối đến cơ sở dữ liệu
            QuanLySQL.KetNoi();

            try
            {
                // Truy vấn dữ liệu sản phẩm
                string query = "SELECT * FROM SanPham WHERE Enable = 1";
                DataTable sanPham = QuanLySQL.XuatDLTuSQL(query);

                foreach (DataRow row in sanPham.Rows)
                {
                    // Tạo một panel mới cho mỗi sản phẩm
                    Panel pThuocTinhHH = new Panel
                    {
                        BackColor = SystemColors.ControlLightLight,
                        Size = new Size(300, 100),
                        Margin = new Padding(15, 15, 0, 0)
                    };

                    VeBoGocPanel.BoGocPanel(pThuocTinhHH, 30);

                    // Tạo và cấu hình PictureBox
                    PictureBox pbAnhSP = new PictureBox
                    {
                        Size = new Size(60, 60),
                        Location = new Point(20, 20),
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        ImageLocation = row["HinhAnhURL"].ToString() // Đặt URL ảnh
                    };
                    pbAnhSP.Click += (s, e) => Panels_Click(pThuocTinhHH, e);  // Đăng ký sự kiện cho PictureBox

                    // Tạo và cấu hình các Label
                    Label lbTenSP = new Label
                    {
                        Font = new Font("Times New Roman", 12.75F),
                        Location = new Point(90, 20),
                        Size = new Size(181, 19),
                        MaximumSize = new Size(200, 40),
                        Text = row["Ten"].ToString()
                    };
                    lbTenSP.Click += (s, e) => Panels_Click(pThuocTinhHH, e);  // Đăng ký sự kiện cho Label tên sản phẩm

                    Label lbGiaSP = new Label
                    {
                        Font = new Font("Times New Roman", 12.75F),
                        Location = new Point(90, 60),
                        MaximumSize = new Size(110, 20),
                        Text = string.Format("{0:N0}", Convert.ToDecimal(row["GiaBan"])) // Định dạng giá với dấu phân cách nghìn
                    };
                    lbGiaSP.Click += (s, e) => Panels_Click(pThuocTinhHH, e);  // Đăng ký sự kiện cho Label giá

                    Label lbSoLuongSP = new Label
                    {
                        Font = new Font("Times New Roman", 12.75F),
                        Location = new Point(200, 60),
                        MaximumSize = new Size(70, 20),
                        Text = row["Ton"].ToString()
                    };
                    lbSoLuongSP.Click += (s, e) => Panels_Click(pThuocTinhHH, e);  // Đăng ký sự kiện cho Label số lượng

                    // Thêm các điều khiển vào panel
                    pThuocTinhHH.Controls.Add(pbAnhSP);
                    pThuocTinhHH.Controls.Add(lbTenSP);
                    pThuocTinhHH.Controls.Add(lbGiaSP);
                    pThuocTinhHH.Controls.Add(lbSoLuongSP);

                    // Gán tag cho panel để nhận diện khi nhấp chuột
                    pThuocTinhHH.Tag = row["ID"].ToString();

                    // Đăng ký sự kiện nhấp chuột cho panel
                    pThuocTinhHH.Click += new EventHandler(Panels_Click);

                    // Thêm panel vào FlowLayoutPanel
                    flpSanPham.Controls.Add(pThuocTinhHH);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải sản phẩm: " + ex.Message);
            }
            finally
            {
                // Ngắt kết nối cơ sở dữ liệu
                QuanLySQL.NgatKetNoi();
            }
        }

        private void Panels_Click(object sender, EventArgs e)
        {
            if (sender is Panel panel)
            {
                // Lấy ID sản phẩm từ tag
                string idSanPham = panel.Tag.ToString();

                // Tạo hoặc cập nhật
                TaoOrCapNhatSPDon(idSanPham);
            }
        }

        private void TaoOrCapNhatSPDon(string ID)
        {
            // Kết nối đến cơ sở dữ liệu
            QuanLySQL.KetNoi();

            try
            {
                string query = $"SELECT * FROM SanPham WHERE ID = {ID}";
                DataTable dataTable = QuanLySQL.XuatDLTuSQL(query);

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];

                    // Kiểm tra nếu đã có panel chi tiết cho sản phẩm đó
                    if (listCacSPDon.ContainsKey(ID))
                    {
                        // Nếu có, tăng số lượng mua thêm 1
                        Panel existingPanel = listCacSPDon[ID]; // Panel chứa các controls
                        TextBox txtSoLuong1SPDon = existingPanel.Controls.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "txtSoLuong1SPDon"); // Tìm TextBox

                        if (txtSoLuong1SPDon != null)
                        {
                            int soLuongMua = int.Parse(txtSoLuong1SPDon.Text);
                            txtSoLuong1SPDon.Text = (soLuongMua + 1).ToString();
                            CapNhatGia(existingPanel);
                        }
                    }
                    else
                    {
                        Panel pThuocTinhHHDon = new Panel
                        {
                            Size = new Size(725, 91),
                            BorderStyle = BorderStyle.None,
                            BackColor = Color.White,
                            Margin = new Padding(0, 0, 0, 10),
                        };

                        VeBoGocPanel.BoGocPanel(pThuocTinhHHDon, 30);

                        Label lbSttSP = new Label
                        {
                            AutoSize = true,
                            Name = "lbSttSP",
                            Text = sttSP.ToString(),
                            Location = new Point(13, 13)
                        };

                        PictureBox pbXoaSPDon = new PictureBox
                        {
                            Name = "pbXoaSPDon",
                            Size = new Size(20, 20),
                            Location = new Point(41, 13),
                            Image = Image.FromFile(@"C:\Users\Tienk\Downloads\trash.png"),
                            SizeMode = PictureBoxSizeMode.StretchImage
                        };

                        Label lbTenSPDon = new Label
                        {
                            Location = new Point(247, 13),
                            Size = new Size(300, 30),
                            Text = row["Ten"].ToString()
                        };

                        Label lbTongGia1SPDon = new Label
                        {
                            Name = "lbTongGia1SPDon",
                            Location = new Point(579, 49),
                            Text = string.Format("{0:N0}", Convert.ToDecimal(row["GiaBan"])) // Định dạng giá với dấu phân cách nghìn
                        };

                        TextBox txtSoLuong1SPDon = new TextBox
                        {
                            Name = "txtSoLuong1SPDon",
                            Text = "1",
                            Size = new Size(76, 29),
                            Location = new Point(125, 46),
                        };

                        TextBox txtGia1SPDon = new TextBox
                        {
                            Name = "txtGia1SP",
                            Location = new Point(352, 46),
                            Size = new Size(100, 30),
                            AutoSize = true,
                            Text = row["GiaBan"].ToString()
                        };

                        Label lbMaSPDon = new Label
                        {
                            AutoSize = true,
                            Name = "lbMaSPDon",
                            Text = "SP" + row["ID"].ToString().PadLeft(3, '0'),
                            Size = new Size(59, 21),
                            Location = new Point(122, 13)
                        };

                        // Đăng ký sự kiện khi giá thay đổi
                        txtGia1SPDon.TextChanged += new EventHandler(txtGia1SPDon_TextChanged);
                        txtSoLuong1SPDon.TextChanged += new EventHandler(txtSoLuong1SPDon_TextChanged);
                        pbXoaSPDon.Click += PbXoaSPDon_Click;

                        // Thêm các điều khiển vào panel chi tiết
                        pThuocTinhHHDon.Controls.Add(lbSttSP);
                        pThuocTinhHHDon.Controls.Add(pbXoaSPDon);
                        pThuocTinhHHDon.Controls.Add(lbTenSPDon);
                        pThuocTinhHHDon.Controls.Add(lbTongGia1SPDon);
                        pThuocTinhHHDon.Controls.Add(txtSoLuong1SPDon);
                        pThuocTinhHHDon.Controls.Add(txtGia1SPDon);
                        pThuocTinhHHDon.Controls.Add(lbMaSPDon);


                        sttSP++;

                        // Lưu trữ panel chi tiết vào từ điển
                        listCacSPDon[ID] = pThuocTinhHHDon;

                        // Hiển thị panel chi tiết trong form
                        flptest.Controls.Add(pThuocTinhHHDon);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin sản phẩm.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị chi tiết sản phẩm: " + ex.Message);
            }
            finally
            {
                // Ngắt kết nối cơ sở dữ liệu
                QuanLySQL.NgatKetNoi();
            }
        }

        private void PbXoaSPDon_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pbXoaSPDon)
            {
                
                // Tìm panel chứa PictureBox
                Panel panel = pbXoaSPDon.Parent as Panel;

                if (panel != null)
                {
                    // Lấy ID sản phẩm từ tag của panel
                    string idSanPham = panel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == "lbMaSPDon")?.Text.Substring(2).TrimStart('0');

                    if (!string.IsNullOrEmpty(idSanPham) && listCacSPDon.ContainsKey(idSanPham))
                    {
                        // Xóa panel khỏi FlowLayoutPanel và từ điển
                        flptest.Controls.Remove(panel);
                        listCacSPDon.Remove(idSanPham);

                        CapNhatStt();
                    }
                }
            }
        }

        private void txtSoLuong1SPDon_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox txtSoLuong1SPDon)
            {
                // Lấy panel chứa TextBox
                Panel panel = txtSoLuong1SPDon.Parent as Panel;

                if (panel != null)
                {
                    // Cập nhật lại tổng giá khi số lượng thay đổi
                    CapNhatGia(panel);
                }
            }
        }


        private void txtGia1SPDon_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox txtGia1SPDon)
            {
                // Lấy panel chứa TextBox
                Panel panel = txtGia1SPDon.Parent as Panel;

                if (panel != null)
                {
                    // Cập nhật lại tổng giá khi giá thay đổi
                    CapNhatGia(panel);
                }
            }
        }

        private void CapNhatGia(Panel panel)
        {
            // Lấy TextBox số lượng và giá từ panel
            TextBox txtSoLuong1SPDon = panel.Controls.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "txtSoLuong1SPDon");
            TextBox txtGia1SPDon = panel.Controls.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "txtGia1SP");

            // Lấy nhãn để hiển thị tổng giá
            Label lbTongGia1SPDon = panel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == "lbTongGia1SPDon");

            // Biến lưu số lượng và giá
            int soLuong = 0;
            decimal giaBan = 0;

            // Thử lấy số lượng từ TextBox, nếu không thể thì đặt soLuong = 0
            if (!int.TryParse(txtSoLuong1SPDon.Text, out soLuong))
            {
                soLuong = 0;
            }

            // Thử lấy giá từ TextBox, nếu không thể thì đặt giaBan = 0
            if (!decimal.TryParse(txtGia1SPDon.Text, out giaBan))
            {
                giaBan = 0;
            }

            // Tính tổng giá
            decimal tongGia = soLuong * giaBan;

            // Cập nhật tổng giá vào nhãn
            lbTongGia1SPDon.Text = string.Format("{0:N0}", tongGia);
        }

        private void CapNhatStt()
        {
            // Lặp qua các panel trong FlowLayoutPanel
            int stt = 1;
            foreach (Panel panel in flptest.Controls.OfType<Panel>())
            {
                Label lbSttSP = panel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == "lbSttSP");
                if (lbSttSP != null)
                {
                    lbSttSP.Text = stt.ToString();
                }
                stt++;
            }

            // Cập nhật số thứ tự cho biến sttSP
            sttSP = stt;
        }

    }
}
