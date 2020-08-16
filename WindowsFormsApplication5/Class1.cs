using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    class Class1
    {
        public static void showcontrol(System.Windows.Forms.Control control, System.Windows.Forms.Control panelchinh)
        {
            panelchinh.Controls.Clear();
            control.Dock = DockStyle.Fill;
            control.BringToFront();
            control.Focus();
            panelchinh.Controls.Add(control);
        }
    }
   
}
