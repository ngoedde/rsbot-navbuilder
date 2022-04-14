using NavBuilder.Core.Navmesh.IO;

namespace NavBuilder.Core.Navmesh.Struct
{
    internal class NavEdgeInternal : NavEdge
    {
        #region Properties

        /// <summary>
        /// Gets or sets the cell source.
        /// </summary>
        /// <value>
        /// The cell source.
        /// </value>
        public ushort CellSource { get; set; }

        /// <summary>
        /// Gets or sets the cell destination.
        /// </summary>
        /// <value>
        /// The cell destination.
        /// </value>
        public ushort CellDestination { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance has neighbour cell.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has neighbour cell; otherwise, <c>false</c>.
        /// </value>
        public bool HasNeighbourCell => CellSource == ushort.MaxValue || CellDestination == ushort.MaxValue;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Reads the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override void Read(NavMeshReader reader)
        {
            base.Read(reader);

            CellSource = reader.ReadUInt16();
            CellDestination = reader.ReadUInt16();
        }

        #endregion Methods
    }
}