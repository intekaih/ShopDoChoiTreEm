using Project01.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project01
{
    public partial class ucHangHoa : UserControl
    {
        private Dictionary<string, string> idtoten;
        private Dictionary<string, int> tentoid;

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
            VeBoGoc();
        }

        private void VeBoGoc()
        {
            VeBoGocPanel.BoGocPanel(flpLocLoaiHang, 30);
            VeBoGocPanel.BoGocPanel(pTimKiem, 40);
            VeBoGocPanel.BoGocPanel(flpTonKho, 30);
            VeBoGocPanel.BoGocPanel(flpThem, 10);
            VeBoGocPanel.BoGocPanel(flpXuatFile, 10);
            VeBoGocPanel.BoGocPanel(flpNhapFile, 10);
            VeBoGocPanel.BoGocPanel(flpXuatXu, 30);
            VeBoGocPanel.BoGocPanel(flpHangSX, 30);
            VeBoGocPanel.BoGocPanel(flpLocDoTuoi, 30);
        }

        //Hiển thị TenLoai tu MaLoai
        private void IDtoTen(string bangSQL)
        {
            idtoten = new Dictionary<string, string>();

            // Nạp dữ liệu từ bảng LoaiSP
            DataTable dataTable = QuanLySQL.XuatDLTuSQL($"SELECT ID, Ten FROM [{bangSQL}]");

            foreach (DataRow row in dataTable.Rows)
            {
                string id = row["ID"].ToString();
                string ten = row["Ten"].ToString();
                idtoten[id] = ten;
            }
        }
        // đổi tên thành ID
        private int? TentoID(string bangSQL, string ten)
        {
            tentoid = new Dictionary<string, int>();

            // Nạp dữ liệu từ bảng trong CSDL
            DataTable dataTable = QuanLySQL.XuatDLTuSQL($"SELECT ID, Ten FROM [{bangSQL}]");

            foreach (DataRow row in dataTable.Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string tenRow = row["Ten"].ToString();
                tentoid[tenRow] = id;
            }

            // Kiểm tra nếu tên tồn tại trong từ điển, trả về ID
            if (tentoid.ContainsKey(ten))
            {
                return tentoid[ten];
            }

            return null; // Trả về null nếu không tìm thấy tên
        }


        //Load data loc
        private void LoadDataLoc(DataGridView dgvLoc, string tenCell, string tenbangSQL)
        {
            // Xóa tất cả các hàng hiện có trong DataGridView
            dgvLoc.Rows.Clear();
            // Thêm một hàng mới với giá trị "Tất cả" vào cột TenLoai
            int rowIndexAll = dgvLoc.Rows.Add();
            dgvLoc.Rows[rowIndexAll].Cells[tenCell].Value = "Tất cả";

            // Lấy dữ liệu từ cơ sở dữ liệu
            DataTable dataTableLoai = QuanLySQL.XuatDLTuSQL($"select ID, Ten FROM [{tenbangSQL}]");

            // Thêm từng hàng từ cơ sở dữ liệu vào DataGridView
            foreach (DataRow rowLoai in dataTableLoai.Rows)
            {
                int rowIndexLoc = dgvLoc.Rows.Add();
                dgvLoc.Rows[rowIndexLoc].Cells[tenCell].Value = rowLoai["Ten"];
            }

        }


        //Loaddata vào dgvDataHang
        private void LoadData()
        {
            dgvDataHang.Rows.Clear();

            DinhDangSo();

            LoadDataLoc(dgvLocLoai, "LoaiSP", "LoaiSP");
            LoadDataLoc(dgvLocDoTuoi, "DoTuoi", "DoTuoi");
            LoadDataLoc(dgvLocHangSX, "HangSX", "HangSX");
            LoadDataLoc(dgvLocXuatXu, "XuatXu", "XuatXu");

            
            DataTable dataTable = QuanLySQL.XuatDLTuSQL("SELECT * FROM SanPham");

            IDtoTen("LoaiSP");

           
            LoadDataToDGV(dataTable);
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
            LoadDataLoc(dgvLocLoai, "LoaiSP", "LoaiSP");
            LoadDataLoc(dgvLocDoTuoi, "DoTuoi", "DoTuoi");
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





        //XyLy MenuStrip Sửa
        private void tsmiSua_Click(object sender, EventArgs e)
        {

            // Kiểm tra xem có hàng nào được chọn trong DataGridView không
            if (dgvDataHang.SelectedRows.Count > 0)
            {
                // Lấy hàng được chọn (nếu có nhiều hàng, chỉ lấy hàng đầu tiên)
                DataGridViewRow selectedRow = dgvDataHang.SelectedRows[0];

                // Lấy LoaiID từ hàng được chọn
                string maHangHoa = selectedRow.Cells["MaHangHoa"].Value.ToString();
                // Truy vấn SQL để lấy toàn bộ dữ liệu của maHangHoa
                string query = $"SELECT * FROM SanPham WHERE ID = '{maHangHoa}'";

                DataTable dataTable = QuanLySQL.XuatDLTuSQL(query);

                if (dataTable.Rows.Count > 0)
                {
                    // Lấy dữ liệu từ SQL
                    DataRow row = dataTable.Rows[0];
                    string trangThai = row["enable"].ToString();
                    string duongDanAnh = row["hinhanhurl"].ToString();

                    // Mở form Nhập Hàng và truyền dữ liệu
                    frmThemHang frmNhap = new frmThemHang();
                    // Hiển thị form để sửa thông tin
                    frmNhap.DataChanged += LoadDataChanged;
                    frmNhap.HienThiBtnSua();
                    // Gán giá trị cho các điều khiển trong frmNhapHang
                    frmNhap.txtID.Text = row["ID"].ToString();          // Gán giá trị cho TextBox TenHang
                    frmNhap.txtTenHang.Text = row["Ten"].ToString();          // Gán giá trị cho TextBox TenHang
                    frmNhap.txtGiaNhap.Text = row["GiaNhap"].ToString();          // Gán giá trị cho TextBox GiaNhap
                    frmNhap.txtGiaBan.Text = row["GiaBan"].ToString();            // Gán giá trị cho TextBox GiaBan
                    frmNhap.txtSoLuong.Text = row["Ton"].ToString();          // Gán giá trị cho TextBox SoLuong
                    frmNhap.cboTrangThaiBan.Checked = (trangThai == "True");  // Gán giá trị cho CheckBox TrangThaiBan
                    frmNhap.txtNote.Text = row["MoTa"].ToString();              // Gán giá trị cho TextBox GhiChu
                    frmNhap.LoadData();
                    frmNhap.cbXuatXu.SelectedValue = row["XuatXuID"];
                    frmNhap.cbHangSX.SelectedValue = row["HangID"];
                    frmNhap.cbDoTuoi.SelectedValue = row["dotuoiid"];
                    frmNhap.cbLoaiHang.SelectedValue = row["LoaiID"];



                    // Kiểm tra nếu đường dẫn ảnh hợp lệ thì gán ảnh vào PictureBox
                    if (!string.IsNullOrEmpty(duongDanAnh) && File.Exists(duongDanAnh))
                    {
                        frmNhap.pbHinhSP.Image = Image.FromFile(duongDanAnh);  // Gán ảnh vào PictureBox
                        frmNhap.pbHinhSP.ImageLocation = duongDanAnh;
                    }
                    else
                        frmNhap.pbHinhSP.Image = Image.FromFile(anhMacDinhPath);  // Nếu không có ảnh, gán ảnh mặc định


                    frmNhap.ShowDialog();
                }
                else
                    MessageBox.Show("Không tìm thấy dữ liệu cho mã loại hàng này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Vui lòng chọn hàng cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        //XuLy MenuStrip Xóa
        private void tsmiXoa_Click(object sender, EventArgs e)
        {
            if (dgvDataHang.SelectedRows.Count > 0)
            {
                // Lấy hàng được chọn (nếu có nhiều hàng, chỉ lấy hàng đầu tiên)
                DataGridViewRow selectedRow = dgvDataHang.SelectedRows[0];

                // Lấy LoaiID từ hàng được chọn
                string maHang = selectedRow.Cells["MaHangHoa"].Value.ToString();

                //Xác nhận Xóa
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa hàng hóa có mã {maHang} không?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Tạo câu lệnh SQL để xóa thông tin dựa trên MaHangHoa
                    string query = $"DELETE FROM SanPham WHERE ID = '{maHang}'";

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
        //Sự kiện kich vào 1 ô để Lọc
        private void dgvLocLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            XyLyLoc();
        }

        private void dgvLocDoTuoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            XyLyLoc();
        }

        private void dgvLocHangSX_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            XyLyLoc();
        }

        private void dgvLocXuatXu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            XyLyLoc();
        }

        private void rdHetHang_CheckedChanged(object sender, EventArgs e)
        {
            XyLyLoc();
        }

        private void rdHangSapHet_CheckedChanged(object sender, EventArgs e)
        {
            XyLyLoc();
        }

        private void rdAll_CheckedChanged(object sender, EventArgs e)
        {
            XyLyLoc();
        }

        // Tạo lệnh query để thực hiện lọc
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
        // Lấy ID từ các row được chọn
        private int? SelectToID(DataGridView dgv, string tenCell, string tenBangSQL)
        {
            int? ID = null;
            DataGridViewRow row = dgv.SelectedRows[0];
            string ten = row.Cells[tenCell].Value.ToString();
            ID = TentoID(tenBangSQL, ten);
            return ID;
        }


        private void XyLyLoc()
        {
            int? loaiID = SelectToID(dgvLocLoai, "LoaiSP", "LoaiSP");

            int? dotuoiID = SelectToID(dgvLocDoTuoi, "DoTuoi", "DoTuoi");

            int? hangID = SelectToID(dgvLocHangSX, "HangSX", "HangSX");

            int? xuatxuID = SelectToID(dgvLocXuatXu, "XuatXu", "XuatXu");

            int? tonMin = null;
            int? tonKhong = null;
            if (rdHangSapHet.Checked == true)
            {
                if (string.IsNullOrEmpty(txtSLTonLoc.Text))
                    tonMin = null;
                else
                    tonMin = int.Parse(txtSLTonLoc.Text);
            }
            if (rdHetHang.Checked == true)
            {
                tonKhong = 0;
            }

            string query = QueryLoc(loaiID, dotuoiID, hangID, xuatxuID, tonMin, tonKhong);

            dgvDataHang.Rows.Clear();

            DataTable dataTable = QuanLySQL.XuatDLTuSQL(query);

            IDtoTen("LoaiSP");
            LoadDataToDGV(dataTable);

        }

       

        private void LoadDataToDGV(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                int rowIndex = dgvDataHang.Rows.Add();

                // Gán giá trị cho các cột
                dgvDataHang.Rows[rowIndex].Cells["MaHangHoa"].Value = row["ID"];
                dgvDataHang.Rows[rowIndex].Cells["TenHang"].Value = row["Ten"];
                dgvDataHang.Rows[rowIndex].Cells["GiaNhap"].Value = row["GiaNhap"];
                dgvDataHang.Rows[rowIndex].Cells["GiaBan"].Value = row["GiaBan"];
                dgvDataHang.Rows[rowIndex].Cells["SoLuong"].Value = row["Ton"];
                dgvDataHang.Rows[rowIndex].Cells["TrangThai"].Value = (bool)row["enable"];
                dgvDataHang.Rows[rowIndex].Cells["GhiChu"].Value = row["MoTa"];

                string maLoaiHang = row["LoaiID"].ToString();
                dgvDataHang.Rows[rowIndex].Cells["MaLoaiHang"].Value = idtoten[maLoaiHang];

                //Xử lý ảnh
                string duongDanAnh = row["hinhanhurl"].ToString();
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
            frmThemHang frmNhap = new frmThemHang();
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


        //chỉ cho phép Nhập số
        private void txtSLTonLoc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        //XyLy Tìm kiếm theo chữ cái
        private void txtTimKiemHang_TextChanged(object sender, EventArgs e)
        {
            string textTK = txtTimKiemHang.Text.Trim().ToLower();
            LocDuLieuDGV(dgvDataHang, "TenHang", textTK);
        }

        private void txtTKLoai_TextChanged(object sender, EventArgs e)
        {
            string textKT = txtTKLoai.Text.Trim().ToLower();
            LocDuLieuDGV(dgvLocLoai, "LoaiSP", textKT);
        }

        private void txtTKDoTuoi_TextChanged(object sender, EventArgs e)
        {
            string textKT = txtTKDoTuoi.Text.Trim().ToLower();
            LocDuLieuDGV(dgvLocDoTuoi, "DoTuoi", textKT);
        }

        private void txtTKHangSX_TextChanged(object sender, EventArgs e)
        {
            string textKT = txtTKHangSX.Text.Trim().ToLower();
            LocDuLieuDGV(dgvLocHangSX, "HangSX", textKT);
        }

        private void txtTKXuatXu_TextChanged(object sender, EventArgs e)
        {
            string textKT = txtTKXuatXu.Text.Trim().ToLower();
            LocDuLieuDGV(dgvLocXuatXu, "XuatXu", textKT);
        }

        private void LocDuLieuDGV(DataGridView dgv, string tenCell, string textTK)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (!row.IsNewRow)
                {
                    string ten = row.Cells[tenCell].Value?.ToString().ToLower() ?? "";
                    bool hienThiHang = ten.Contains(textTK);
                    row.Visible = hienThiHang;
                }
            }
        }

        private void txtSLTonLoc_TextChanged(object sender, EventArgs e)
        {
            XyLyLoc();
        }

        private void flpXuatFile_Click(object sender, EventArgs e)
        {
           
        }

    }
}
