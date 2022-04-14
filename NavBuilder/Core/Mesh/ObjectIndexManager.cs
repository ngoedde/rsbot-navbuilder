using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using NavBuilder.Client;

namespace NavBuilder.Core.Mesh
{
    internal static class ObjectIndexManager
    {
        #region Constants

        private const string FileName = "object.ifo";

        #endregion Constants

        #region Properties

        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public static Dictionary<int, string> Index { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads the index of the object.
        /// </summary>
        public static void LoadObjectIndex()
        {
            Debug.WriteLine("======== object.ifo =========");
            Debug.WriteLine("Loading file object.ifo...");

            if (!Pk2Controller.Data.FileExists(FileName))
            {
                MessageBox.Show("Could not load object index! The file object.ifo does not exist in the data.pk2",
                    "object.ifo not found", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            using (var reader = new StreamReader(Pk2Controller.Data.GetFile(FileName).GetStream()))
            {
                if (reader.ReadLine() != "JMXVOBJI1000")
                {
                    MessageBox.Show(
                        "Could not load object index! The file has an unknown format!",
                        "object.ifo corrupted?", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                var count = Convert.ToInt32(reader.ReadLine());
                Index = new Dictionary<int, string>(count);

                var fileContent = reader.ReadToEnd().Split('\n');
                for (var i = 0; i < count; i++)
                {
                    var line = fileContent[i];

                    Index.Add(
                        Convert.ToInt32(line.Substring(0, 5)),
                        line.Substring(18).Trim('"')
                    );
                }
            }

            stopWatch.Stop();

            Debug.WriteLine($"Found {Index.Count} resources in the object.ifo");
            Debug.WriteLine($"Loaded file object.ifo in {stopWatch.ElapsedMilliseconds}ms");
            Debug.WriteLine("======== /object.ifo/ =========");
        }

        #endregion Methods
    }
}