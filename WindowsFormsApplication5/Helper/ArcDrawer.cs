using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WindowsFormsApplication5.Model;

namespace WindowsFormsApplication5.Helper
{
    public static class ArcDrawer
    {
        private static Form mMask;
        private static Point mPos;
        private static Point pos;
        private static PictureBox pBox;
        private static Point _centerPoint;

        private static ArcDrawerModel arcDrawer;

        private static ArcType _arcType;

        public static ArcDrawerModel Draw(PictureBox pictureBox, Point centerPoint, ArcType arcType)
        {
            // Record the start point
            mPos = pictureBox.PointToClient(Control.MousePosition);
            pBox = pictureBox;
            _centerPoint = centerPoint;
            _arcType = arcType;

            arcDrawer = new ArcDrawerModel();
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
            mMask.Paint += PaintArc;
            mMask.Load += DoCapture;
            // Display the overlay
            mMask.ShowDialog(pictureBox);
            // Clean-up and calculate return value
            mMask.Dispose();
            mMask = null;

            return arcDrawer;
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

        private static void PaintArc(object sender, PaintEventArgs e)
        {
            // Draw the current rectangle
            pos = mMask.PointToClient(Control.MousePosition);

            arcDrawer.StartAngle = DrawHelper.GetAngle(_centerPoint, mPos);
            arcDrawer.StopAngle = DrawHelper.GetAngle(_centerPoint, pos);
            arcDrawer.SweepAngle = DrawHelper.GetSweepAngle(arcDrawer.StartAngle, arcDrawer.StopAngle);


            int radius1 = DrawHelper.GetRadius(_centerPoint, mPos);
            int radius2 = DrawHelper.GetRadius(_centerPoint, pos);

            // với phần vẽ full hình cung, bán kính của kình cung sẽ bằng bán kính của hình tròn và bằng tâm điểm của hình tròn
            if (_arcType == ArcType.FULL)
            {
                radius1 = _centerPoint.X;
                radius2 = _centerPoint.X;

                arcDrawer.Pen = new Pen(Brushes.Red);
                arcDrawer.Pen.DashStyle = DashStyle.Solid;

                arcDrawer.StopPointLine1 = _centerPoint;
                arcDrawer.StopPointLine2 = _centerPoint;
            }
            else
            {
                arcDrawer.Pen = new Pen(Brushes.White);
                arcDrawer.Pen.DashStyle = DashStyle.Dot;

                arcDrawer.StopPointLine1 = DrawHelper.getPointDrawLine(arcDrawer.StartAngle + 90, _centerPoint, radius2);
                arcDrawer.StopPointLine2 = DrawHelper.getPointDrawLine(arcDrawer.StopAngle + 90, _centerPoint, radius2);
            }

            arcDrawer.StartPointLine1 = DrawHelper.getPointDrawLine(arcDrawer.StartAngle + 90, _centerPoint, radius1);
            arcDrawer.StartPointLine2 = DrawHelper.getPointDrawLine(arcDrawer.StopAngle + 90, _centerPoint, radius1);

            arcDrawer.Rectangle1 = new Rectangle() { Height = radius1 * 2, Width = radius1 * 2, X = _centerPoint.X - radius1, Y = _centerPoint.Y - radius1 };
            arcDrawer.Rectangle2 = new Rectangle() { Height = radius2 * 2, Width = radius2 * 2, X = _centerPoint.X - radius2, Y = _centerPoint.Y - radius2 };

            e.Graphics.ArcDrawer(arcDrawer);
        }
    }
}
