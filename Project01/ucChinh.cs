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
using System.Xml.Linq;

namespace Project01
{
    public partial class ucChinh : UserControl
    {
        private Dictionary<int, bool> hoaDonDaTao = new Dictionary<int, bool>();// Chứa các hóa đơn đã tạo
        private Dictionary<string, Panel> listCacSPDon = new Dictionary<string, Panel>();
        // Xác định 1 tab thì chỉ có 1 HoaDon (lưu HoaDon)
        private Dictionary<int, int> hoaDonDict = new Dictionary<int, int>();
        //thùng chứa các listCacSPDon với khóa là tabhoadonID 
        Dictionary<int, Dictionary<string, Panel>> dictListCacSPDon = new Dictionary<int, Dictionary<string, Panel>>();


        BindingSource KhachHang = new BindingSource();

        public int sttSP = 1;

        int tabHoaDonID = 0;
        int donHangID = 0;


        public ucChinh()
        {
            InitializeComponent();
            TaopTabCoDinh();

        }




        private void ucChinh_Load(object sender, EventArgs e)
        {
            QuanLySQL.KetNoi();
            LoadSPToflpSanPham(string.Empty);
            VeBoGoc();
            dtpNgayLapHD.Value = DateTime.Now;
            TongGiaSPHD();
            LoadKhachHangToCb();

        }

        private void VeBoGoc()
        {
            TrangTri.BoGocPanel(pLocSP, 30);
            TrangTri.BoGocPanel(pBill, 50);
            TrangTri.BoGocPanel(pHoaDon, 30);
            TrangTri.BoGocPanel(pInfoKhach, 30);
            TrangTri.BoGocPanel(pTimKiem, 40);
            TrangTri.BoGocPanel(pBtnn, 50);
            TrangTri.BoGocPanel(pTinhTien, 30);
            TrangTri.BoGocPanel(pHuyDon, 20);
            TrangTri.BoGocPanel(pLuuDon, 20);
            TrangTri.BoGocPanel(pLuuIn, 20);

        }
        //XyLy tinh tien
        private void txtThueVATHD_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtThueVATHD.Text, out decimal value))
            {
                if (value < 0) value = 0;
                if (value > 100) value = 00;
                txtThueVATHD.Text = value.ToString();
            }
            else
                txtThueVATHD.Text = "0";
            TongThanhToan();
            txtThueVATHD.SelectionStart = txtThueVATHD.Text.Length;
        }

        private void lbTongTienHang_TextChanged(object sender, EventArgs e)
        {
            TongThanhToan();
        }

        private void txtPhiKhacHD_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtPhiKhacHD.Text, out decimal value))
            {
                if (value < 0) value = 0;
                txtPhiKhacHD.Text = value.ToString("N0");
            }
            else
                txtPhiKhacHD.Text = "0";
            TongThanhToan();
            txtPhiKhacHD.SelectionStart = txtPhiKhacHD.Text.Length;
        }

        private void txtGiamGiaHD_TextChanged(object sender, EventArgs e)
        {
            if (rdPhanTram.Checked == true)
            {
                if (decimal.TryParse(txtGiamGiaHD.Text, out decimal valuePhanTram))
                {
                    if (valuePhanTram < 0) valuePhanTram = 0;
                    if (valuePhanTram > 100) valuePhanTram = 00;
                    txtGiamGiaHD.Text = valuePhanTram.ToString();
                }
                else
                    txtThueVATHD.Text = "0";
            }
            else
            {
                if (decimal.TryParse(txtGiamGiaHD.Text, out decimal value))
                {
                    if (value < 0) value = 0;
                    txtGiamGiaHD.Text = value.ToString("N0");
                }
                else
                    txtGiamGiaHD.Text = "0";
            }
            TongThanhToan();
            txtGiamGiaHD.SelectionStart = txtGiamGiaHD.Text.Length;
        }

        private void rdVND_CheckedChanged(object sender, EventArgs e)
        {
            txtGiamGiaHD_TextChanged(sender, e);
        }

        private void rdPhanTram_CheckedChanged(object sender, EventArgs e)
        {
            txtGiamGiaHD_TextChanged(sender, e);
        }

        private void TongThanhToan()
        {
            decimal tongThanhToan = decimal.Parse(lbTongTienHang.Text);
            decimal phiKhac = decimal.Parse(txtPhiKhacHD.Text);
            decimal giamGia = 0;
            if (rdVND.Checked == true)
                giamGia = decimal.Parse(txtGiamGiaHD.Text);
            if (rdPhanTram.Checked == true)
                giamGia = tongThanhToan * decimal.Parse(txtGiamGiaHD.Text) / 100;
            decimal thueVAT = tongThanhToan * decimal.Parse(txtThueVATHD.Text) / 100;

            decimal thanhTien = tongThanhToan + phiKhac + thueVAT - giamGia;

            lbTongThanhToan.Text = thanhTien.ToString("N0");
        }


        private void TongSLSPHD()
        {
            TongGiaSPHD();
            decimal tongSL = 0;

            foreach (Control control in flpBangHoaDon.Controls)
            {
                if (control is Panel panel)
                {
                    foreach (Control controlItem in panel.Controls)
                    {
                        if (controlItem is TextBox textbox && textbox.Name == "txtSoLuong1SPDon")
                        {
                            if (decimal.TryParse(textbox.Text, out decimal sL))
                            {
                                tongSL += sL;
                            }
                        }
                    }
                }
            }
            lbTongSLSpHd.Text = "Tổng số lượng Sản phẩm: " + tongSL.ToString();
        }
        private void TongGiaSPHD()
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
            lbTongTienHang.Text = tongGia.ToString("N0"); // Định dạng với 2 chữ số thập phân
        }

        //Xy ly bang KhachHang
        private void LoadKhachHangToCb()
        {
            string queryKh = "select * from KhachHang where enable = 1";
            DataTable dataTable = QuanLySQL.XuatDLTuSQL(queryKh);

            cbHoTenKH.DataSource = dataTable;
            cbHoTenKH.DisplayMember = "HoTen";
            cbHoTenKH.ValueMember = "ID";
        }

        private void cbHoTenKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHoTenKH.SelectedItem != null)
            {
                DataRowView selectedRow = cbHoTenKH.SelectedItem as DataRowView;

                if (selectedRow != null)
                {
                    string id = selectedRow["ID"].ToString();
                    string query = $"SELECT * FROM KhachHang WHERE ID = '{id}'";
                    DataTable dataTable = QuanLySQL.XuatDLTuSQL(query);

                    if (dataTable.Rows.Count > 0)
                    {
                        txtSDT.Text = dataTable.Rows[0]["DienThoai"].ToString();
                        txtDiaChi.Text = dataTable.Rows[0]["DiaChi"].ToString();
                        cbHoTenKH.Text = dataTable.Rows[0]["Hoten"].ToString();
                    }
                }
                txtF.Focus();
            }
        }

        private void cbHoTenKH_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbHoTenKH.Text))
            {
                txtSDT.Text = "";
                txtDiaChi.Text = "";
            }
        }

        public string QueryLoc(int? loaiID, int? dotuoiID, int? hangID, int? xuatxuID, int? tonMin, int? tonKhong)
        {
            // Khởi tạo câu lệnh SELECT cơ bản
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM SanPham WHERE 1=1 and enable = 1");

            // Thêm điều kiện nếu loaiID có giá trị
            if (loaiID.HasValue)
            {
                queryBuilder.Append($" AND LoaiID = {loaiID.Value}");
            }

            // Thêm điều kiện nếu dotuoiID có giá trị
            if (dotuoiID.HasValue)
            {
                queryBuilder.Append($" AND dotuoiid = {dotuoiID.Value}");
            }

            // Thêm điều kiện nếu hangID có giá trị
            if (hangID.HasValue)
            {
                queryBuilder.Append($" AND HangID = {hangID.Value}");
            }

            // Thêm điều kiện nếu xuatxuID có giá trị
            if (xuatxuID.HasValue)
            {
                queryBuilder.Append($" AND XuatXuID = {xuatxuID.Value}");
            }

            // Thêm điều kiện nếu tonMin có giá trị
            if (tonMin.HasValue)
            {
                queryBuilder.Append($" AND Ton < {tonMin.Value}");
            }

            // Thêm điều kiện nếu tonMin có giá trị
            if (tonKhong.HasValue)
            {
                queryBuilder.Append($" AND Ton = {tonKhong.Value}");
            }

            // Trả về chuỗi SQL đã được xây dựng
            return queryBuilder.ToString();
        }

        private void LoadSPToflpSanPham(string tuKhoa)
        {
            cbTinhTrangHD.SelectedItem = "Đã Thanh Toán";
            flpSanPham.Controls.Clear();
            try
            {
                // Truy vấn dữ liệu sản phẩm
                string query = "SELECT * FROM SanPham WHERE Enable = 1";
                DataTable sanPham = QuanLySQL.XuatDLTuSQL(query);

                foreach (DataRow row in sanPham.Rows)
                {
                    string productName = row["Ten"].ToString().ToLower(); // Lấy tên sản phẩm

                    // Kiểm tra xem tên sản phẩm có chứa từ khóa tìm kiếm không
                    if (string.IsNullOrEmpty(tuKhoa) || productName.Contains(tuKhoa))
                    {
                        // Tạo một panel mới cho mỗi sản phẩm
                        Panel pThuocTinhHH = new Panel
                        {
                            BackColor = SystemColors.ControlLightLight,
                            Size = new Size(267, 100),
                            Margin = new Padding(7, 15, 7, 0),
                        };

                        TrangTri.BoGocPanel(pThuocTinhHH, 30);
                        TrangTri.ThemHieuUngHighlight(pThuocTinhHH, 33, 2);

                        // Tạo và cấu hình PictureBox
                        PictureBox pbAnhSP = new PictureBox
                        {
                            Size = new Size(60, 60),
                            Location = new Point(20, 20),
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            ImageLocation = row["HinhAnhURL"].ToString() // Đặt URL ảnh
                        };
                        pbAnhSP.Click += (s, e) => pThuocTinhHH_Kich(pThuocTinhHH, e); // Đăng ký sự kiện cho PictureBox

                        // Tạo và cấu hình các Label
                        Label lbTenSP = new Label
                        {
                            Font = new Font("Times New Roman", 12.75F),
                            Location = new Point(85, 20),
                            Size = new Size(178, 19),
                            MaximumSize = new Size(178, 40),
                            ForeColor = ColorTranslator.FromHtml("#2c3f50"),
                            Text = row["Ten"].ToString()
                        };
                        lbTenSP.Click += (s, e) => pThuocTinhHH_Kich(pThuocTinhHH, e); // Đăng ký sự kiện cho Label tên sản phẩm

                        Label lbGiaSP = new Label
                        {
                            Font = new Font("Times New Roman", 12.75F),
                            Location = new Point(85, 60),
                            MaximumSize = new Size(110, 20),
                            ForeColor = ColorTranslator.FromHtml("#008fff"),
                            Text = string.Format("{0:N0}", Convert.ToDecimal(row["GiaBan"])) // Định dạng giá với dấu phân cách nghìn
                        };
                        lbGiaSP.Click += (s, e) => pThuocTinhHH_Kich(pThuocTinhHH, e); // Đăng ký sự kiện cho Label giá

                        Label lbSoLuongSP = new Label
                        {
                            Font = new Font("Times New Roman", 12.75F),
                            Location = new Point(200, 60),
                            MaximumSize = new Size(60, 20),
                            ForeColor = ColorTranslator.FromHtml("#ff5436"),
                            Text = row["Ton"].ToString()
                        };
                        lbSoLuongSP.Click += (s, e) => pThuocTinhHH_Kich(pThuocTinhHH, e); // Đăng ký sự kiện cho Label số lượng

                        // Thêm các điều khiển vào panel
                        pThuocTinhHH.Controls.Add(pbAnhSP);
                        pThuocTinhHH.Controls.Add(lbTenSP);
                        pThuocTinhHH.Controls.Add(lbGiaSP);
                        pThuocTinhHH.Controls.Add(lbSoLuongSP);

                        // Gán tag cho panel để nhận diện khi nhấp chuột
                        pThuocTinhHH.Tag = row["ID"].ToString();

                        // Đăng ký sự kiện nhấp chuột cho panel
                        pThuocTinhHH.Click += new EventHandler(pThuocTinhHH_Kich);

                        // Thêm panel vào FlowLayoutPanel
                        flpSanPham.Controls.Add(pThuocTinhHH);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải sản phẩm: " + ex.Message);
            }
        }



        private void pThuocTinhHH_Kich(object sender, EventArgs e)
        {
            if (sender is Panel panel)
            {
                string idSanPham = panel.Tag.ToString();

                // Lấy thông tin sản phẩm từ database
                DataTable dataTable = QuanLySQL.XuatDLTuSQL($"SELECT * FROM SanPham WHERE ID = {idSanPham}");
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    int soLuongTon = int.Parse(row["Ton"].ToString());

                    // Kiểm tra số lượng sản phẩm
                    if (soLuongTon <= 0)
                    {
                        MessageBox.Show("Sản phẩm đã hết hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Dừng hàm nếu sản phẩm đã hết hàng
                    }
                }

                // Tiến hành gọi hàm tạo hoặc cập nhật sản phẩm
                TaoOrCapNhatSPDon(idSanPham, donHangID, tabHoaDonID);

                TongGiaSPHD();
                TongSLSPHD();
                CapNhatStt();
            }
        }


        private void TaoOrCapNhatSPDon(string sanphamID, int donHangID, int tabHoaDonID)
        {
            try
            {

                DataTable dataTable = QuanLySQL.XuatDLTuSQL($"SELECT * FROM SanPham WHERE ID = {sanphamID}");

                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];


                    if (listCacSPDon.ContainsKey(sanphamID))
                    {

                        Panel existingPanel = listCacSPDon[sanphamID];
                        TextBox txtSoLuong1SPDon = existingPanel.Controls.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "txtSoLuong1SPDon"); // Tìm TextBox

                        if (txtSoLuong1SPDon != null)
                        {
                            int soLuongMua = int.Parse(txtSoLuong1SPDon.Text);
                            txtSoLuong1SPDon.Text = (soLuongMua + 1).ToString();

                        }
                    }
                    else
                    {
                        decimal giaGiam = decimal.Parse(row["GiaBan"].ToString());
                        string query = $"insert into ChiTietHoaDon (TabHoaDonID, DonHangID, SanPhamID, SoLuong, GiaGiam, thanhtien) values " +
                            $"({tabHoaDonID}, {donHangID}, {sanphamID},1, {giaGiam}, 0)";
                        QuanLySQL.NhapDLVaoSQL(query);
                    }
                    LoadSPToflpBangHoaDon(donHangID, tabHoaDonID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị chi tiết sản phẩm: " + ex.Message);
            }
        }


        private void LoadSPToflpBangHoaDon(int donHangID, int tabHoaDonID)
        {

            try
            {
                // Kiểm tra xem từ điển listCacSPDon đã được tạo chưa
                if (!dictListCacSPDon.ContainsKey(tabHoaDonID))
                {
                    dictListCacSPDon[tabHoaDonID] = new Dictionary<string, Panel>();
                }

                listCacSPDon = dictListCacSPDon[tabHoaDonID];

                string query = $"SELECT ct.SanPhamID, sp.Ten, ct.SoLuong, ct.GiaGiam, ct.ThanhTien, sp.Ton " +
                               $"FROM ChiTietHoaDon ct " +
                               $"JOIN SanPham sp ON ct.SanPhamID = sp.ID " +
                               $"WHERE ct.DonHangID = {{0}} AND ct.TabHoaDonID = {{1}} ";
                string queryUd = $"UPDATE ChiTietHoaDon SET ThanhTien = (GiaGiam * SoLuong) WHERE DonHangID = {{0}} AND TabHoaDonID = {{1}};";

                QuanLySQL.NhapDLVaoSQL(string.Format(queryUd, donHangID, tabHoaDonID));
                DataTable dataTable = QuanLySQL.XuatDLTuSQL(string.Format(query, donHangID, tabHoaDonID));

                foreach (DataRow row in dataTable.Rows)
                {
                    string sanPhamID = row["SanPhamID"].ToString();
                    int soLuongTon = int.Parse(row["Ton"].ToString());
                    int soLuongMua = int.Parse(row["SoLuong"].ToString());

                    if (listCacSPDon.ContainsKey(sanPhamID))
                    {
                        Panel existingPanel = listCacSPDon[sanPhamID];
                        TextBox txtSoLuong1SPDon = existingPanel.Controls.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "txtSoLuong1SPDon");
                        TextBox txtGia1SPDon = existingPanel.Controls.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "txtGia1SPDon");
                        Label lbTongGia1SPDon = existingPanel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == "lbTongGia1SPDon");

                        if (txtSoLuong1SPDon != null && txtGia1SPDon != null && lbTongGia1SPDon != null)
                        {
                            if (soLuongMua > soLuongTon)
                            {
                                MessageBox.Show($"Số lượng sản phẩm chỉ còn {soLuongTon}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtSoLuong1SPDon.Text = soLuongTon.ToString();
                            }
                            else
                            {
                                txtSoLuong1SPDon.Text = soLuongMua.ToString();
                            }

                            txtGia1SPDon.Text = Convert.ToDecimal(row["GiaGiam"]).ToString("N0");
                            lbTongGia1SPDon.Text = Convert.ToDecimal(row["ThanhTien"]).ToString("N0");
                        }
                    }
                    else
                    {
                        Panel pThuocTinhHHDon = new Panel
                        {
                            Size = new Size(725, 91),
                            BorderStyle = BorderStyle.None,
                            BackColor = Color.White,
                            Margin = new Padding(0, 3, 0, 3),
                        };

                        TrangTri.BoGocPanel(pThuocTinhHHDon, 30);
                        TrangTri.ThemHieuUngHighlight(pThuocTinhHHDon, 35, 2);

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
                            Size = new Size(30, 30),
                            Location = new Point(41, 9),
                            Image = Image.FromFile(@"F:\Học Tập\ShopDoChoiTreEm\Picture\trash (1).png"),
                            SizeMode = PictureBoxSizeMode.CenterImage,
                            Cursor = Cursors.Hand,
                        };

                        TrangTri.ThemHieuUngHighlight(pbXoaSPDon, 1, 1);

                        Label lbTenSPDon = new Label
                        {
                            Name = "lbTenSPDon",
                            Location = new Point(247, 13),
                            Size = new Size(300, 30),
                            Text = row["Ten"].ToString()
                        };

                        Label lbTongGia1SPDon = new Label
                        {
                            Name = "lbTongGia1SPDon",
                            Location = new Point(579, 49),
                            Text = string.Format("{0:N0}", Convert.ToDecimal(row["ThanhTien"]))
                        };

                        TextBox txtSoLuong1SPDon = new TextBox
                        {
                            Name = "txtSoLuong1SPDon",
                            Text = row["SoLuong"].ToString(),
                            Size = new Size(76, 29),
                            Location = new Point(125, 46),
                            TextAlign = HorizontalAlignment.Center
                        };

                        TextBox txtGia1SPDon = new TextBox
                        {
                            Name = "txtGia1SPDon",
                            Location = new Point(352, 46),
                            Size = new Size(100, 30),
                            AutoSize = true,
                            Text = Convert.ToDecimal(row["GiaGiam"]).ToString("N0"),
                            TextAlign = HorizontalAlignment.Center
                        };

                        Label lbMaSPDon = new Label
                        {
                            AutoSize = true,
                            Name = "lbMaSPDon",
                            Text = "SP" + sanPhamID.PadLeft(3, '0'),
                            Size = new Size(59, 21),
                            Location = new Point(122, 13)
                        };

                        // Đăng ký sự kiện khi giá hoặc số lượng thay đổi
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

                        listCacSPDon[sanPhamID] = pThuocTinhHHDon;

                        flpBangHoaDon.Controls.Add(pThuocTinhHHDon);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị chi tiết sản phẩm: " + ex.Message);
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
                        // Xóa dữ liệu khỏi cơ sở dữ liệu SQL
                        try
                        {
                            // Tạo câu lệnh SQL để xóa bản ghi
                            string query = $"DELETE FROM ChiTietHoaDon WHERE SanPhamID = {{0}} AND DonHangID = {{1}} AND TabHoaDonID = {{2}}";

                            // Thực thi câu lệnh SQL với giá trị tham số
                            QuanLySQL.NhapDLVaoSQL(string.Format(query, idSanPham, donHangID, tabHoaDonID));

                            // Xóa panel khỏi FlowLayoutPanel và từ điển
                            flpBangHoaDon.Controls.Remove(panel);
                            listCacSPDon.Remove(idSanPham);

                            // Cập nhật lại thông tin
                            CapNhatStt();
                            TongGiaSPHD();
                            TongSLSPHD();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
                        }
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
                    string idSanPhamStr = panel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == "lbMaSPDon")?.Text.Substring(2).TrimStart('0');

                    if (int.TryParse(idSanPhamStr, out int idSanPham))
                    {
                        // Lấy giá từ TextBox
                        if (decimal.TryParse(txtSoLuong1SPDon.Text, out decimal soLuong))
                        {
                            // Tạo câu lệnh SQL với tham số
                            string query = $"UPDATE ChiTietHoaDon SET SoLuong = {{0}} WHERE SanPhamID = {{1}} AND DonHangID = {{2}} AND TabHoaDonID = {{3}}";

                            // Thực thi câu lệnh SQL với giá trị tham số
                            QuanLySQL.NhapDLVaoSQL(string.Format(query, soLuong, idSanPham, donHangID, tabHoaDonID));


                            LoadSPToflpBangHoaDon(donHangID, tabHoaDonID);
                        }
                        else
                        {
                            txtSoLuong1SPDon.Text = "0";
                        }
                    }
                }
                txtSoLuong1SPDon.SelectionStart = txtSoLuong1SPDon.Text.Length;
                TongSLSPHD();
                TongThanhToan();
            }

        }

        private void txtGia1SPDon_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox txtGia1SPDon)
            {
                Panel panel = txtGia1SPDon.Parent as Panel;

                if (panel != null)
                {
                    string idSanPhamStr = panel.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == "lbMaSPDon")?.Text.Substring(2).TrimStart('0');

                    if (int.TryParse(idSanPhamStr, out int idSanPham))
                    {
                        // Lấy giá từ TextBox
                        if (decimal.TryParse(txtGia1SPDon.Text, out decimal giaBan))
                        {
                            // Tạo câu lệnh SQL với tham số
                            string query = $"UPDATE ChiTietHoaDon SET GiaGiam = {{0}} WHERE SanPhamID = {{1}} AND DonHangID = {{2}} AND TabHoaDonID = {{3}}";

                            // Thực thi câu lệnh SQL với giá trị tham số
                            QuanLySQL.NhapDLVaoSQL(string.Format(query, giaBan, idSanPham, donHangID, tabHoaDonID));


                            LoadSPToflpBangHoaDon(donHangID, tabHoaDonID);
                        }
                        else
                        {
                            txtGia1SPDon.Text = "0";
                        }
                    }

                }
                txtGia1SPDon.SelectionStart = txtGia1SPDon.Text.Length;
            }

            TongThanhToan();
        }
        private void CapNhatStt()
        {
            // Lặp qua các panel trong FlowLayoutPanel
            int stt = 1;
            foreach (Panel panel in flpBangHoaDon.Controls.OfType<Panel>())
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

        private void pbExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void LoadDataChanged(object sender, EventArgs e)
        {
            LoadKhachHangToCb();
        }

        private void pbKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHang frmKhachHang = new frmKhachHang();
            frmKhachHang.DataChanged += LoadDataChanged;
            frmKhachHang.ShowDialog();
        }

        private void btnHuyDon_Click(object sender, EventArgs e)
        {
            // Xóa hóa đơn trong cơ sở dữ liệu
            XoaHoaDon();

            // Cập nhật giao diện
            ResetGiaoDien();

            XuLyXoaTab(tabHoaDonID);

            // Cập nhật vị trí của tất cả các pTab và nút thêm
            CapNhatViTriCacTab();
            CapNhatViTriNutThem();
        }

        private void XoaHoaDon()
        {
            string queryChiTiet = $"DELETE FROM ChiTietHoaDon WHERE TabHoaDonID = {tabHoaDonID} AND DonHangID = {donHangID}";
            string queryHoaDon = $"DELETE FROM HoaDon WHERE ID = {donHangID}";

            QuanLySQL.NhapDLVaoSQL(queryChiTiet);
            QuanLySQL.NhapDLVaoSQL(queryHoaDon);

            listCacSPDon.Clear();
            hoaDonDict.Remove(tabHoaDonID);
            dictListCacSPDon.Remove(tabHoaDonID);
        }

        private void ResetGiaoDien()
        {
            flpBangHoaDon.Controls.Clear();
            lbTongSLSpHd.Text = "";
            txtPhiKhacHD.Text = "0";
            txtGiamGiaHD.Text = "0";
            txtThueVATHD.Text = "0";
            txtGhiChuHoaDon.Text = "";
            CapNhatStt();
            TongGiaSPHD();
        }

        private void CapNhatViTriNutThem()
        {
            pbThem.Location = new Point(123 * hoaDonDaTao.Count + 15, 0);
        }

        private void XuLyXoaTab(int tabHoaDonID)
        {
            Panel tabToRemove = pTabControl.Controls.OfType<Panel>().FirstOrDefault(p => (int)p.Tag == tabHoaDonID);

            if (tabToRemove != null)
            {
                if (tabHoaDonID == 1)
                {
                    TaopTabCoDinh();
                    Panel newTab = pTabControl.Controls.OfType<Panel>().FirstOrDefault(p => (int)p.Tag == 1);
                    if (newTab != null)
                    {
                        pTab_Click(newTab, EventArgs.Empty);
                    }
                }
                else
                {
                    XoaTab(tabToRemove, tabHoaDonID);
                }
            }
        }


        private void XoaTab(Panel pTab, int index)
        {
            if (pTabControl.Controls.Contains(pTab))
            {
                pTabControl.Controls.Remove(pTab);
                hoaDonDaTao.Remove(index);
                CapNhatViTriCacTab();

                if (index == 1)
                {
                    TaopTabCoDinh();

                }
                else
                {
                    ChonTabTruoc(index);
                }
            }
        }

        private void ChonTabTruoc(int index)
        {
            if (index - 1 > 0)
            {
                Panel tabTruoc = pTabControl.Controls.OfType<Panel>().FirstOrDefault(p => (int)p.Tag == index - 1);
                if (tabTruoc != null)
                {
                    pTab_Click(tabTruoc, EventArgs.Empty);
                }
            }
        }

        private void CapNhatViTriCacTab()
        {
            int viTri = 0; // Vị trí bắt đầu
            foreach (var index in hoaDonDaTao.Keys.OrderBy(k => k))
            {
                Panel panel = pTabControl.Controls.OfType<Panel>().FirstOrDefault(p => (int)p.Tag == index);
                if (panel != null)
                {
                    panel.Location = new Point(123 * viTri + 10, 10);
                    viTri++;
                }
            }
            pbThem.Location = new Point(123 * viTri + 15, 0);
        }
        private void pTab_Click(object sender, EventArgs e)
        {
            MessageBox.Show(tabHoaDonID.ToString(), donHangID.ToString());
            flpBangHoaDon.Controls.Clear();
            ResetTabColors();

            Panel clickedPanel = sender as Panel;
            if (clickedPanel != null)
            {
                clickedPanel.BackColor = SystemColors.Control;
                tabHoaDonID = (int)clickedPanel.Tag;

                if (!hoaDonDict.ContainsKey(tabHoaDonID))
                {
                    TaoHoaDonSQL();
                }
                else
                {
                    donHangID = hoaDonDict[tabHoaDonID];
                    listCacSPDon = dictListCacSPDon[tabHoaDonID];
                    LoadProducts();
                }
            }
            TongGiaSPHD();
        }

        private void ResetTabColors()
        {
            foreach (Panel panel in pTabControl.Controls.OfType<Panel>())
            {
                panel.BackColor = Color.FromArgb(255, 224, 192);
            }
        }

        private void TaoHoaDonSQL()
        {
            dictListCacSPDon[tabHoaDonID] = new Dictionary<string, Panel>();
            string query = $"INSERT INTO HoaDon (TabHoaDonID) VALUES ({tabHoaDonID}); SELECT SCOPE_IDENTITY();";

            using (DataTable dataTable = QuanLySQL.XuatDLTuSQL(query))
            {
                donHangID = Convert.ToInt32(dataTable.Rows[0][0]);
            }

            hoaDonDict[tabHoaDonID] = donHangID;

            LoadSPToflpBangHoaDon(donHangID, tabHoaDonID);
        }

        private void LoadProducts()
        {
            foreach (var panel in listCacSPDon.Values)
            {
                flpBangHoaDon.Controls.Add(panel);
            }
        }

        private void TaopTabCoDinh()
        {
            if (!hoaDonDaTao.ContainsKey(1))
            {
                Panel pTab = new Panel
                {
                    Size = new Size(122, 58),
                    Location = new Point(10, 10),
                    BackColor = Color.FromArgb(255, 224, 192),
                    Tag = 1,
                };

                Label lbTab = new Label
                {
                    Size = new Size(93, 21),
                    Location = new Point(15, 10),
                    Text = "Hóa đơn 1",
                };

                lbTab.Click += (s, args) => pTab_Click(pTab, args);
                pTab.Controls.Add(lbTab);
                TrangTri.BoGocPanel(pTab, 25);
                pTab.Click += pTab_Click;

                pTabControl.Controls.Add(pTab);
                hoaDonDaTao[1] = false;

                pTab_Click(pTab, EventArgs.Empty);
            }
        }

        private void pbThem_Click(object sender, EventArgs e)
        {
            int newIndex = TimChiSoTruoc();
            if (newIndex > 7)
            {
                MessageBox.Show("Chương trình giới hạn 7 hóa đơn.");
                return;
            }

            Panel pTab = new Panel
            {
                Size = new Size(122, 58),
                BackColor = Color.FromArgb(255, 224, 192),
                Tag = newIndex,
            };

            Label lbTab = new Label
            {
                Size = new Size(93, 21),
                Location = new Point(15, 10),
                Text = "Hóa đơn " + newIndex,
            };

            lbTab.Click += (s, args) => pTab_Click(pTab, args);
            pTab.Controls.Add(lbTab);
            TrangTri.BoGocPanel(pTab, 25);
            pTab.Click += pTab_Click;

            pTabControl.Controls.Add(pTab);
            hoaDonDaTao[newIndex] = false;
            CapNhatViTriCacTab();
            pTab_Click(pTab, EventArgs.Empty);
        }

        private int TimChiSoTruoc()
        {
            int newIndex = 1;
            while (hoaDonDaTao.ContainsKey(newIndex))
            {
                newIndex++;
            }
            return newIndex;
        }

        private void LuuHoaDon()
        {
            int khachID = int.Parse(cbHoTenKH.SelectedValue.ToString());
            DateTime ngayLap = dtpNgayLapHD.Value;
            decimal tongTienHang = decimal.Parse(lbTongTienHang.Text);
            string trangThai = "Chờ Xử Lý";
            int nguoiBanID = 1;
            decimal giamGiaTien = rdVND.Checked ? decimal.Parse(txtGiamGiaHD.Text) : 0;
            decimal giamGiaPhanTram = rdPhanTram.Checked ? decimal.Parse(txtGiamGiaHD.Text) : 0;
            decimal thueVAT = decimal.Parse(txtThueVATHD.Text);
            decimal phiKhac = decimal.Parse(txtPhiKhacHD.Text);
            decimal tongThanhToan = decimal.Parse(lbTongThanhToan.Text);
            decimal khachTra = decimal.Parse(txtKhachTra.Text);
            string ghiChu = txtGhiChuHoaDon.Text;
            int thanhToanID = 3;

            if (rdTienMat.Checked) thanhToanID = 1;
            if (rdThe.Checked) thanhToanID = 2;

            string queryUpdateHoaDon = $"UPDATE HoaDon SET " +
                                         $"KhachID = {khachID}, " +
                                         $"NgayLap = '{ngayLap:yyyy-MM-dd HH:mm:ss}', " +
                                         $"TongTienHang = {tongTienHang}, " +
                                         $"TrangThai = N'{trangThai}', " +
                                         $"NguoiBanID = {nguoiBanID}, " +
                                         $"GiamGiaTien = {giamGiaTien}, " +
                                         $"GiamGiaPhanTram = {giamGiaPhanTram}, " +
                                         $"ThueVAT = {thueVAT}, " +
                                         $"PhiKhac = {phiKhac}, " +
                                         $"TongThanhToan = {tongThanhToan}, " +
                                         $"GhiChu = N'{ghiChu}', " +
                                         $"ThanhToanID = {thanhToanID}, " +
                                         $"KhachTra = {khachTra}, " +
                                         $"IsSaved = 1 " +
                                         $"WHERE ID = {donHangID};";

            // Thực hiện câu lệnh cập nhật
            QuanLySQL.NhapDLVaoSQL(queryUpdateHoaDon);
        }


        private void btnLuuDon_Click(object sender, EventArgs e)
        {
            LuuHoaDon();
            MessageBox.Show("Lưu hóa đơn thành công!");
        }

        private void btnLuuIn_Click(object sender, EventArgs e)
        {
            LuuHoaDon();

            string trangThai = cbTinhTrangHD.SelectedItem.ToString();

            string query = $"update hoadon set " +
                $"trangthai = N'{trangThai}'," +
                $"enable = 1" +
                $"where id = {donHangID}";
            QuanLySQL.NhapDLVaoSQL(query);

            string queryCTHD = $"update ChiTietHoaDon set Enable = 1 where DonHangID = {donHangID}";
            QuanLySQL.NhapDLVaoSQL(queryCTHD);

            string queryUpdateSLSP = $"UPDATE SanPham SET Ton = Ton - ct.SoLuong " +
                                 $"FROM SanPham sp JOIN ChiTietHoaDon ct ON sp.ID = ct.SanPhamID " +
                                 $"WHERE ct.TabHoaDonID = {tabHoaDonID} AND ct.DonHangID = {donHangID}";
            QuanLySQL.NhapDLVaoSQL(queryUpdateSLSP);

            listCacSPDon.Clear();
            hoaDonDict.Remove(tabHoaDonID);
            dictListCacSPDon.Remove(tabHoaDonID);

            XuLyXoaTab(tabHoaDonID);

            LoadSPToflpSanPham(string.Empty);
            ResetGiaoDien();
            MessageBox.Show("Lưu hóa đơn thành công!");

        }


        private void lbTongThanhToan_TextChanged(object sender, EventArgs e)
        {
            lbKhachCanTra.Text = lbTongThanhToan.Text;
            txtKhachTra.Text = lbKhachCanTra.Text;
        }

        private void txtKhachTra_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtKhachTra.Text, out decimal khachTra))
            {

                txtKhachTra.Text = khachTra.ToString("N0");
                txtKhachTra.SelectionStart = txtKhachTra.Text.Length;

                decimal tongTien = decimal.Parse(lbKhachCanTra.Text);
                lbTienThua.Text = (khachTra - tongTien).ToString("N0");
            }
            else txtKhachTra.Text = "0";
        }

        private void txtTimKiemHang_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiemHang.Text.ToLower(); // Lấy từ khóa tìm kiếm và chuyển về chữ thường
            LoadSPToflpSanPham(tuKhoa);
        }

       
    }
}
