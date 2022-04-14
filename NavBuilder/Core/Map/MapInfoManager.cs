using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NavBuilder.Client;

namespace NavBuilder.Core.Map
{
    internal static class MapInfoManager
    {
        #region Constants

        private const string Filename = "mapinfo.mfo";

        private const int RegionsX = 256;
        private const int RegionsY = 256;
        public const int RegionsTotal = RegionsX * RegionsY;

        private const int NameBufferSize = 12;
        private const int RegionsBufferSize = RegionsTotal / 8;

        #endregion Constants

        #region Fields

        private static BitArray _regions;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Loads the map information.
        /// </summary>
        public static void LoadMapInfo()
        {
            Debug.WriteLine("==== Mapinfo.mfo ====");

            if (!Pk2Controller.Data.FileExists(Filename))
            {
                MessageBox.Show("Could not load mapinfo.mfo The file does not exist!", "mapinfo.mfo missing",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            using (var reader = new BinaryReader(Pk2Controller.Data.GetFile(Filename).GetStream()))
            {
                var header = Encoding.ASCII.GetString(reader.ReadBytes(NameBufferSize));
                if (header != "JMXVMFO 1000")
                {
                    MessageBox.Show("Could not load map info! The file has an unknown format!", "mapinfo.mfo unknown format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Name
                reader.ReadBytes(12);

                var buffer = reader.ReadBytes(RegionsBufferSize);
                for (var i = 0; i < RegionsBufferSize; i++)
                    buffer[i] = buffer[i].ReverseWithLookupTable();

                _regions = new BitArray(buffer);
            }

            stopWatch.Stop();
            Debug.WriteLine($"Mapinfo.mfo loaded in {stopWatch.ElapsedMilliseconds}ms");
            Debug.WriteLine("==== /Mapinfo.mfo/ ====");
        }

        /// <summary>
        /// Determines whether the specified region is active or not.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>
        ///   <c>true</c> if [is region active] [the specified x]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRegionActive(byte x, byte y)
        {
            return _regions[y * 256 + x];
        }

        /// <summary>
        /// Determines whether the specified region is active or not.
        /// </summary>
        /// <param name="regionId">The region identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is region active] [the specified region identifier]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRegionActive(int regionId)
        {
            return _regions[regionId];
        }
        #endregion Methods
    }
}