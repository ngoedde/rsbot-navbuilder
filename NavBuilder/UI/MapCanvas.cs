using NavBuilder.Client;
using NavBuilder.Core;
using NavBuilder.Core.Collision;
using NavBuilder.Core.Export;
using NavBuilder.Core.GraphicsUtilities;
using NavBuilder.Core.Navmesh.Struct;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;
using Region = NavBuilder.Core.Map.Region;

namespace NavBuilder.UI
{
    public partial class MapCanvas : UserControl
    {
        #region Delegates

        public delegate void LoadRegionEventHandler(Region region);

        public event LoadRegionEventHandler RegionLoaded;

        public delegate void BeforeLoadRegionEventHandler(Region region);

        public event BeforeLoadRegionEventHandler BeforeLoadRegion;

        #endregion Delegates

        #region Properties

        /// <summary>
        /// Gets the center region.
        /// </summary>
        /// <value>
        /// The center region.
        /// </value>
        public Region CenterRegion => Regions != null && Regions.Length > 4 ? Regions[4] : new Region(0);

        /// <summary>
        /// Gets the regions.
        /// </summary>
        /// <value>
        /// The regions.
        /// </value>
        public Region[] Regions { get; private set; }

        /// <summary>
        /// Gets the terrains.
        /// </summary>
        /// <value>
        /// The terrains.
        /// </value>
        internal NavMeshTerrain[] Terrains { get; private set; }

        /// <summary>
        /// Gets the file name of the collision file
        /// </summary>
        public string ExternalCollisionFileName { get; private set; }

        #endregion Properties

        #region Fields

        private object _gLock;
        private CollisionLoader _collisionLoader;

        private SharpDX.Point _pointSource;
        private SharpDX.Point _pointDestination;

        private List<Line> _loadedCollisions;

        private SharpDX.Point _clickPosA;
        private SharpDX.Point _clickPosB;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MapCanvas"/> class.
        /// </summary>
        public MapCanvas()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Loads the region.
        /// </summary>
        /// <param name="region">The region.</param>
        public void LoadRegion(Region region)
        {
            if (Pk2Controller.Media == null || !Pk2Controller.Media.Loaded) return;

            BeforeLoadRegion?.Invoke(region);

            Regions = region.GetSurroundingRegions();
            Terrains = new NavMeshTerrain[9];

            _gLock = new object();

            ClearImage();
            LoadMiniMap();

            if (ExternalCollisionFileName != null)
                LoadCollisionsFromFile(ExternalCollisionFileName);
            else
                LoadTerrainCollision();

            RegionLoaded?.Invoke(region);
        }

        /// <summary>
        /// Loads the map.
        /// </summary>
        private void LoadMiniMap()
        {
            if (Pk2Controller.Media == null || !Pk2Controller.Media.Loaded) return;

            var sectorImages = new Image[9];
            for (var i = 0; i < 9; i++)
            {
                try
                {
                    var file = Pk2Controller.Media.GetFile(Regions[i].ImageFileName);
                    sectorImages[i] = file.ToImage();
                }
                catch
                {
                    sectorImages[i] = new Bitmap(256, 256);
                }
            }

            var graphics = GetGraphics();

            lock (_gLock)
            {
                //Top row
                graphics.DrawImageUnscaledAndClipped(sectorImages[0], new Rectangle(0, 0, 256, 256));
                graphics.DrawImageUnscaledAndClipped(sectorImages[1], new Rectangle(256, 0, 256, 256));
                graphics.DrawImageUnscaledAndClipped(sectorImages[2], new Rectangle(512, 0, 256, 256));

                ////Middle row
                graphics.DrawImageUnscaledAndClipped(sectorImages[3], new Rectangle(0, 256, 256, 256));
                graphics.DrawImageUnscaledAndClipped(sectorImages[4], new Rectangle(256, 256, 256, 256));
                graphics.DrawImageUnscaledAndClipped(sectorImages[5], new Rectangle(512, 256, 256, 256));

                ////Bottom row
                graphics.DrawImageUnscaledAndClipped(sectorImages[6], new Rectangle(0, 512, 256, 256));
                graphics.DrawImageUnscaledAndClipped(sectorImages[7], new Rectangle(256, 512, 256, 256));
                graphics.DrawImageUnscaledAndClipped(sectorImages[8], new Rectangle(512, 512, 256, 256));

                graphics.DrawLine(Pens.Black, new Point(256, 0), new Point(256, 768));
                graphics.DrawLine(Pens.Black, new Point(512, 0), new Point(512, 768));
                graphics.DrawLine(Pens.Black, new Point(0, 256), new Point(768, 256));
                graphics.DrawLine(Pens.Black, new Point(0, 512), new Point(768, 512));
            }

            picRenderer.Refresh();
        }

        /// <summary>
        /// Loads the terrain collision.
        /// </summary>
        private void LoadTerrainCollision()
        {
            var graphics = GetGraphics();

            for (var i = 0; i < Regions.Length; i++)
            {
                try
                {
                    var drawOffset = GetDrawOffset(i);
                    var region = Regions[i];
                    var terrain = region.Terrain;

                    if (terrain == null)
                        continue;

                    //Terrain's own collision
                    var edges = terrain.GetInternalEdges();
                    if (edges != null)
                        foreach (var edge in edges)
                        {
                            if (!edge.HasNeighbourCell) continue;

                            var line = GetPrefabLine(edge.Min, edge.Max, drawOffset);

                            lock (_gLock)
                                graphics.DrawLine(Pens.Red, line.Source.ToDrawingPoint(), line.Destination.ToDrawingPoint());
                        }

                    var objects = terrain.GetObjects();
                    //Objects on the terrain
                    if (objects != null)
                        foreach (var meshInst in objects)
                        {
                            if (meshInst.Resource?.Mesh?.Outlines == null)
                                continue;

                            foreach (var outline in meshInst.Resource.Mesh.Outlines)
                            {
                                var vectorA = meshInst.Resource.Mesh.Points[outline.PointIndexA]
                                    .GetPrefabPosition(meshInst);

                                var vectorB = meshInst.Resource.Mesh.Points[outline.PointIndexB]
                                    .GetPrefabPosition(meshInst);

                                var prefabLine = GetPrefabLine(vectorA, vectorB, drawOffset);

                                if (outline.Flag != NavEdgeFlag.Bridge)
                                {
                                    lock (_gLock)
                                        graphics.DrawLine(Pens.Red, prefabLine.Source.ToDrawingPoint(), prefabLine.Destination.ToDrawingPoint());
                                }
                                else
                                {
                                    lock (_gLock)
                                        graphics.DrawLine(Pens.LimeGreen, prefabLine.Source.ToDrawingPoint(), prefabLine.Destination.ToDrawingPoint());
                                }
                            }
                        }

                    picRenderer.Refresh();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error during rendering: ", ex.Message);
                }

            }

        }

        /// <summary>
        /// Loads the collisions from file.
        /// </summary>
        public void LoadCollisionsFromFile(string fileName)
        {
            ClearImage();
            LoadMiniMap();

            if (fileName == null)
            {
                LoadTerrainCollision();
                ExternalCollisionFileName = null;
            }

            if (_collisionLoader == null)
                _collisionLoader = new CollisionLoader(fileName, fileName + "i");

            ExternalCollisionFileName = fileName;

            var graphics = GetGraphics();
            _loadedCollisions = new List<Line>();

            var scale = 256 / 1920f;

            //Translates lines to absolute grid positions
            for (var i = 0; i < 9; i++)
            {
                var region = Regions[i];

                var drawOffset = GetDrawOffset(i);

                foreach (var line in _collisionLoader.GetCollisions(region.Id))
                {
                    var translatedLine = new Line
                    {
                        Source = new SharpDX.Point
                        (
                            Convert.ToInt32((drawOffset.X + line.Source.X) * scale),
                            Convert.ToInt32((drawOffset.Y + line.Source.Y) * scale)
                       ),
                        Destination = new SharpDX.Point
                        (
                            Convert.ToInt32((drawOffset.X + line.Destination.X) * scale),
                            Convert.ToInt32((drawOffset.Y + line.Destination.Y) * scale)
                         )
                    };

                    _loadedCollisions.Add(translatedLine);
                }
            }

            foreach (var line in _loadedCollisions)
            {
                var vectorA = new Vector2(line.Source.X, line.Source.Y);
                var vectorB = new Vector2(line.Destination.X, line.Destination.Y);

                lock (_gLock)
                {
                    graphics.DrawLine(Pens.Red, vectorA.X, vectorA.Y, vectorB.X, vectorB.Y);
                }
            }

            picRenderer.Invalidate();
        }

        /// <summary>
        /// Clears the image.
        /// </summary>
        private void ClearImage()
        {
            picRenderer.Image = new Bitmap(768, 768);
        }

        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <returns></returns>
        private Graphics GetGraphics()
        {
            return Graphics.FromImage(picRenderer.Image);
        }

        /// <summary>
        /// Gets the draw offset.
        /// </summary>
        /// <param name="index">The index.</param>
        private Point GetDrawOffset(int index)
        {
            switch (index)
            {
                case 0:
                    return new Point(0, 0);

                case 1:
                    return new Point(1920, 0);

                case 2:
                    return new Point(1920 * 2, 0);

                case 3:
                    return new Point(0, 1920);

                case 4:
                    return new Point(1920, 1920);

                case 5:
                    return new Point(1920 * 2, 1920);

                case 6:
                    return new Point(0, 1920 * 2);

                case 7:
                    return new Point(1920, 1920 * 2);

                case 8:
                    return new Point(1920 * 2, 1920 * 2);

                default:
                    return new Point(0, 0);
            }
        }

        /// <summary>
        /// Gets the rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <param name="drawOffset">The draw offset.</param>
        /// <returns></returns>
        private Rectangle GetPrefabRectangle(SharpDX.RectangleF rectangle, Point drawOffset)
        {
            var scale = (float)256 / (float)1920m;

            var location = new Point(
                    Convert.ToInt32((drawOffset.X + rectangle.X) * scale),
                    Convert.ToInt32((drawOffset.Y + (1920 - rectangle.Y)) * scale)
                );

            var size = new Size(
                    Convert.ToInt32(rectangle.Width * scale),
                    Convert.ToInt32(rectangle.Height * scale)
                );

            return new Rectangle(location, size);
        }

        /// <summary>
        /// Gets the prefab line.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="drawOffset">The draw offset.</param>
        /// <returns></returns>
        private Line GetPrefabLine(Vector2 min, Vector2 max, Point drawOffset)
        {
            var scale = (float)256 / (float)1920m;

            var line = new Line
            {
                Source = new SharpDX.Point(
                    Convert.ToInt32((drawOffset.X + min.X) * scale),
                    Convert.ToInt32((drawOffset.Y + (1920 - min.Y)) * scale)
                ),
                Destination = new SharpDX.Point(
                    Convert.ToInt32((drawOffset.X + max.X) * scale),
                    Convert.ToInt32((drawOffset.Y + (1920 - max.Y)) * scale)
                )
            };

            return line;
        }

        private PointF GetPrefabPointF(PointF point, Point drawOffset)
        {
            var scale = (float)256 / (float)1920m;
            return new PointF(
                Convert.ToInt32((drawOffset.X + point.X) * scale),
                Convert.ToInt32((drawOffset.Y + (1920 - point.Y)) * scale)
            );
        }

        /// <summary>
        /// Gets the prefab point f.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="drawOffset">The draw offset.</param>
        /// <returns></returns>
        private Point GetPrefabPointF(Vector3 position, Point drawOffset)
        {
            var scale = (float)256 / (float)1920m;

            return new Point(
                Convert.ToInt32((drawOffset.X + position.X) * scale),
                Convert.ToInt32((drawOffset.Y + (1920 - position.Z)) * scale)
            );
        }

        /// <summary>
        /// Gets the region index at.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        private int GetRegionIndexAt(int x, int y)
        {
            if (x < 256)
            {
                if (y < 256)
                    return 0;

                if (y > 256 && y < 512)
                    return 3;

                if (y > 512)
                    return 6;
            }

            if (x > 256 && x < 512)
            {
                if (y < 256)
                    return 1;

                if (y > 256 && y < 512)
                    return 4;

                if (y > 512)
                    return 7;
            }

            if (x > 512)
            {
                if (y < 256)
                    return 2;

                if (y > 256 && y < 512)
                    return 5;

                if (y > 512)
                    return 8;
            }

            return 0;
        }

        #endregion Methods

        private void picRenderer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || Globals.CurrentProject == null || ExternalCollisionFileName == null)
                return;

            if (_clickPosA.X == 0 && _clickPosA.Y == 0)
            {
                //Player
                var graphics = GetGraphics();

                lock (_gLock)
                    graphics.FillRectangle(Brushes.LimeGreen, e.X, e.Y, 5, 5);

                _clickPosA = new SharpDX.Point(e.X, e.Y);
            }
            else if (_clickPosA.X != 0 && _clickPosA.Y != 0 && _clickPosB.X == 0 && _clickPosB.Y == 0)
            {
                //Monster
                var graphics = GetGraphics();

                lock (_gLock)
                    graphics.FillRectangle(Brushes.Blue, e.X, e.Y, 5, 5);

                _clickPosB = new SharpDX.Point(e.X, e.Y);

                //Check if standing inside collision
                var tempCollisionPoint = CollisionDetector.HasCollisionBetween(_clickPosA,
                    CalculatePoint(_clickPosA.ToDrawingPoint(), _clickPosB.ToDrawingPoint(), 10).ToDXPoint(), _loadedCollisions);

                if (tempCollisionPoint.X != 0 || tempCollisionPoint.Y != 0)
                {
                    _clickPosA = CalculatePoint(_clickPosA.ToDrawingPoint(), _clickPosB.ToDrawingPoint(), 10).ToDXPoint();
                    graphics.DrawRectangle(Pens.Yellow, _clickPosA.X, _clickPosA.Y, 2, 2);
                }

                var collisionPoint = CollisionDetector.HasCollisionBetween(_clickPosA, _clickPosB, _loadedCollisions);

                if (collisionPoint.X != 0 && collisionPoint.Y != null)
                {
                    lock (_gLock)
                    {
                        graphics.FillRectangle(Brushes.Indigo, collisionPoint.X, collisionPoint.Y, 5, 5);
                        graphics.DrawLine(Pens.Red, _clickPosA.ToDrawingPoint(), collisionPoint.ToDrawingPoint());
                    }
                }
                else
                {
                    lock (_gLock)
                    {
                        graphics.DrawLine(Pens.Blue, _clickPosA.ToDrawingPoint(), _clickPosB.ToDrawingPoint());
                    }
                }
            }
            else
            {
                //Reset
                _clickPosA = new SharpDX.Point();
                _clickPosB = new SharpDX.Point();

                LoadRegion(CenterRegion);
            }

            picRenderer.Invalidate();
        }

        private static Point CalculatePoint(Point a, Point b, int distance)
        {
            // a. calculate the vector from o to g:
            double vectorX = b.X - a.X;
            double vectorY = b.Y - a.Y;

            // b. calculate the proportion of hypotenuse
            double factor = distance / Math.Sqrt(vectorX * vectorX + vectorY * vectorY);

            // c. factor the lengths
            vectorX *= factor;
            vectorY *= factor;

            // d. calculate and Draw the new vector,
            return new Point((int)(a.X + vectorX), (int)(a.Y + vectorY));
        }
    }
}