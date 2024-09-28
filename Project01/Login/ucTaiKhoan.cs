using DocumentFormat.OpenXml.Math;
using Project01.Class;
using Project01.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Project01
{
    public partial class ucTaiKhoan : Form
    {
        BindingSource bs = new BindingSource();

        public ucTaiKhoan()
        {
            InitializeComponent();
        }
      

        private void ucTaiKhoan_Load(object sender, EventArgs e)
        {
            //TODO: khong can ket noi
            QuanLySQL.KetNoi();
            LoadTKToDGV();
        }

        private void LoadTKToDGV()
        {
            string query = "select * from TaiKhoan";
            DataTable dt = QuanLySQL.XuatDLTuSQL(query);

            dt.Columns.Add("ActualPassword", typeof(string));

            // Mask passwords and store actual passwords in a hidden column
            foreach (DataRow row in dt.Rows)
            {
                string matKhau = row["MatKhau"].ToString(); // Get the actual password
                row["MatKhau"] = new string('*', matKhau.Length); // Replace with asterisks
                row["ActualPassword"] = matKhau; // Store actual password in the hidden column
            }

            //bs.DataSource = dt;

            dgvTaiKhoan.DataSource = dt;
            dgvTaiKhoan.Columns["ActualPassword"].Visible = false;
        }

        private bool CheckID()
        {
            string query = $"select * from Taikhoan where TenDangNhap = '{txtTenDangNhap}'";
            var data = QuanLySQL.LayTruongDuLieu(query);

            if (data == txtTenDangNhap.Text)
            {
                return true;
            }
            return false;
        }


        private void dgvTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTenDangNhap.Text = dgvTaiKhoan.SelectedCells[1].Value.ToString();
            txtHoTen.Text = dgvTaiKhoan.SelectedCells[3].Value.ToString();
            txtSoDienThoai.Text = dgvTaiKhoan.SelectedCells[6].Value.ToString();
            cb0VaiTro.Text = dgvTaiKhoan.SelectedCells[4].Value.ToString();
            txt_CanCuocCongDan.Text = dgvTaiKhoan.SelectedCells[7].Value.ToString();
            txt_NgayThoiViec.Text = dgvTaiKhoan.SelectedCells[8].Value.ToString();
            txt_NgaySinh.Text = dgvTaiKhoan.SelectedCells[9].Value.ToString();
            txt_DiaChiThuongTru.Text = dgvTaiKhoan.SelectedCells[10].Value.ToString();
            txt_Email.Text = dgvTaiKhoan.SelectedCells[11].Value.ToString();
            txt_LuuY.Text = dgvTaiKhoan.SelectedCells[12].Value.ToString();

            btn_Xoa.Enabled = true;
            btn_CapNhat.Enabled = true;
            string query = "SELECT * FROM TaiKhoan"; // Query to get active accounts
            DataTable dt = QuanLySQL.XuatDLTuSQL(query); // Execute the query

            // Add a hidden column for actual passwords
            dt.Columns.Add("ActualPassword", typeof(string));

            // Mask passwords and store actual passwords in a hidden column
            foreach (DataRow row in dt.Rows)
            {
                string matKhau = row["MatKhau"].ToString(); // Get the actual password
                row["MatKhau"] = new string('*', matKhau.Length); // Replace with asterisks
                row["ActualPassword"] = matKhau; // Store actual password in the hidden column
            }

            dgvTaiKhoan.DataSource = dt; // Assign data to DataGridView

            // Optionally hide the actual password column
            dgvTaiKhoan.Columns["ActualPassword"].Visible = false;
        }

        private void dgvTaiKhoan_Click(object sender, EventArgs e)
        {
            var debug = dgvTaiKhoan;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private bool validateAddUpdate()
        {

            return string.IsNullOrWhiteSpace(txtTenDangNhap.Text) ||
                    string.IsNullOrEmpty(txtHoTen.Text) ||
                    string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ||
                    string.IsNullOrWhiteSpace(txt_CanCuocCongDan.Text) ||
                    string.IsNullOrWhiteSpace(txt_NgayThoiViec.Text) ||
                     string.IsNullOrWhiteSpace(txt_NgaySinh.Text) ||
                      string.IsNullOrWhiteSpace(txt_DiaChiThuongTru.Text) ||
                       string.IsNullOrWhiteSpace(txt_Email.Text) ||
                       string.IsNullOrWhiteSpace(txt_LuuY.Text) ||

                    string.IsNullOrEmpty(cb0VaiTro.Text) ||
                    CheckID();


        }
        private bool ValidatePhoneNumber(string phoneNumber)
        {
            // Biểu thức chính quy để kiểm tra số điện thoại
            string pattern = @"^(0[3-9]\d{8}|\+84[3-9]\d{8})$";
            return Regex.IsMatch(phoneNumber, pattern);
        }





      

           

       
    

        private void txtTenDangNhap_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count == 0) return;

            int idDelete = (int)dgvTaiKhoan.SelectedCells[0].Value;
            DialogResult dialogResult = MessageBox.Show("     BẠN CÓ CHẮC CHẮN  ", "  XÓA", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                // Sửa câu lệnh SQL với tên cột chính xác
                string query = $"DELETE FROM TaiKhoan WHERE ID = {idDelete}"; // Thay 'TenDangNhap' bằng tên cột chính xác
                QuanLySQL.NhapDLVaoSQL(query);
                LoadTKToDGV();
            }
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            // Validate all required fields
            if (validateAddUpdate())
            {
                MessageBox.Show("Thêm Thất Bại: Vui lòng kiểm tra các trường.");
                return;
            }

            // Validate Tên Đăng Nhập
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Đăng Nhập.");
                return;
            }

            // Validate Ngày Thôi Việc
            DateTime ngayThoiViec;
            if (!DateTime.TryParse(txt_NgayThoiViec.Text, out ngayThoiViec))
            {
                MessageBox.Show("Ngày Thôi Việc không hợp lệ. Vui lòng nhập đúng định dạng ngày.");
                return;
            }

            // Validate Ngày Sinh
            DateTime ngaySinh;
            if (!DateTime.TryParse(txt_NgaySinh.Text, out ngaySinh))
            {
                MessageBox.Show("Ngày Sinh không hợp lệ. Vui lòng nhập đúng định dạng ngày.");
                return;
            }

            // Validate Địa chỉ thường trú
            if (string.IsNullOrWhiteSpace(txt_DiaChiThuongTru.Text))
            {
                MessageBox.Show("Vui lòng nhập Địa chỉ thường trú.");
                return;
            }

            // Validate Email
            if (string.IsNullOrWhiteSpace(txt_Email.Text))
            {
                MessageBox.Show("Vui lòng nhập Email.");
                return;
            }

            // Validate Lưu Ý
            if (string.IsNullOrWhiteSpace(txt_LuuY.Text))
            {
                MessageBox.Show("Vui lòng nhập Lưu Ý.");
                return;
            }

            // Prepare SQL statement for insertion
            string sql = $"INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, Enable, SDT, CanCuocCongDan, NgaySinh, NgayThoiViec, DiaChiThuongTru, Email, LuuY) VALUES " +
                          $"(N'{txtTenDangNhap.Text}', N'{txtTenDangNhap.Text}', N'{txtHoTen.Text}', N'{cb0VaiTro.Text}', 1, " +
                          $"'{txtSoDienThoai.Text}', '{txt_CanCuocCongDan.Text}', '{ngaySinh:yyyy-MM-dd}', '{ngayThoiViec:yyyy-MM-dd}', N'{txt_DiaChiThuongTru.Text}', '{txt_Email.Text}', N'{txt_LuuY.Text}')";

            // Execute SQL command
            try
            {
                QuanLySQL.NhapDLVaoSQL(sql);
                MessageBox.Show("Thêm Thành Công");
                LoadTKToDGV();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi thêm: {ex.Message}");
            }
        }

        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường không được để trống
            if (validateAddUpdate())
            {
                MessageBox.Show("Cập nhật thất bại, vui lòng điền đầy đủ thông tin.");
                return; // Dừng thực hiện nếu có trường trống
            }

            // Lấy ID từ dòng đang chọn trong DataGridView
            if (dgvTaiKhoan.SelectedCells.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần cập nhật.");
                return; // Dừng nếu không có dòng nào được chọn
            }

            int selectedID = Convert.ToInt32(dgvTaiKhoan.SelectedCells[0].Value);

            // Kiểm tra tính hợp lệ của Ngày Thôi Việc
            DateTime ngayThoiViec;
            if (!DateTime.TryParse(txt_NgayThoiViec.Text, out ngayThoiViec))
            {
                MessageBox.Show("Ngày Thôi Việc không hợp lệ. Vui lòng nhập đúng định dạng ngày.");
                return;
            }

            // Kiểm tra tính hợp lệ của Ngày Sinh
            DateTime ngaySinh;
            if (!DateTime.TryParse(txt_NgaySinh.Text, out ngaySinh))
            {
                MessageBox.Show("Ngày Sinh không hợp lệ. Vui lòng nhập đúng định dạng ngày.");
                return;
            }

            // Kiểm tra tính hợp lệ của Địa chỉ thường trú
            if (string.IsNullOrWhiteSpace(txt_DiaChiThuongTru.Text))
            {
                MessageBox.Show("Vui lòng nhập Địa chỉ thường trú.");
                return;
            }

            // Kiểm tra tính hợp lệ của Email
            if (string.IsNullOrWhiteSpace(txt_Email.Text))
            {
                MessageBox.Show("Vui lòng nhập Email.");
                return;
            }

            // Kiểm tra tính hợp lệ của trường Lưu ý
            if (string.IsNullOrWhiteSpace(txt_LuuY.Text))
            {
                MessageBox.Show("Vui lòng nhập Lưu ý.");
                return;
            }

            // Chuẩn bị câu lệnh SQL để cập nhật thông tin
            string sql = $"UPDATE TaiKhoan " +
                         $"SET TenDangNhap = N'{txtTenDangNhap.Text}', " +
                         $"HoTen = N'{txtHoTen.Text}', " +
                         $"VaiTro = N'{cb0VaiTro.Text}', " +
                         $"Enable = 1, " +
                         $"SDT = '{txtSoDienThoai.Text}', " +
                         $"CanCuocCongDan = '{txt_CanCuocCongDan.Text}', " +
                         $"NgaySinh = '{ngaySinh.ToString("yyyy-MM-dd")}', " +
                         $"NgayThoiViec = '{ngayThoiViec.ToString("yyyy-MM-dd")}', " +
                         $"DiaChiThuongTru = N'{txt_DiaChiThuongTru.Text}', " +
                         $"Email = '{txt_Email.Text}', " +
                         $"LuuY = N'{txt_LuuY.Text}' " + // Thêm trường Lưu ý
                         $"WHERE ID = {selectedID}";

            try
            {
                // Thực thi câu lệnh SQL
                QuanLySQL.NhapDLVaoSQL(sql);
                MessageBox.Show("Cập nhật thành công!");

                // Tải lại dữ liệu sau khi cập nhật
                LoadTKToDGV();
            }
            catch (Exception ex)
            {
                // Hiển thị lỗi nếu có lỗi xảy ra trong quá trình cập nhật
                MessageBox.Show($"Có lỗi xảy ra khi cập nhật: {ex.Message}");
            }
        }

        private void btn_Xoa_Click_1(object sender, EventArgs e)
        {

            if (dgvTaiKhoan.SelectedRows.Count == 0) return;

            int idDelete = (int)dgvTaiKhoan.SelectedCells[0].Value;
            DialogResult dialogResult = MessageBox.Show("     BẠN CÓ CHẮC CHẮN  ", "  XÓA", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                // Sửa câu lệnh SQL với tên cột chính xác
                string query = $"DELETE FROM TaiKhoan WHERE ID = {idDelete}"; // Thay 'TenDangNhap' bằng tên cột chính xác
                QuanLySQL.NhapDLVaoSQL(query);
                LoadTKToDGV();
            }
        }

        private void btn_TaoMoi_Click(object sender, EventArgs e)
        {
            TaoMoiText();
        }
        private void TaoMoiText()
        {
            txtTenDangNhap.Text = "";
            txtHoTen.Text = "";
            txtSoDienThoai.Text = "";
            cb0VaiTro.SelectedIndex = -1;
            txt_CanCuocCongDan.Text = "";
            txt_NgayThoiViec.Text = "";
            txt_DiaChiThuongTru.Text = "";
            txt_NgaySinh.Text = "";
            txt_Email.Text = "";
            txt_LuuY.Text = "";
            btn_Xoa.Enabled = false;
            btn_CapNhat.Enabled = false;
            btn_Xoa.Enabled = false;
            btn_CapNhat.Enabled = false;
        }

        private void btn_DoiMatKhau_Click(object sender, EventArgs e)
        {
            using (FrmDoiMatKhau changePasswordForm = new FrmDoiMatKhau())
            {
                changePasswordForm.ID = dgvTaiKhoan.SelectedCells[0].Value.ToString();
                changePasswordForm.TenDangNhap = dgvTaiKhoan.SelectedCells[1].Value.ToString();
                changePasswordForm.ShowDialog();
            }
        }

       

        private void btn_Disable_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn hay không
            if (dgvTaiKhoan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một tài khoản.");
                return;
            }

            // Lấy chỉ số hàng hiện tại đang được chọn
            int selectedRowIndex = dgvTaiKhoan.CurrentCell.RowIndex;

            // Lấy giá trị của cột "TenDangNhap" và "Enable"
            string tenDangNhap = dgvTaiKhoan.Rows[selectedRowIndex].Cells["TenDangNhap"].Value.ToString();

            // Kiểm tra xem cột "Enable" có giá trị không và chuyển đổi thành bool
            object cellValue = dgvTaiKhoan.Rows[selectedRowIndex].Cells["Enable"].Value;

            if (cellValue == null || !(cellValue is bool))
            {
                MessageBox.Show("Trạng thái tài khoản không hợp lệ.");
                return;
            }

            // Lấy trạng thái hiện tại của tài khoản (Enable = true/false)
            bool isEnabled = (bool)cellValue;

            // Đảo ngược trạng thái tài khoản
            bool newStatus = !isEnabled;

            // Cập nhật trạng thái tài khoản trong cơ sở dữ liệu
            string sql = $"UPDATE TaiKhoan SET Enable = {(newStatus ? 1 : 0)} WHERE TenDangNhap = '{tenDangNhap}'";

            try
            {
                // Thực thi câu lệnh SQL để cập nhật trạng thái
                QuanLySQL.NhapDLVaoSQL(sql);

                MessageBox.Show($"{(newStatus ? "Enabled" : "Disabled")} tài khoản {tenDangNhap} thành công.");

                // Cập nhật trạng thái mới trên DataGridView
                dgvTaiKhoan.Rows[selectedRowIndex].Cells["Enable"].Value = newStatus;

                // Đổi text nút "Enable/Disable" theo trạng thái mới
                btn_Disable.Text = newStatus ? "Disable" : "Enable";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra: {ex.Message}");
            }
        }
    }


    } 



