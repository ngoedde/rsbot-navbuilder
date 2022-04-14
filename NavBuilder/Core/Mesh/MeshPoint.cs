using NavBuilder.Core.Navmesh.Struct;
using SharpDX;
using System;

namespace NavBuilder.Core.Mesh
{
    internal class MeshPoint
    {
        #region Properties

        /// <summary>
        /// The position of this Point.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// The flag of this Point.
        /// </summary>
        public byte Flag;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the absolute position.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns></returns>
        public Vector2 GetPrefabPosition(NavMeshInst entry)
        {
            var vector2 = new Vector2(Position.X, Position.Z);

            vector2.X += entry.Position.X;
            vector2.Y += entry.Position.Z;

            return RotateVector(vector2, new Vector2(entry.Position.X, entry.Position.Z), entry.Angle.Degrees);
        }

        /// <summary>
        /// Rotates one point around another
        /// </summary>
        /// <param name="pointToRotate">The point to rotate.</param>
        /// <param name="centerPoint">The center point of rotation.</param>
        /// <param name="angleInDegrees">The rotation angle in degrees.</param>
        /// <returns>Rotated point</returns>
        private Vector2 RotateVector(Vector2 pointToRotate, Vector2 centerPoint, double angleInDegrees)
        {
            var angleInRadians = angleInDegrees * (Math.PI / 180);
            var cosTheta = Math.Cos(angleInRadians);
            var sinTheta = Math.Sin(angleInRadians);
            return new Vector2
            {
                X =
                    (int)
                    (cosTheta * (pointToRotate.X - centerPoint.X) -
                     sinTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.X),
                Y =
                    (int)
                    (sinTheta * (pointToRotate.X - centerPoint.X) +
                     cosTheta * (pointToRotate.Y - centerPoint.Y) + centerPoint.Y)
            };
        }

        #endregion Methods
    }
}