using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication5.Model
{
    public class ArcDrawerModel
    {
        public Point StartPointLine1 { get; set; }
        public Point StopPointLine1 { get; set; }
        public Point StartPointLine2 { get; set; }
        public Point StopPointLine2 { get; set; }
        public Rectangle Rectangle1 { get; set; }
        public Rectangle Rectangle2 { get; set; }
        public float StartAngle { get; set; }
        public float SweepAngle { get; set; }
        public float StopAngle { get; set; }
        public Pen Pen { get; set; }
        public ArcType ArcType { get; set; }
    }

    public enum ArcType
    {
        HALF, FULL
    }
}
