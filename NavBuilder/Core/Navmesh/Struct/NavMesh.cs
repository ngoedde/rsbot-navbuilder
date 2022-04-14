using System.IO;

namespace NavBuilder.Core.Navmesh.Struct
{
    internal abstract class NavMesh
    {
        #region Methods

        /// <summary>
        /// Loads the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public abstract void Load(string fileName);

        #endregion Methods
    }
}