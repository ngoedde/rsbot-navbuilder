using NavBuilder.Core.Navmesh.IO;

namespace NavBuilder.Core.Navmesh.Struct
{
    internal class NavEdgeGlobal : NavEdgeInternal
    {
        #region Properties

        /// <summary>
        /// Gets or sets the region source.
        /// </summary>
        /// <value>
        /// The region source.
        /// </value>
        public ushort RegionSource { get; set; }

        /// <summary>
        /// Gets or sets the region destination.
        /// </summary>
        /// <value>
        /// The region destination.
        /// </value>
        public ushort RegionDestination { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has neighbour region.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has neighbour region; otherwise, <c>false</c>.
        /// </value>
        public bool HasNeighbourRegion => RegionSource == ushort.MaxValue || RegionDestination == ushort.MaxValue;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Reads the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override void Read(NavMeshReader reader)
        {
            base.Read(reader);

            RegionSource = reader.ReadUInt16();
            RegionDestination = reader.ReadUInt16();
        }

        #endregion Methods
    }
}