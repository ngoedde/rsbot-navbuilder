using NavBuilder.Core.Mesh;
using NavBuilder.Core.Navmesh.IO;
using SharpDX;
using System.Collections.Generic;

namespace NavBuilder.Core.Navmesh.Struct
{
    internal class NavMeshInst : INavData
    {
        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public NavMeshInstType Type { get; set; }

        /// <summary>
        /// Gets or sets the angle.
        /// </summary>
        /// <value>
        /// The angle.
        /// </value>
        public AngleSingle Angle { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public ushort UniqueId { get; set; }

        /// <summary>
        /// Gets or sets the byte0.
        /// </summary>
        /// <value>
        /// The byte0.
        /// </value>
        public byte Byte0 { get; set; } //might need to be merged -> weird data.

        /// <summary>
        /// Gets or sets the byte1.
        /// </summary>
        /// <value>
        /// The byte1.
        /// </value>
        public byte Byte1 { get; set; } //might need to be merged -> weird data.

        /// <summary>
        /// Gets or sets a value indicating whether this instance is large.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is large; otherwise, <c>false</c>.
        /// </value>
        public bool IsLarge { get; set; } //Exceeds a region (Jangan Walls etc..)

        /// <summary>
        /// Gets or sets a value indicating whether this instance is structure.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is structure; otherwise, <c>false</c>.
        /// </value>
        public bool IsStructure { get; set; } //requires objectstring.ifo entry (eventzone?)

        /// <summary>
        /// Gets or sets the region identifier.
        /// </summary>
        /// <value>
        /// The region identifier.
        /// </value>
        public ushort RegionId { get; set; }

        /// <summary>
        /// Gets or sets the mounts.
        /// </summary>
        /// <value>
        /// The mounts.
        /// </value>
        public List<NavMeshInstMount> Mounts { get; set; } = new List<NavMeshInstMount>();

        /// <summary>
        /// Gets the resource.
        /// </summary>
        /// <value>
        /// The resource.
        /// </value>
        public Resource Resource => ResourceManager.LoadResource(Id);

        #endregion Properties

        #region Methods

        /// <summary>
        /// Reads the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void Read(NavMeshReader reader)
        {
            Id = reader.ReadInt32();

            Position = reader.ReadVector3();
            Type = (NavMeshInstType)reader.ReadUInt16();
            Angle = reader.ReadAngle();
            UniqueId = reader.ReadUInt16();

            Byte0 = reader.ReadByte();
            Byte1 = reader.ReadByte();

            IsLarge = reader.ReadBoolean();
            IsStructure = reader.ReadBoolean();
            RegionId = reader.ReadUInt16();

            var mountCnt = reader.ReadInt16();
            Mounts.Capacity = mountCnt;

            for (var i = 0; i < mountCnt; i++)
                Mounts.Add(reader.ReadData<NavMeshInstMount>());
        }

        #endregion Methods
    }
}