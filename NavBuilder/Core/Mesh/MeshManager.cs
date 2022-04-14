using System.Collections.Generic;

namespace NavBuilder.Core.Mesh
{
    internal static class MeshManager
    {
        #region Fields

        private static Dictionary<string, MeshObject> _meshes;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Loads the mesh object.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static MeshObject LoadMeshObject(string fileName)
        {
            if (fileName == null)
                return null;

            if (_meshes == null)
                _meshes = new Dictionary<string, MeshObject>();

            var stopWatch = System.Diagnostics.Stopwatch.StartNew();

            if (_meshes.ContainsKey(fileName))
            {
                var mesh = _meshes[fileName];

                FileEvents.Raise(FileEvents.ResourceGroup.Object, fileName, mesh.Pk2File.Size, stopWatch.ElapsedMilliseconds, mesh);

                return _meshes[fileName];
            }
            var meshObject = new MeshObject();
            meshObject.Load(fileName);

            if (!_meshes.ContainsKey(fileName))
                _meshes.Add(fileName, meshObject);

            FileEvents.Raise(FileEvents.ResourceGroup.Object, fileName, meshObject.Pk2File.Size, stopWatch.ElapsedMilliseconds, meshObject);

            return meshObject;
        }

        #endregion Methods
    }
}