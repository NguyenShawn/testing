using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace karaoke.UI
{
    internal class ButtonActive
    {
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
