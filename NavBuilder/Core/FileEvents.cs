namespace NavBuilder.Core
{


    internal class FileEvents
    {   
        internal delegate void FileLoadedEventHandler(ResourceGroup group, string path, long size, long loadTimeMs, object propertyInfo);
        public static event FileLoadedEventHandler OnFileLoaded;

        public static void Raise(ResourceGroup group, string path, long size, long loadTimeMs, object propertyInfo)
        {
            OnFileLoaded?.Invoke(group, path, size, loadTimeMs, propertyInfo);
        }

        internal enum ResourceGroup : int
        {
            Navmesh = 0,
            Minimap = 2,
            Object = 1,
            Resource = 3
        }
    }
}
