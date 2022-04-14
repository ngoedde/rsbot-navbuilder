using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavBuilder.UI
{
    public static class SharpDXExtensions
    {
        public static System.Drawing.Point ToDrawingPoint(this SharpDX.Point point)
        {
            return new System.Drawing.Point(point.X, point.Y);
        }

        public static SharpDX.Point ToDXPoint(this System.Drawing.Point point)
        {
            return new SharpDX.Point(point.X, point.Y);
        }
    }
}
