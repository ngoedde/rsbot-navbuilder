using NavBuilder.Core.Navmesh.IO;

namespace NavBuilder.Core.Navmesh.Struct
{
    internal class NavCellTri : NavCell, INavData<NavFlag>
    {
        #region Properties

        /// <summary>
        /// Gets a.
        /// </summary>
        /// <value>
        /// a.
        /// </value>
        public ushort A { get; private set; }

        /// <summary>
        /// Gets the b.
        /// </summary>
        /// <value>
        /// The b.
        /// </value>
        public ushort B { get; private set; }

        /// <summary>
        /// Gets the c.
        /// </summary>
        /// <value>
        /// The c.
        /// </value>
        public ushort C { get; private set; }

        /// <summary>
        /// Gets the d.
        /// </summary>
        /// <value>
        /// The d.
        /// </value>
        public ushort D { get; private set; }

        /// <summary>
        /// Gets or sets the byte0.
        /// </summary>
        /// <value>
        /// The byte0.
        /// </value>
        public byte Byte0 { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Reads the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="arg0">The arg0.</param>
        public void Read(NavMeshReader reader, NavFlag arg0)
        {
            A = reader.ReadUInt16();
            B = reader.ReadUInt16();
            C = reader.ReadUInt16();
            D = reader.ReadUInt16();

            if ((arg0 & NavFlag.Cell) == NavFlag.Cell)
                Byte0 = reader.ReadByte();
        }

        #endregion Methods

    }
}