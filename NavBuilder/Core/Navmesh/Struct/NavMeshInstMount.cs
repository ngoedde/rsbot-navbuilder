using NavBuilder.Core.Navmesh.IO;

namespace NavBuilder.Core.Navmesh.Struct
{
    internal class NavMeshInstMount : INavData
    {
        #region Properties

        /// <summary>
        /// Gets or sets the short0.
        /// </summary>
        /// <value>
        /// The short0.
        /// </value>
        public short Short0 { get; set; } //-1 = ANY,

        /// <summary>
        /// Gets or sets the short1.
        /// </summary>
        /// <value>
        /// The short1.
        /// </value>
        public short Short1 { get; set; } //-1 = ANY,

        /// <summary>
        /// Gets or sets the short2.
        /// </summary>
        /// <value>
        /// The short2.
        /// </value>
        public short Short2 { get; set; } //-1 = ANY,

        #endregion Properties

        #region Methods

        /// <summary>
        /// Reads the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void Read(NavMeshReader reader)
        {
            Short0 = reader.ReadInt16();
            Short1 = reader.ReadInt16();
            Short2 = reader.ReadInt16();
        }

        #endregion Methods
    }
}