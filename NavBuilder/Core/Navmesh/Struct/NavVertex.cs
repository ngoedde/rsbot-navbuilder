using NavBuilder.Core.Navmesh.IO;
using SharpDX;

namespace NavBuilder.Core.Navmesh.Struct
{
    internal class NavVertex : INavData
    {
        #region Properties

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Vector3 Position { get; private set; }

        /// <summary>
        /// Gets the byte0.
        /// </summary>
        /// <value>
        /// The byte0.
        /// </value>
        public byte Byte0 { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Reads the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void Read(NavMeshReader reader)
        {
            Position = reader.ReadVector3();
            Byte0 = reader.ReadByte();
        }

        #endregion Methods
    }
}