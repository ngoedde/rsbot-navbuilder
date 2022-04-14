using NavBuilder.Core.Navmesh.IO;
using SharpDX;

namespace NavBuilder.Core.Navmesh.Struct
{
    internal abstract class NavEdge : INavData
    {
        #region Properties

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public Vector2 Min { get; set; }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public Vector2 Max { get; set; }

        /// <summary>
        /// Gets or sets the flag.
        /// </summary>
        /// <value>
        /// The flag.
        /// </value>
        public NavEdgeFlag Flag { get; set; }

        /// <summary>
        /// Gets or sets the direction source.
        /// </summary>
        /// <value>
        /// The direction source.
        /// </value>
        public NavEdgeDirection DirectionSource { get; set; }

        /// <summary>
        /// Gets or sets the direction destination.
        /// </summary>
        /// <value>
        /// The direction DST.
        /// </value>
        public NavEdgeDirection DirectionDestination { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Reads the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public virtual void Read(NavMeshReader reader)
        {
            Min = reader.ReadVector2();
            Max = reader.ReadVector2();

            Flag = (NavEdgeFlag)reader.ReadByte();
            DirectionSource = (NavEdgeDirection)reader.ReadByte();
            DirectionDestination = (NavEdgeDirection)reader.ReadByte();
        }

        #endregion Methods
    }
}