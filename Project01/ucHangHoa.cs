using Project01.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Project01
{
    public partial class ucHangHoa : UserControl
    {
        private int selectedRowIndex;
        private Dictionary<string, string> maLoaitotenLoai;
        string iconNutSua = @"F:\Học Tập\ShopDoChoiTreEm\Picture\pencil.png";
        string anhMacDinhPath = @"F:\Học Tập\ShopDoChoiTreEm\Picture\default-product.png";
        public ucHangHoa()
        {
            InitializeComponent();
            dgvDataHang.ContextMenuStrip = cmsChuotPhai;
        }

        private void ucHangHoa_Load(object sender, EventArgs e)
        {
            LoadData();
            TuyChinhCotTieuDe();
            DinhDangSo();
            LoadDataLoc();
            VeBoGoc();


        }

        private void VeBoGoc()
        {

            VeBoGocPanel.BoGocPanel(flpLocLoaiHang, 30);
            VeBoGocPanel.BoGocPanel(pTimKiem, 40);
            VeBoGocPanel.BoGocPanel(flpTinhTrang, 30);
            VeBoGocPanel.BoGocPanel(flpTonKho, 30);
            VeBoGocPanel.BoGocPanel(flpThem, 10);
            VeBoGocPanel.BoGocPanel(flpXuatFile, 10);
            VeBoGocPanel.BoGocPanel(flpNhapFile, 10);

        }

        //Hiển thị TenLoai tu MaLoai
        private void LoadLoaiHangMapping()
        {
            maLoaitotenLoai = new Dictionary<string, string>();

            // Nạp dữ liệu từ bảng QuanLyLoaiHang
            DataTable dataTable = QuanLySQL.XuatDLTuSQL("SELECT MaLoai, TenLoai FROM QuanLyLoaiHang");

            foreach (DataRow row in dataTable.Rows)
            {
                string maLoai = row["MaLoai"].ToString();
                string tenLoai = row["TenLoai"].ToString();
                maLoaitotenLoai[maLoai] = tenLoai;
            }
        }
        //Load data vào dgvLocLoai
        private void LoadDataLoc()
        {
            // Xóa tất cả các hàng hiện có trong DataGridView
            dgvLocLoai.Rows.Clear();

            // Đường dẫn đến file hình ảnh
            Image iconNS = Image.FromFile(iconNutSua);

            // không hiển thị ảnh có giá trị null
            dgvLocLoai.Columns["NutSua"].DefaultCellStyle.NullValue = null;

            // Thêm một hàng mới với giá trị "Tất cả" vào cột TenLoai
            int rowIndexAll = dgvLocLoai.Rows.Add();
            dgvLocLoai.Rows[rowIndexAll].Cells["TenLoai"].Value = "Tất cả";

            // Lấy dữ liệu từ cơ sở dữ liệu
            DataTable dataTableLoai = QuanLySQL.XuatDLTuSQL("select MaLoai, TenLoai FROM QuanLyLoaiHang");

            // Thêm từng hàng từ cơ sở dữ liệu vào DataGridView
            foreach (DataRow rowLoai in dataTableLoai.Rows)
            {
                int rowIndexLoc = dgvLocLoai.Rows.Add();
                dgvLocLoai.Rows[rowIndexLoc].Cells["TenLoai"].Value = rowLoai["TenLoai"];
            }

        }

        //Loaddata vào dgvDataHang
        private void LoadData()
        {
            dgvDataHang.Rows.Clear();

            // Nạp dữ liệu vào DataGridView từ SQL
            DataTable dataTable = QuanLySQL.XuatDLTuSQL("SELECT MaHangHoa, TenHang, MaLoaiHang, GiaNhap, GiaBan, SoLuong, " +
                "TrangThai, GhiChu, DuongDanAnh FROM QuanLyHang");

            LoadLoaiHangMapping();

            foreach (DataRow row in dataTable.Rows)
            {
                int rowIndex = dgvDataHang.Rows.Add();

                // Gán giá trị cho các cột
                dgvDataHang.Rows[rowIndex].Cells["MaHangHoa"].Value = row["MaHangHoa"];
                dgvDataHang.Rows[rowIndex].Cells["TenHang"].Value = row["TenHang"];
                dgvDataHang.Rows[rowIndex].Cells["GiaNhap"].Value = row["GiaNhap"];
                dgvDataHang.Rows[rowIndex].Cells["GiaBan"].Value = row["GiaBan"];
                dgvDataHang.Rows[rowIndex].Cells["SoLuong"].Value = row["SoLuong"];
                dgvDataHang.Rows[rowIndex].Cells["TrangThai"].Value = (bool)row["TrangThai"];
                dgvDataHang.Rows[rowIndex].Cells["GhiChu"].Value = row["GhiChu"];
                //dgvDataHang.Rows[rowIndex].Cells["MaLoaiHang"].Value = row["MaLoaiHang"];

                string maLoaiHang = row["MaLoaiHang"].ToString();
                if (maLoaitotenLoai.ContainsKey(maLoaiHang))
                    dgvDataHang.Rows[rowIndex].Cells["MaLoaiHang"].Value = maLoaitotenLoai[maLoaiHang];
                else
                    dgvDataHang.Rows[rowIndex].Cells["MaLoaiHang"].Value = "Không xác định";


                //Xử lý ảnh
                string duongDanAnh = row["DuongDanAnh"].ToString();
                if (!string.IsNullOrEmpty(duongDanAnh) && System.IO.File.Exists(duongDanAnh))
                {
                    using (Image anhDaCo = Image.FromFile(duongDanAnh))
                    {
                        // Thay đổi kích thước ảnh và gán vào ô
                        dgvDataHang.Rows[rowIndex].Cells["DuongDanAnh"].Value = ThayDoiKichThuocAnh(anhDaCo, 50, 50);
                    }
                }
                else
                {
                    using (Image anhMacDinh = Image.FromFile(anhMacDinhPath))
                    {
                        // Thay đổi kích thước ảnh và gán vào ô
                        dgvDataHang.Rows[rowIndex].Cells["DuongDanAnh"].Value = ThayDoiKichThuocAnh(anhMacDinh, 50, 50);
                    }
                }
            }
        }

        private Image ThayDoiKichThuocAnh(Image anh, int rong, int cao)
        {
            // Tạo một đối tượng Bitmap mới với kích thước mới
            Bitmap anhDaThayDoiKichThuoc = new Bitmap(rong, cao);

            // Sử dụng đối tượng Graphics để vẽ hình ảnh mới
            using (Graphics g = Graphics.FromImage(anhDaThayDoiKichThuoc))
            {
                // Cài đặt chế độ nội suy để cải thiện chất lượng hình ảnh khi thay đổi kích thước
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // Vẽ hình ảnh gốc lên đối tượng Bitmap mới với kích thước mới
                g.DrawImage(anh, 0, 0, rong, cao);
            }

            // Trả về hình ảnh đã được thay đổi kích thước
            return anhDaThayDoiKichThuoc;
        }

        private void DinhDangSo()
        {
            // Định dạng cột GiaNhap với phân cách hàng nghìn và không có chữ số thập phân
            dgvDataHang.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";

            // Định dạng cột GiaBan với phân cách hàng nghìn và không có chữ số thập phân
            dgvDataHang.Columns["GiaBan"].DefaultCellStyle.Format = "N0";

        }

        private void TuyChinhCotTieuDe()
        {
            // Căn chỉnh tiêu đề cột GiaNhap, GiaBan, SoLuong căn giữa phải
            dgvDataHang.Columns["GiaNhap"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDataHang.Columns["GiaBan"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvDataHang.Columns["SoLuong"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Căn chỉnh tiêu đề cột TrangThai căn giữa
            dgvDataHang.Columns["TrangThai"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dgvDataHang.Columns["GhiChu"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        
        //Load lại data khi du lieu thay doi
        private void LoadDataChanged(object sender, EventArgs e)
        {
            // load lại data khi dữ liệu thay đổi
            LoadData();
            LoadDataLoc();
        }
        //Dinh dang SP00i
        private void dgvDataHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDataHang.Columns[e.ColumnIndex].Name == "MaHangHoa" && e.Value != null)
            {
                string value = e.Value.ToString();
                // Đảm bảo rằng mã đồ chơi có định dạng SP00i
                e.Value = "SP" + value.PadLeft(3, '0');
                e.FormattingApplied = true;
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {

        }
        //XyLy MenuStrip Sửa
        private void tsmiSua_Click(object sender, EventArgs e)
        {

            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dgvDataHang.SelectedRows.Count > 0)
            {
                // Lấy hàng được chọn (nếu có nhiều hàng, chỉ lấy hàng đầu tiên)
                DataGridViewRow selectedRow = dgvDataHang.SelectedRows[0];

                // Lấy MaLoaiHang từ hàng được chọn
                string maHangHoa = selectedRow.Cells["MaHangHoa"].Value.ToString();
                // Truy vấn SQL để lấy toàn bộ dữ liệu của maHangHoa
                string query = $"SELECT * FROM QuanLyHang WHERE MaHangHoa = '{maHangHoa}'";


                DataTable dataTable = QuanLySQL.XuatDLTuSQL(query);


                if (dataTable.Rows.Count > 0)
                {
                    // Lấy dữ liệu từ SQL
                    DataRow row = dataTable.Rows[0];

                    string maLoaiHang = row["MaLoaiHang"].ToString();
                    string tenHang = row["TenHang"].ToString();
                    string giaNhap = row["GiaNhap"].ToString();
                    string giaBan = row["GiaBan"].ToString();
                    string soLuong = row["SoLuong"].ToString();
                    string trangThai = row["TrangThai"].ToString();
                    string ghiChu = row["GhiChu"].ToString();
                    string duongDanAnh = row["DuongDanAnh"].ToString();

                    // Mở form Nhập Hàng và truyền dữ liệu
                    frmNhapHang frmNhap = new frmNhapHang();
                    // Hiển thị form để sửa thông tin
                    frmNhap.DataChanged += LoadDataChanged;
                    frmNhap.HienThiBtnSua();
                    // Gán giá trị cho các điều khiển trong frmNhapHang
                    frmNhap.txtMaHH.Text = maHangHoa;         // Gán giá trị cho TextBox TenHang
                    frmNhap.txtTenHang.Text = tenHang;         // Gán giá trị cho TextBox TenHang
                    frmNhap.txtGiaNhap.Text = giaNhap;         // Gán giá trị cho TextBox GiaNhap
                    frmNhap.txtGiaBan.Text = giaBan;           // Gán giá trị cho TextBox GiaBan
                    frmNhap.txtSoLuong.Text = soLuong;         // Gán giá trị cho TextBox SoLuong
                    frmNhap.cbTrangThaiBan.Checked = (trangThai == "True");  // Gán giá trị cho CheckBox TrangThaiBan
                    frmNhap.txtNote.Text = ghiChu;             // Gán giá trị cho TextBox GhiChu

                    // Kiểm tra nếu đường dẫn ảnh hợp lệ thì gán ảnh vào PictureBox
                    if (!string.IsNullOrEmpty(duongDanAnh) && File.Exists(duongDanAnh))
                    {
                        frmNhap.pbHinhSP.Image = Image.FromFile(duongDanAnh);  // Gán ảnh vào PictureBox
                        frmNhap.pbHinhSP.ImageLocation = duongDanAnh;
                    }
                    else
                    {
                        frmNhap.pbHinhSP.Image = Image.FromFile(anhMacDinhPath);  // Nếu không có ảnh, gán ảnh mặc định
                    }
                    frmNhap.LoadData();

                    frmNhap.cbLoaiHang.SelectedValue = maLoaiHang;

                    frmNhap.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu cho mã loại hàng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //XuLy MenuStrip Xóa
        private void tsmiXoa_Click(object sender, EventArgs e)
        {
            if (dgvDataHang.SelectedRows.Count > 0)
            {
                // Lấy hàng được chọn (nếu có nhiều hàng, chỉ lấy hàng đầu tiên)
                DataGridViewRow selectedRow = dgvDataHang.SelectedRows[0];

                // Lấy MaLoaiHang từ hàng được chọn
                string maHang = selectedRow.Cells["MaHangHoa"].Value.ToString();

                //Xác nhận Xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa hàng hóa có mã {maHang} không?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Tạo câu lệnh SQL để xóa thông tin dựa trên MaHangHoa
                    string query = $"DELETE FROM QuanLyHang WHERE MaHangHoa = '{maHang}'";

                    try
                    {
                        // Thực hiện câu lệnh xóa
                        QuanLySQL.NhapDLVaoSQL(query);

                        // Thông báo xóa thành công
                        MessageBox.Show("Xóa hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Cập nhật lại danh sách sau khi xóa
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn hàng hóa để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvDataHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        //Xử Lý Sắp Xếp
        private SortOrder LayHuongSapXep(int chiSoCot)
        {
            // Lấy giá trị sắp xếp hiện tại của cột
            if (dgvDataHang.SortedColumn != null && dgvDataHang.SortedColumn.Index == chiSoCot)
            {
                // Nếu cột đã được sắp xếp, thay đổi hướng sắp xếp
                return dgvDataHang.SortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                // Nếu cột chưa được sắp xếp, sắp xếp theo thứ tự tăng dần
                return SortOrder.Ascending;
            }
        }

        private void SapXepCot(int chiSoCot, SortOrder huongSapXep)
        {
            // Xác định cột sắp xếp
            DataGridViewColumn cot = dgvDataHang.Columns[chiSoCot];

            // Thực hiện sắp xếp
            if (huongSapXep == SortOrder.Ascending)
            {
                dgvDataHang.Sort(cot, ListSortDirection.Ascending);
            }
            else
            {
                dgvDataHang.Sort(cot, ListSortDirection.Descending);
            }

            // Cập nhật tiêu đề cột để hiển thị hướng sắp xếp
            foreach (DataGridViewColumn c in dgvDataHang.Columns)
            {
                c.HeaderCell.SortGlyphDirection = c.Index == chiSoCot ? huongSapXep : SortOrder.None;
            }
        }

        private void dgvDataHang_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Lấy chỉ số của cột được nhấp vào
            int chiSoCot = e.ColumnIndex;

            DataGridViewColumn cot = dgvDataHang.Columns[chiSoCot];
            if (cot.SortMode == DataGridViewColumnSortMode.NotSortable)
            {
                // Hiển thị thông báo nếu cột không thể sắp xếp
                //MessageBox.Show("Cột này không thể sắp xếp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Kiểm tra hướng sắp xếp hiện tại (tăng dần hoặc giảm dần)
            SortOrder huongSapXep = LayHuongSapXep(chiSoCot);

            // Thực hiện sắp xếp
            SapXepCot(chiSoCot, huongSapXep);
        }
        //Sự kiện kich vào 1 ô để Lọc theo Loại
        private void dgvLocLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo người dùng chọn vào dòng hợp lệ
            {
                // Lấy giá trị của ô "TenLoai" trong dòng được chọn
                string tenLoai = dgvLocLoai.Rows[e.RowIndex].Cells["TenLoai"].Value.ToString();
                // Lấy thông tin ô được chọn
                DataGridViewCell cell = dgvLocLoai.Rows[e.RowIndex].Cells[e.ColumnIndex];
                LocTheoLoaiHang(tenLoai); // Lọc dữ liệu theo loại hàng
            }
        }



        private void LocTheoLoaiHang(string tenLoai)
        {
            // Nếu tenLoai là "Tất cả", tải toàn bộ dữ liệu
            if (tenLoai == "Tất cả")
            {
                LoadData(); // Phương thức để tải toàn bộ dữ liệu
                return;
            }

            // Truy vấn SQL để lọc các sản phẩm theo tên loại hàng
            string query = "SELECT MaHangHoa, TenHang, MaLoaiHang, GiaNhap, GiaBan, SoLuong, TrangThai, GhiChu, DuongDanAnh " +
                   "FROM QuanLyHang " +
                   $"WHERE MaLoaiHang IN (SELECT MaLoai FROM QuanLyLoaiHang WHERE TenLoai = N'{tenLoai}')";

            dgvDataHang.Rows.Clear();

            // Nạp dữ liệu vào DataGridView từ SQL
            DataTable dataTable = QuanLySQL.XuatDLTuSQL(query);

            LoadLoaiHangMapping();

            foreach (DataRow row in dataTable.Rows)
            {
                int rowIndex = dgvDataHang.Rows.Add();

                // Gán giá trị cho các cột
                dgvDataHang.Rows[rowIndex].Cells["MaHangHoa"].Value = row["MaHangHoa"];
                dgvDataHang.Rows[rowIndex].Cells["TenHang"].Value = row["TenHang"];
                dgvDataHang.Rows[rowIndex].Cells["GiaNhap"].Value = row["GiaNhap"];
                dgvDataHang.Rows[rowIndex].Cells["GiaBan"].Value = row["GiaBan"];
                dgvDataHang.Rows[rowIndex].Cells["SoLuong"].Value = row["SoLuong"];
                dgvDataHang.Rows[rowIndex].Cells["TrangThai"].Value = (bool)row["TrangThai"];
                dgvDataHang.Rows[rowIndex].Cells["GhiChu"].Value = row["GhiChu"];
                //dgvDataHang.Rows[rowIndex].Cells["MaLoaiHang"].Value = row["MaLoaiHang"];

                string maLoaiHang = row["MaLoaiHang"].ToString();
                if (maLoaitotenLoai.ContainsKey(maLoaiHang))
                    dgvDataHang.Rows[rowIndex].Cells["MaLoaiHang"].Value = maLoaitotenLoai[maLoaiHang];
                else
                    dgvDataHang.Rows[rowIndex].Cells["MaLoaiHang"].Value = "Không xác định";


                //Xử lý ảnh
                string duongDanAnh = row["DuongDanAnh"].ToString();
                if (!string.IsNullOrEmpty(duongDanAnh) && System.IO.File.Exists(duongDanAnh))
                {
                    using (Image anhDaCo = Image.FromFile(duongDanAnh))
                    {
                        // Thay đổi kích thước ảnh và gán vào ô
                        dgvDataHang.Rows[rowIndex].Cells["DuongDanAnh"].Value = ThayDoiKichThuocAnh(anhDaCo, 50, 50);
                    }
                }
                else
                {
                    using (Image anhMacDinh = Image.FromFile(anhMacDinhPath))
                    {
                        // Thay đổi kích thước ảnh và gán vào ô
                        dgvDataHang.Rows[rowIndex].Cells["DuongDanAnh"].Value = ThayDoiKichThuocAnh(anhMacDinh, 50, 50);
                    }
                }
            }
        }




        private void dgvLocLoai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void pbThemLoai_Click(object sender, EventArgs e)
        {
            frmLoaiHang frmLoai = new frmLoaiHang();
            frmLoai.DataChanged += LoadDataChanged;
            frmLoai.ShowDialog();
        }

        
        //XyLy flpThem
        private void flpThem_Click(object sender, EventArgs e)
        {
            frmNhapHang frmNhap = new frmNhapHang();
            frmNhap.DataChanged += LoadDataChanged;
            frmNhap.ShowDialog();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            flpThem_Click(sender, e);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            flpThem_Click(sender, e);
        }

        private void flpThem_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
