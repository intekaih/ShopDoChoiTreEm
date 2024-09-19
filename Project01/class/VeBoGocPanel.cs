using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project01.Class
{
    class VeBoGocPanel
    {

        // Phương thức để bo góc cho Panel
        public static void BoGocPanel(Panel panel, int banKinhGoc)
        {
            // Tạo đối tượng GraphicsPath để vẽ các đường viền bo góc
            System.Drawing.Drawing2D.GraphicsPath duongVien = new System.Drawing.Drawing2D.GraphicsPath();

            // Vẽ góc trên trái
            duongVien.AddArc(0, 0, banKinhGoc, banKinhGoc, 180, 90);

            // Vẽ góc trên phải
            duongVien.AddArc(panel.Width - banKinhGoc, 0, banKinhGoc, banKinhGoc, 270, 90);

            // Vẽ góc dưới phải
            duongVien.AddArc(panel.Width - banKinhGoc, panel.Height - banKinhGoc, banKinhGoc, banKinhGoc, 0, 90);

            // Vẽ góc dưới trái
            duongVien.AddArc(0, panel.Height - banKinhGoc, banKinhGoc, banKinhGoc, 90, 90);

            // Kết thúc các đoạn đường vẽ và hoàn tất hình dạng
            duongVien.CloseAllFigures();

            // Thiết lập vùng bo góc cho Panel
            panel.Region = new Region(duongVien);
        }

        

    }

}

