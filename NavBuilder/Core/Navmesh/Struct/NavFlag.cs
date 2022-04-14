using System;

namespace NavBuilder.Core.Navmesh.Struct
{
    [Flags]
    internal enum NavFlag : int
    {
        None = 0,
        Edge = 1,
        Cell = 2,
        Event = 4,
    }
}