using NavBuilder.Client;
using NavBuilder.Core.Navmesh.IO;
using RoadShark.Pk2.Types;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace NavBuilder.Core.Navmesh.Struct
{
    internal class NavMeshTerrain : NavMesh
    {
        #region Constants
        public PK2File Pk2File { get; set; }
        public const int BlocksX = 6;
        public const int BlocksZ = 6;
        //public const int BlocksTotal = BlocksX * BlocksZ;

        public const int CellsX = 96;
        public const int CellsZ = 96;
        //public const int CellsTotal = CellsX * CellsZ;

        public const int VerticiesX = CellsX + 1;
        public const int VerticiesZ = CellsZ + 1;
        //public const int VerticiesTotal = VerticiesX * VerticiesZ;

        public List<NavMeshInst> Objects => _objectList;

        public List<NavCellQuad> Cells => _cellList;

        public List<NavEdgeGlobal> GlobalEdges => _globalEdgeList;

        public List<NavEdgeInternal> InternalEdges => _internalEdgeList;

        #endregion Constants

        private readonly List<NavMeshInst> _objectList = new List<NavMeshInst>();
        private readonly List<NavCellQuad> _cellList = new List<NavCellQuad>();
        private readonly List<NavEdgeGlobal> _globalEdgeList = new List<NavEdgeGlobal>();
        private readonly List<NavEdgeInternal> _internalEdgeList = new List<NavEdgeInternal>();
        //private readonly float[] _heightMap = new float[VerticiesTotal];
        //private readonly NavWater[] _waterMap = new NavWater[BlocksTotal];

        #region Methods

        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public override void Load(string fileName)
        {
            if (!Pk2Controller.Data.FileExists(fileName)) return;

            Pk2File = Pk2Controller.Data.GetFile(fileName);

            using (var reader = new NavMeshReader(Pk2File.GetStream()))
            {
                var signature = reader.ReadString(12);
                Debug.Assert(signature == "JMXVNVM 1000");

                //Objects
                var objectCnt = reader.ReadInt16();
                _objectList.Capacity = objectCnt;
                for (var i = 0; i < objectCnt; i++)
                {
                    var obj = reader.ReadData<NavMeshInst>();
                    _objectList.Add(obj);
                }

                Debug.WriteLine($"Objects: {objectCnt}");

                //Cells
                var cellCnt = reader.ReadInt32();
                reader.ReadInt32();
                _cellList.Capacity = cellCnt;
                for (var i = 0; i < cellCnt; i++)
                {
                    var cell = reader.ReadData<NavCellQuad>();
                    _cellList.Add(cell);
                }

                Debug.WriteLine($"Cells: {cellCnt}");

                //EdgeGlobal
                var globalEdgeCnt = reader.ReadInt32();
                _globalEdgeList.Capacity = globalEdgeCnt;
                for (var i = 0; i < globalEdgeCnt; i++)
                {
                    var edge = reader.ReadData<NavEdgeGlobal>();
                    _globalEdgeList.Add(edge);
                }

                Debug.WriteLine($"Global edges: {globalEdgeCnt}");

                //EdgeInternal
                var internalEdgeCnt = reader.ReadInt32();
                _internalEdgeList.Capacity = internalEdgeCnt;
                for (var i = 0; i < internalEdgeCnt; i++)
                {
                    var edge = reader.ReadData<NavEdgeInternal>();
                    _internalEdgeList.Add(edge);
                }

                Debug.WriteLine($"Internal edges: {internalEdgeCnt}");

                ////TileMap
                //stream.Seek(8 * CellsTotal, SeekOrigin.Current);

                ////HeightMap
                //for (int i = 0; i < VERTICIES_TOTAL; i++)
                //    _heightMap[i] = reader.ReadSingle();

                ////WaterMap
                //for (int i = 0; i < BLOCKS_TOTAL; i++)
                //    _waterMap[i].Type = (NavWaterType)reader.ReadByte();

                ////WaterHeightMap
                //for (int i = 0; i < BLOCKS_TOTAL; i++)
                //    _waterMap[i].Height = reader.ReadSingle();
            }
        }

        /// <summary>
        /// Gets the cells.
        /// </summary>
        /// <returns></returns>
        public List<NavCellQuad> GetCells()
        {
            return _cellList;
        }

        /// <summary>
        /// Gets the global edges.
        /// </summary>
        /// <returns></returns>
        public List<NavEdgeGlobal> GetGlobalEdges()
        {
            return _globalEdgeList;
        }

        /// <summary>
        /// Gets the internal edges.
        /// </summary>
        /// <returns></returns>
        public List<NavEdgeInternal> GetInternalEdges()
        {
            return _internalEdgeList;
        }

        /// <summary>
        /// Gets the objects.
        /// </summary>
        /// <returns></returns>
        public List<NavMeshInst> GetObjects()
        {
            return _objectList;
        }
        #endregion
    }
}