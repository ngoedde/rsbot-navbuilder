using NavBuilder.Client;
using RoadShark.Pk2.Types;
using SharpDX;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NavBuilder.Core.Mesh
{
    internal class Resource
    {
        #region Properties

        public PK2File Pk2File { get; private set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ResourceType Type { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the mesh path.
        /// </summary>
        /// <value>
        /// The mesh path.
        /// </value>
        public string MeshPath { get; set; }

        /// <summary>
        /// Gets or sets the bounding box.
        /// </summary>
        /// <value>
        /// The bounding box.
        /// </value>
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets the oriented bounding box.
        /// </summary>
        /// <value>
        /// The oriented bounding box.
        /// </value>
        public OrientedBoundingBox OrientedBoundingBox { get; set; }

        /// <summary>
        /// Gets the mesh.
        /// </summary>
        /// <value>
        /// The mesh.
        /// </value>
        public MeshObject Mesh => MeshManager.LoadMeshObject(Path.GetFileName(MeshPath));

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
                Debug.WriteLine($"Could not find resource file {fileName}, skipping it.");
                return;
            }

            Pk2File = Pk2Controller.Data.GetFile(fileName);

            Debug.WriteLine($"Loading resource file {fileName}...");

            using (var reader = new BinaryReader(Pk2File.GetStream()))
            {
                var header = reader.ReadBytes(12);
                if (Encoding.ASCII.GetString(header) != "JMXVRES 0109")
                {
                    Debug.WriteLine($"The resource file {fileName} has an invalid header, skipping it.");
                    return;
                }

                //Pointers inside the same file
                reader.ReadUInt32(); //Material
                reader.ReadUInt32(); //Mesh
                reader.ReadUInt32(); //Skeleton
                reader.ReadUInt32(); //Animation
                reader.ReadUInt32(); //Mesh group
                reader.ReadUInt32(); //Animation group
                reader.ReadUInt32(); //Sound effect
                var boundingBoxPointer = reader.ReadUInt32();

                reader.ReadBytes(20); //Unknown flags

                Type = (ResourceType)reader.ReadUInt32();
                Name = reader.ReadJoymaxString();

                reader.ReadBytes(48); //Padding or unknown buffer

                if (Type == ResourceType.Building
                    || Type == ResourceType.Artifact
                    || Type == ResourceType.Nature
                    || Type == ResourceType.CompoundObject)
                {
                    reader.BaseStream.Position = boundingBoxPointer;
                    MeshPath = reader.ReadJoymaxString();
                    BoundingBox = reader.ReadStruct<BoundingBox>();
                    OrientedBoundingBox = reader.ReadStruct<OrientedBoundingBox>();
                }
            }
        }

        #endregion Methods
    }
}