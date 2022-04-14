using System.Collections.Generic;
using System.IO;

namespace NavBuilder.Core.Mesh
{
    internal class ResourceManager
    {
        #region Fields

        /// <summary>
        /// Gets or sets the resources.
        /// </summary>
        /// <value>
        /// The resources.
        /// </value>
        private static Dictionary<int, Resource> _resources;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Loads the resource.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public static Resource LoadResource(int id)
        {
            if (_resources == null)
                _resources = new Dictionary<int, Resource>(ObjectIndexManager.Index.Count);

            var stopWatch = System.Diagnostics.Stopwatch.StartNew();

            if (_resources.ContainsKey(id))
            {
                var _resource = _resources[id];
                FileEvents.Raise(FileEvents.ResourceGroup.Resource, _resource.Pk2File.Name, _resource.Pk2File.Size, stopWatch.ElapsedMilliseconds, _resource);

                return _resource;
            }

            var resource = new Resource();

            var resourceFileName = Path.GetFileName(ObjectIndexManager.Index[id]);
            resource.Load(resourceFileName);
            _resources.Add(id, resource);

            FileEvents.Raise(FileEvents.ResourceGroup.Resource, resource.Pk2File.Name, resource.Pk2File.Size, stopWatch.ElapsedMilliseconds, resource);
            return resource;
        }

        #endregion Methods
    }
}