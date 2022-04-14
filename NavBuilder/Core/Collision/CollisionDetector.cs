using NavBuilder.Core.GraphicsUtilities;
using SharpDX;
using System.Collections.Generic;
using System.Diagnostics;

namespace NavBuilder.Core.Collision
{
    public class CollisionDetector
    {
        public static Point HasCollisionBetween(Point source, Point destination, List<Line> obsctacles)
        {
            if (obsctacles == null) 
                return new Point();

            var lineTrace = new Line
            {
                Source = source,
                Destination = destination
            };

            var stopWatch = new Stopwatch();

            stopWatch.Start();
            foreach (var lineObstacle in obsctacles)
            {
                var intersection = LineIntersection.FindIntersection(lineTrace, lineObstacle);
                if (intersection.X != 0 && intersection.Y != 0)
                {
                    stopWatch.Stop();

                    Debug.WriteLine($"Found collision at {intersection.ToString()} in {stopWatch.ElapsedMilliseconds}ms ");
                    return intersection;
                }
            }

            return new Point();
        }
    }
}