using System.IO;
using System.Windows.Forms;

namespace NavBuilder.Core
{
    internal class Project
    {
        #region Constants

        /// <summary>
        /// The configuration file name
        /// </summary>
        public const string ConfigFileName = "navbuilder.cfg";

        #endregion Constants

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name => Config?.Get<string>("NavBuilder.Name");

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public Config Config { get; set; }

        /// <summary>
        /// Gets or sets the output file.
        /// </summary>
        /// <value>
        /// The output file.
        /// </value>
        public string OutputFile => System.IO.Path.Combine(Path, Name, ".wg");

        /// <summary>
        /// Gets the silkroad path.
        /// </summary>
        /// <value>
        /// The silkroad path.
        /// </value>
        public string SilkroadPath => Config?.Get<string>("NavBuilder.SilkroadPath");

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static Project Load(string path)
        {
            if (!Directory.Exists(path))
            {
                MessageBox.Show($@"Could not find any project at {path}", @"Invalid project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            var configPath = System.IO.Path.Combine(path, ConfigFileName);

            if (!File.Exists(configPath))
            {
                MessageBox.Show($@"Could not find any project settings at {path}", @"Invalid project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            var result = new Project
            {
                Path = path,
                Config = new Config(configPath)
            };
            
            return result;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            if (Config == null)
                return;

            Config.Save();
        }

        #endregion Methods
    }
}