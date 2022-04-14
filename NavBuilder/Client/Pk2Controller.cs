using RoadShark.Pk2;
using RoadShark.Pk2.Security;
using System.IO;
using System.Windows.Forms;

namespace NavBuilder.Client
{
    internal static class Pk2Controller
    {
        #region Properties

        /// <summary>
        /// Gets or sets the media.
        /// </summary>
        /// <value>
        /// The media.
        /// </value>
        public static PK2Archive Media { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public static PK2Archive Data { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public static PK2Archive Map { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads the media archive.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void LoadMediaArchive(string path)
        {
            try
            {
                Media = new PK2Archive(path);
            }
            catch (BlowfishSecurityException blowfishSecurityException)
            {
                MessageBox.Show(
                    $@"Could not load media archive. Got an security exception: {blowfishSecurityException.Message}",
                    @"Media load error", MessageBoxButtons.OK);
            }
            catch (IOException ioException)
            {
                MessageBox.Show(
                    $@"Could not load media archive. Got an IO exception: {ioException.Message}",
                    @"Media load error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Loads the data archive.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void LoadDataArchive(string path)
        {
            try
            {
                Data = new PK2Archive(path);
            }
            catch (BlowfishSecurityException blowfishSecurityException)
            {
                MessageBox.Show(
                    $@"Could not load data archive. Got an security exception: {blowfishSecurityException.Message}",
                    @"Data load error", MessageBoxButtons.OK);
            }
            catch (IOException ioException)
            {
                MessageBox.Show(
                    $@"Could not load data archive. Got an IO exception: {ioException.Message}",
                    @"Data load error", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Loads the map archive.
        /// </summary>
        /// <param name="path">The path.</param>
        public static void LoadMapArchive(string path)
        {
            try
            {
                Map = new PK2Archive(path);
            }
            catch (BlowfishSecurityException blowfishSecurityException)
            {
                MessageBox.Show(
                    $@"Could not load map archive. Got an security exception: {blowfishSecurityException.Message}",
                    @"Data load error", MessageBoxButtons.OK);
            }
            catch (IOException ioException)
            {
                MessageBox.Show(
                    $@"Could not load map archive. Got an IO exception: {ioException.Message}",
                    @"Map load error", MessageBoxButtons.OK);
            }
        }

        #endregion Methods
    }
}