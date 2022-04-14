using NavBuilder.Client;
using NavBuilder.Core.Navmesh.Struct;
using SharpDX;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NavBuilder.Core.Mesh
{
    internal class MeshObject
    {
        #region Properties

        public RoadShark.Pk2.Types.PK2File Pk2File { get; private set; }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <value>
        /// The filename.
        /// </value>
        public string Filename { get; private set; }

        /// <summary>
        /// Gets the bounding box.
        /// </summary>
        /// <value>
        /// The bounding box.
        /// </value>
        public BoundingBox BoundingBox { get; private set; }

        /// <summary>
        /// Gets the outlines.
        /// </summary>
        /// <value>
        /// The outlines.
        /// </value>
        public MeshLine[] Outlines { get; private set; }

        /// <summary>
        /// Gets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public MeshPoint[] Points { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Load(string fileName)
        {
            if (!Pk2Controller.Data.FileExists(fileName))
            {
                Debug.WriteLine($"Could not find mesh file {fileName}, skipping it!");
                return;
            }

            Debug.WriteLine($"Loading mesh file {fileName}...");

            Pk2File = Pk2Controller.Data.GetFile(fileName);

            using (var reader = new BinaryReader(Pk2File.GetStream()))
            {
                var header = reader.ReadBytes(12);
                if (Encoding.ASCII.GetString(header) != "JMXVBMS 0110")
                {
                    Debug.WriteLine($"The mesh file {fileName} has an invalid header, skipping it.");
                    return;
                }

                Filename = fileName;

                reader.ReadUInt32(); //Vertex Pointer
                reader.ReadUInt32(); //Skin Pointer
                reader.ReadUInt32(); //Face Pointer
                reader.ReadUInt32(); //Cloth Pointer
                reader.ReadUInt32(); //Cloth edge Pointer

                var boundingBoxPointer = reader.ReadUInt32();
                reader.ReadUInt32(); //Gate Pointer
                var navMeshPointer = reader.ReadUInt32();
                reader.ReadUInt32(); //Unknown Pointer
                reader.ReadUInt32(); //Unknown Pointer
                reader.ReadUInt32(); //Unknown data
                var navFlag = reader.ReadUInt32();

                reader.BaseStream.Position = boundingBoxPointer;
                BoundingBox = reader.ReadStruct<BoundingBox>();

                //Does not have collision
                if (navMeshPointer == 0)
                    return;

                reader.BaseStream.Position = navMeshPointer;

                //Not interesting for collision stuff, skipping it
                var pointsCount = reader.ReadUInt32();
                Points = new MeshPoint[pointsCount];
                for (var i = 0; i < pointsCount; i++)
                {
                    var meshPoint = new MeshPoint
                    {
                        Position = reader.ReadStruct<Vector3>(),
                        Flag = reader.ReadByte()
                    };

                    Points[i] = meshPoint;
                }

                var groundsCount = reader.ReadUInt32();
                for (var i = 0; i < groundsCount; i++)
                {
                    reader.ReadUInt16(); //Triangle A
                    reader.ReadUInt16(); //Triangle B
                    reader.ReadUInt16(); //Triangle C
                    reader.ReadUInt16(); //Unused D

                    if ((navFlag & 2) == 2)
                        reader.ReadByte(); //unk
                }

                var outlineCount = reader.ReadUInt32();
                Outlines = new MeshLine[outlineCount];

                for (var i = 0; i < outlineCount; i++)
                {
                    var outline = new MeshLine
                    {
                        PointIndexA = reader.ReadUInt16(),
                        PointIndexB = reader.ReadUInt16(),
                    };

                    reader.ReadUInt16(); //Neighbour Point index A
                    reader.ReadUInt16(); //Neighbour Point index B

                    outline.Flag = (NavEdgeFlag)reader.ReadByte();

                    if ((navFlag & 1) == 1)
                        reader.ReadByte();

                    Outlines[i] = outline;
                }
            }

        }

        #endregion Methods
    }
}