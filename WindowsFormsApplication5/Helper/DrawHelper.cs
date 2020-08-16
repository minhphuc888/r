using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication5.Model;

namespace WindowsFormsApplication5.Helper
{
    /// <summary>
    /// Tách các hàm thường sử dụng sang đây cho gọn và tái sử dụng
    /// </summary>
    public static class DrawHelper
    {

        public static Point getPointDrawLine(float alpha, Point tamHinhTron, int r)
        {
            if (alpha >= 0 && alpha <= 180)
            {
                return new Point()
                {
                    X = tamHinhTron.X + (int)(r * Math.Sin(Math.PI * alpha / 180)),
                    Y = tamHinhTron.Y - (int)(r * Math.Cos(Math.PI * alpha / 180))
                };
            }
            else
            {
                return new Point()
                {
                    X = tamHinhTron.X - (int)(r * -Math.Sin(Math.PI * alpha / 180)),
                    Y = tamHinhTron.Y - (int)(r * Math.Cos(Math.PI * alpha / 180))
                };
            }
        }

        public static int GetAngle(Point centerPoint, Point currentPoint)
        {
            var x = currentPoint.X - centerPoint.X;
            var y = currentPoint.Y - centerPoint.Y;
            return (int)(Math.Atan2(y, x) * 180 / Math.PI);
        }


        public static int GetRadius(Point centerPoint, Point currentPoint)
        {
            var x = currentPoint.X - centerPoint.X;
            var y = currentPoint.Y - centerPoint.Y;

            return (int)Math.Sqrt(x * x + y * y);
        }

        public static bool IsOutBox(Point centerPoint, Point currentPoint, int limit)
        {
            return GetRadius(centerPoint, currentPoint) > limit;
        }
        public static float GetSweepAngle(float startAngle, float stopAngle)
        {
            var sweepAngle = startAngle - stopAngle;
            return sweepAngle < 0 ? sweepAngle * -1 : 360 - sweepAngle;
        }

        public static void ArcDrawer(this Graphics graphics, ArcDrawerModel arcDrawler)
        {
            graphics.DrawLine(arcDrawler.Pen, arcDrawler.StartPointLine1, arcDrawler.StopPointLine1);
            graphics.DrawLine(arcDrawler.Pen, arcDrawler.StartPointLine2, arcDrawler.StopPointLine2);

            graphics.DrawArc(arcDrawler.Pen, arcDrawler.Rectangle1, arcDrawler.StartAngle, arcDrawler.SweepAngle);
            graphics.DrawArc(arcDrawler.Pen, arcDrawler.Rectangle2, arcDrawler.StartAngle, arcDrawler.SweepAngle);
        }

        public static void ObjMoveDrawer(this Graphics graphics, ObjMoveModel objMove, Control control)
        {
            try
            {
                // if (control.Location == Point.Empty)
                // {
                //     control.Visible = true;
                ////     control.Location = objMove.StartPoint;
                //     control.Tag = objMove.StartPoint.X;
                //     return;
                // }
                if (control.Tag == null)
                {
                    control.Tag = objMove.StartPoint.X;
                    return;
                }

                // lay truc toa do tai vi tri start
                // y = ax
                var x = objMove.StopPoint.X - objMove.StartPoint.X;
                var y = objMove.StopPoint.Y - objMove.StartPoint.Y;

                double a = Math.Sqrt(y * y) / Math.Sqrt(x * x);
                if ((y < 0 && x > 0) || (x < 0 && y > 0))
                {
                    a = a * -1;
                }

                var oldPoint = (int)control.Tag;

                var x1 = oldPoint - objMove.StartPoint.X;
                x1 += objMove.StartPoint.X > objMove.StopPoint.X && objMove.StartPoint.Y < objMove.StopPoint.Y ? -1 : 1;

                var y1 = a * x1;

                var x2 = x1 - 20;
                // doi truc toa do ve 0
                x1 = objMove.StartPoint.X + x1;
                y1 = objMove.StartPoint.Y + y1;

                var y2 = a * x2;
                x2 = objMove.StartPoint.X + x2;
                y2 = objMove.StartPoint.Y + y2;
                // luu vet
                control.Tag = x1;

                // can chinh
                //x1 += 10;
                //y1 += 10;

                // control.Location = new Point(x1, (int)y1);

                //debugger
                graphics.DrawLine(new Pen(Color.OrangeRed, 5f), new Point(x2, (int)y2), new Point(x1, (int)y1));

                if (control.Location.X > objMove.StopPoint.X)
                {
                    // control.Location = objMove.StartPoint;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
