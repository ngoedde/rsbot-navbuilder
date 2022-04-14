using NavBuilder.Core.Navmesh.Struct;
using System;
using NavBuilder.Core.Navmesh;

namespace NavBuilder.Core.Map
{
    public class Region
    {
        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public ushort Id { get; set; }

        /// <summary>
        /// Gets or sets the x sector.
        /// </summary>
        /// <value>
        /// The x sector.
        /// </value>
        public byte XSector => BitConverter.GetBytes(Id)[0];

        /// <summary>
        /// Gets or sets the y sector.
        /// </summary>
        /// <value>
        /// The y sector.
        /// </value>
        public byte YSector => BitConverter.GetBytes(Id)[1];

        /// <summary>
        /// Gets the name of the image file.
        /// </summary>
        /// <value>
        /// The name of the image file.
        /// </value>
        public string ImageFileName => $"{XSector}x{YSector}.ddj";

        /// <summary>
        /// Gets the name of the navmesh file.
        /// </summary>
        /// <value>
        /// The name of the navmesh file.
        /// </value>
        public string NavmeshFileName => $"nv_{Id:x4}.nvm";

        /// <summary>
        /// Gets the terrain.
        /// </summary>
        /// <value>
        /// The terrain.
        /// </value>
        internal NavMeshTerrain Terrain
        {
            get
            {
                if (_terrain != null)
                    return _terrain;

                _terrain = NavMeshManager.LoadNavmesh(NavmeshFileName);

                return _terrain;
            }
        }

        #endregion Properties

        #region Fields

        private NavMeshTerrain _terrain;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Region(ushort id)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/> class.
        /// </summary>
        /// <param name="xSector">The x sector.</param>
        /// <param name="ySector">The y sector.</param>
        public Region(byte xSector, byte ySector)
        {
            Id = BitConverter.ToUInt16(new[] { xSector, ySector }, 0);
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Gets the surrounding regions.
        /// </summary>
        /// <returns></returns>
        public Region[] GetSurroundingRegions()
        {
            var result = new Region[9];

            //1. Top
            result[0] = new Region((byte)(XSector - 1), (byte)(YSector + 1));
            result[1] = new Region(XSector, (byte)(YSector + 1));
            result[2] = new Region((byte)(XSector + 1), (byte)(YSector + 1));

            //2. Middle
            result[3] = new Region((byte)(XSector - 1), YSector);
            result[4] = this;
            result[5] = new Region((byte)(XSector + 1), YSector);

            //3. Bottom
            result[6] = new Region((byte)(XSector - 1), (byte)(YSector - 1));
            result[7] = new Region(XSector, (byte)(YSector - 1));
            result[8] = new Region((byte)(XSector + 1), (byte)(YSector - 1));

            return result;
        }

        #endregion Methods
    }
}