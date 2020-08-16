using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5.Helper
{
    public static class VeDoiTuongDiChuyenHelper
    {
        private static Form mMask;
        private static ObjMoveModel objMove = new ObjMoveModel();
        public static ObjMoveModel Draw(PictureBox pictureBox)
        {
            objMove.StartPoint = new Point();
            objMove.StopPoint = new Point();

            // Create a transparent form on top of <frm>
            mMask = new Form();
            mMask.FormBorderStyle = FormBorderStyle.None;
            mMask.BackColor = Color.Magenta;
            mMask.TransparencyKey = mMask.BackColor;
            mMask.ShowInTaskbar = false;
            mMask.StartPosition = FormStartPosition.Manual;
            mMask.Size = pictureBox.Size;
            mMask.Location = pictureBox.PointToScreen(Point.Empty);
            mMask.MouseMove += MouseMove;
            mMask.MouseUp += MouseUp;
            mMask.Paint += MMask_Paint;
            mMask.Load += DoCapture;

            // debugger
            //var p = new Label { Text = "Debugger" };
            //mMask.Controls.Add(p);

            // Display the overlay
            mMask.ShowDialog(pictureBox);
            // Clean-up and calculate return value
            mMask.Dispose();
            mMask = null;

            return objMove;
        }

        private static void MMask_Paint(object sender, PaintEventArgs e)
        {
            if (objMove.StartPoint == Point.Empty)
            {
                objMove.StartPoint = mMask.PointToClient(Control.MousePosition);
                return;
            }
            // Draw the current rectangle
            objMove.StopPoint = mMask.PointToClient(Control.MousePosition);

            e.Graphics.DrawLine(new Pen(Color.Red), objMove.StartPoint, objMove.StopPoint);
        }

        private static void DoCapture(object sender, EventArgs e)
        {
            // Grab the mouse
            mMask.Capture = true;
        }
        private static void MouseMove(object sender, MouseEventArgs e)
        {
            // Repaint the rectangle
            mMask.Invalidate();
        }
        private static void MouseUp(object sender, MouseEventArgs e)
        {
            // Done, close mask
            mMask.Close();
            // Paint();
        }
    }

    public class ObjMoveModel
    {
        public Point StartPoint { get; set; }
        public Point StopPoint { get; set; }
    }
}
