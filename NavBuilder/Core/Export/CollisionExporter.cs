using NavBuilder.Core.GraphicsUtilities;
using NavBuilder.Core.Navmesh.Struct;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using SharpDX;
using System.Globalization;
using System.IO;
using NavBuilder.Core.Navmesh;

namespace NavBuilder.Core.Export
{
    internal class CollisionExporter
    {
        #region Delegates

        public delegate void ReportProgressEventHandler(int filesDone, int filesMissing);

        public event ReportProgressEventHandler ReportProgress;

        public delegate void BeginFileExportEventHandler(string fileName);

        public event BeginFileExportEventHandler BeginFileExport;

        public delegate void EndFileExportEventHandler(bool success, string file, string message);

        public event EndFileExportEventHandler EndFileExport;

        #endregion Delegates

        #region Properties

        /// <summary>
        /// Gets the directory path.
        /// </summary>
        /// <value>
        /// The directory path.
        /// </value>
        public string FilePath { get; }

        /// <summary>
        /// Gets the index file path.
        /// </summary>
        /// <value>
        /// The index file path.
        /// </value>
        public string IndexFilePath { get; }

        #endregion Properties

        #region Fields

        private BinaryWriter _writer;
        private BinaryWriter _indexWriter;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionExporter" /> class.
        /// </summary>
        /// <param name="filePath">The filePath.</param>
        /// <param name="indexFilePath">The index file path.</param>
        public CollisionExporter(string filePath, string indexFilePath)
        {
            FilePath = filePath;
            IndexFilePath = indexFilePath;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Aborts this instance.
        /// </summary>
        public void Abort()
        {
            _writer.Dispose();
        }

        /// <summary>
        /// Exports the files.
        /// </summary>
        /// <param name="files">The files.</param>
        public void ExportFiles(string[] files)
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);

            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));

            _writer = new BinaryWriter(File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.Write));
            _indexWriter = new BinaryWriter(File.Open(IndexFilePath, FileMode.OpenOrCreate, FileAccess.Write));

            var currentFile = 0;
            foreach (var file in files)
            {
                currentFile++;
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                try
                {
                    BeginFileExport?.Invoke(file);

                    Export(file);
                    
                    stopWatch.Stop();
                    ReportProgress?.Invoke(currentFile, files.Length);
                    EndFileExport?.Invoke(true, file, stopWatch.ElapsedMilliseconds + "ms");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("[ERROR] ===> " + ex.Message);
                    EndFileExport?.Invoke(false, file, ex.Message);
                }
            }

            _writer.Close();
            _indexWriter.Close();
        }

        /// <summary>
        /// Exports the specified navmesh file.
        /// </summary>
        /// <param name="navmeshFile">The navmesh file.</param>
        private void Export(string navmeshFile)
        {
            var regionId = int.Parse(Path.GetFileNameWithoutExtension(navmeshFile).Split('_')[1], NumberStyles.HexNumber);

            AppendToIndex(regionId, _writer.BaseStream.Position);

            var collisionLines = new List<Line>();

            Debug.WriteLine($"Exporting collision file {navmeshFile}");

            _writer.Write(regionId);

            //Fetch terrain collision
            var navmeshTerrain = NavMeshManager.LoadNavmesh(navmeshFile);
            foreach (var edge in navmeshTerrain.GetInternalEdges())
                if (edge.HasNeighbourCell)
                    collisionLines.Add(new Line
                    {
                        Source = new Point(
                            Convert.ToInt32(edge.Min.X),
                            Convert.ToInt32(1920 - edge.Min.Y)
                        ),
                        Destination = new Point(
                            Convert.ToInt32(edge.Max.X),
                            Convert.ToInt32(1920 - edge.Max.Y)
                        ),
                    });
            
            //Write terrain collision
            _writer.Write(collisionLines.Count);
            foreach (var line in collisionLines)
            {
                _writer.Write(line.Source.X);
                _writer.Write(line.Source.Y);

                _writer.Write(line.Destination.X);
                _writer.Write(line.Destination.Y);
            }

            //Fetch object collision on the navmesh
            collisionLines = new List<Line>();
            foreach (var navInst in navmeshTerrain.GetObjects())
            {
                if (navInst.Resource?.Mesh?.Outlines == null)
                    continue;
                
                foreach (var outline in navInst.Resource.Mesh.Outlines)
                {
                    if (outline.Flag == NavEdgeFlag.Bridge)
                        continue;

                    var vectorA = navInst.Resource.Mesh.Points[outline.PointIndexA]
                        .GetPrefabPosition(navInst);

                    var vectorB = navInst.Resource.Mesh.Points[outline.PointIndexB]
                        .GetPrefabPosition(navInst);

                    var line = new Line
                    {
                        Source = new Point(Convert.ToInt32(vectorA.X), Convert.ToInt32(1920 - vectorA.Y)),
                        Destination = new Point(Convert.ToInt32(vectorB.X), Convert.ToInt32(1920 - vectorB.Y))
                    };

                    collisionLines.Add(line);
                }
            }

            //Write object collisions
            _writer.Write(collisionLines.Count);
            foreach (var line in collisionLines)
            {
                _writer.Write(line.Source.X);
                _writer.Write(line.Source.Y);

                _writer.Write(line.Destination.X);
                _writer.Write(line.Destination.Y);
            }

            _writer.Flush();
        }

        /// <summary>
        /// Appends to index.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="pointer">The pointer.</param>
        private void AppendToIndex(int regionId, long pointer)
        {
            _indexWriter.Write(regionId);
            _indexWriter.Write(pointer);
        }
        #endregion Methods
    }
}