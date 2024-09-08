using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project01.Class
{
    class XuLyVien
    {
        

        public class XuLyVienTextBox : TextBox
        {
            private Panel underlinePanel;

            public XuLyVienTextBox(int doDam)
            {
                // Xóa viền mặc định của TextBox
                this.BorderStyle = BorderStyle.None;

                // Tạo panel để làm viền dưới
                underlinePanel = new Panel
                {
                    BackColor = Color.Gray,
                    Height = doDam, // Chiều cao của viền dưới
                    Dock = DockStyle.Bottom,
                    Width = this.Width // Đặt chiều rộng của panel bằng chiều rộng của TextBox
                };

                this.Controls.Add(underlinePanel); // Thêm panel vào TextBox
                this.Resize += (s, e) => underlinePanel.Width = this.Width; // Đảm bảo panel có chiều rộng phù hợp khi TextBox thay đổi kích thước
            }


        }
    }
}
