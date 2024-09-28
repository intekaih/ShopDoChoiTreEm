using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project01.Class
{
    class TrangTri
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

        public static void ThemHieuUngHighlight(Control control, int banKinhGoc, int doDaiViend)
        {
            bool isMouseHovering = false;

            control.MouseEnter += (sender, e) =>
            {
                isMouseHovering = true;
                control.Invalidate(); // Yêu cầu vẽ lại để hiển thị viền
            };

            control.MouseLeave += (sender, e) =>
            {
                isMouseHovering = false;
                control.Invalidate(); // Yêu cầu vẽ lại để xóa viền
            };

            control.Paint += (sender, e) =>
            {
                if (isMouseHovering) // Chỉ vẽ viền khi chuột đang ở trong control
                {
                    using (Pen pen = new Pen(Color.LightPink, 3))
                    {
                        using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                        {
                            path.AddArc(0, 0, banKinhGoc, banKinhGoc, 180, 90);
                            path.AddArc(control.Width - banKinhGoc, 0, banKinhGoc, banKinhGoc, 270, 90);
                            path.AddArc(control.Width - banKinhGoc, control.Height - banKinhGoc, banKinhGoc, banKinhGoc, 0, 90);
                            path.AddArc(0, control.Height - banKinhGoc, banKinhGoc, banKinhGoc, 90, 90);
                            path.CloseAllFigures();

                            // Vẽ viền bo góc
                            e.Graphics.DrawPath(pen, path);

                            // Vẽ viền bên dưới và bên phải
                            e.Graphics.DrawLine(pen, control.Width - doDaiViend, -doDaiViend, control.Width - doDaiViend, control.Height - doDaiViend);
                            e.Graphics.DrawLine(pen, -doDaiViend, control.Height - doDaiViend, control.Width - doDaiViend, control.Height - doDaiViend);
                        }
                    }
                }
            };
        }

    }
}

