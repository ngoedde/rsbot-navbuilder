using NavBuilder.Client;
using NavBuilder.Core.Navmesh.Struct;
using System.Collections.Generic;
using System.Diagnostics;

namespace NavBuilder.Core.Navmesh
{
    internal static class NavMeshManager
    {
        private static Dictionary<string, NavMeshTerrain> _navmeshes;

        public static NavMeshTerrain LoadNavmesh(string fileName)
        {
            if (_navmeshes == null)
                _navmeshes = new Dictionary<string, NavMeshTerrain>();

            if (!Pk2Controller.Data.FileExists(fileName))
            {
                Debug.WriteLine($"Can not find file: {fileName}");
                return null;
            }

            var stopWatch = Stopwatch.StartNew();
            if (_navmeshes.ContainsKey(fileName))
            {
                var _navmesh = _navmeshes[fileName];

                FileEvents.Raise(FileEvents.ResourceGroup.Navmesh, fileName, _navmesh.Pk2File.Size, stopWatch.ElapsedMilliseconds, _navmesh);

                return _navmesh;
            }

            Debug.WriteLine($"Loading navmesh file {fileName}");

            stopWatch.Start();

            var navmesh = new NavMeshTerrain();
            navmesh.Load(fileName);

            _navmeshes.Add(fileName, navmesh);
            stopWatch.Stop();

            Debug.WriteLine($"Loaded navmesh in {stopWatch.ElapsedMilliseconds}ms!");

            FileEvents.Raise(FileEvents.ResourceGroup.Navmesh, fileName, navmesh.Pk2File.Size, stopWatch.ElapsedMilliseconds, navmesh);
            return navmesh;
        }
    }
}