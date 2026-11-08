using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace karaoke.UI
{
    internal class ButtonActive
    {
        public static void RoundButton(Button button)
        {
            int diameter = Math.Min(16, Math.Min(button.Width, button.Height));
            if (diameter <= 0) return;

            using (var path = new GraphicsPath())
            {
                path.AddArc(0, 0, diameter, diameter, 180, 90);
                path.AddArc(button.Width - diameter, 0, diameter, diameter, 270, 90);
                path.AddArc(button.Width - diameter, button.Height - diameter, diameter, diameter, 0, 90);
                path.AddArc(0, button.Height - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();
                var previous = button.Region;
                button.Region = new Region(path);
                if (previous != null) previous.Dispose();
            }
        }

        public static void SetActiveButton(
            Guna2Button activeButton,
            params Guna2Button[] buttons)
        {
            foreach (Guna2Button btn in buttons)
            {
                if (btn != null)
                {
                    btn.Checked = (btn == activeButton);
                }
            }
        }
    }
}
