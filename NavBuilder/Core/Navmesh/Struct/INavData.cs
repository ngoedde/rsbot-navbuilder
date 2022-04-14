using NavBuilder.Core.Navmesh.IO;

namespace NavBuilder.Core.Navmesh.Struct
{
    internal interface INavData
    {
        void Read(NavMeshReader reader);
    }

    internal interface INavData<in TArg>
    {
        void Read(NavMeshReader reader, TArg arg0);
    }
}