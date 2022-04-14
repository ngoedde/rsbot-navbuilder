using NavBuilder.Core.Navmesh.IO;
using SharpDX;
using System.Collections.Generic;

namespace NavBuilder.Core.Navmesh.Struct
{
    internal class NavCellQuad : NavCell, INavData
    {
        #region Properties

        /// <summary>
        /// Gets or sets the rectangle.
        /// </summary>
        /// <value>
        /// The rectangle.
        /// </value>
        public RectangleF Rectangle { get; set; }

        /// <summary>
        /// Gets the instances.
        /// </summary>
        /// <value>
        /// The instances.
        /// </value>
        public List<ushort> Instances { get; } = new List<ushort>();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Reads the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void Read(NavMeshReader reader)
        {
            Rectangle = reader.ReadRectangleF();

            var instCount = reader.ReadByte();
            Instances.Capacity = instCount;
            for (var i = 0; i < instCount; i++)
                Instances.Add(reader.ReadUInt16());
        }

        #endregion Methods
    }
}