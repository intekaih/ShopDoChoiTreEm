using ClosedXML.Excel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Project01.Class
{


    public static class QuanLySQL
    {

        // Kết nối SQL
        private static SqlConnection connection;

        // Phương thức mở kết nối
        public static void KetNoi()
        {
            connection = new SqlConnection();
            connection.ConnectionString =
                @"server=INTEKAIH\TIENKHAI; database = ShopDoChoiTreEm; Integrated Security = true; ";

            if (connection.State != ConnectionState.Open)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối SQL: " + ex.Message);
                }
            }
        }

        // Phương thức ngắt kết nối
        public static void NgatKetNoi()
        {
            if (connection != null && connection.State == ConnectionState.Open)  // Kiểm tra connection có null không
            {
                try
                {
                    connection.Close();
                    connection.Dispose();
                    connection = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi ngắt kết nối SQL: " + ex.Message);
                }
            }
        }


        // Phương thức lấy dữ liệu từ SQL
        public static DataTable XuatDLTuSQL(string query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message);
            }

            return dataTable;
        }

        // Phương thức lưu dữ liệu vào SQL
        public static void NhapDLVaoSQL(string query)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    // Thực thi câu lệnh SQL
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thực thi câu lệnh: " + ex.Message);
                }
            }
        }

        public static bool KiemTraDangNhap(string taiKhoan, string matKhau)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan = @TaiKhoan AND MatKhau = @MatKhau";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TaiKhoan", taiKhoan);
            command.Parameters.AddWithValue("@MatKhau", matKhau);

            try
            {
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kiểm tra đăng nhập: " + ex.Message);
                return false;
            }
        }

        public static bool KTtontai(string bang, string ten, int id)
        {
            string query = $"SELECT COUNT(*) FROM [{bang}] WHERE Ten = '{ten}' AND LoaiID = {id}";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kiểm tra hàng hóa: " + ex.Message);
                    return false;
                }
            }
        }

        public static int KTVaThemGT(string tenBang, string tenCot, string giaTri)
        {
            // Truy vấn kiểm tra sự tồn tại của giá trị trong bảng
            DataTable dt = XuatDLTuSQL($"SELECT ID FROM {tenBang} WHERE {tenCot} = N'{giaTri}'");

            if (dt.Rows.Count == 0)
            {
                // Nếu không tồn tại, thêm giá trị vào bảng
                string query = $"INSERT INTO {tenBang} ({tenCot}, Enable) VALUES (N'{giaTri}', 1)";
                NhapDLVaoSQL(query);

                // Cập nhật lại ID của giá trị từ cơ sở dữ liệu
                dt = XuatDLTuSQL($"SELECT ID FROM {tenBang} WHERE {tenCot} = N'{giaTri}'");
            }

            // Trả về ID của giá trị
            return Convert.ToInt32(dt.Rows[0]["ID"]);
        }



    }

}
