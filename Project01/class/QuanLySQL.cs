using System;
using System.Data;
using System.Data.SqlClient;
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
                @"server=INTEKAIH\TIENKHAI; database = ShopDoChoi; Integrated Security = true; ";

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
            if (connection.State == ConnectionState.Open)
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
        public static void NhapDLVaoSQL(string query, params SqlParameter[] parameters)
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    // Thêm các tham số vào SqlCommand
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

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

        public static bool KiemTraHangHoaTonTai(string tenHang, int maLoaiHang)
        {
            string query = "SELECT COUNT(*) FROM QuanLyHang WHERE TenHang = @TenHang AND MaLoaiHang = @MaLoaiHang";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TenHang", tenHang);
                command.Parameters.AddWithValue("@MaLoaiHang", maLoaiHang);

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
    }

}
