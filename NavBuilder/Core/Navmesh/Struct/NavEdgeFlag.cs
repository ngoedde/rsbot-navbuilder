using System;

namespace NavBuilder.Core.Navmesh.Struct
{
    [Flags]
    internal enum NavEdgeFlag : byte
    {
        /// <summary>
        /// Transition between Terrain and Object space
        /// </summary>
        SwapSpace = 0,

        /// <summary>
        /// Block Vertex2Vertex?
        /// </summary>
        Bit0 = 1,

        /// <summary>
        /// Block Cell2Cell
        /// </summary>
        Bit1 = 2,

        BlockedOutline = Bit0 | Bit1,

        /// <summary>
        /// IsInline?
        /// </summary>
        Inline = 4,

        BlockedInline = Bit0 | Bit1 | Inline,

        /// <summary>
        /// Transition between
        /// </summary>
        SwapMesh = 8,

        /// <summary>
        /// Passable when walking underneath?
        /// </summary>
        Bridge = 16,

        Bit5 = 32,
        Bit6 = 64,

        Siege = 128, //FORTRESS RELATED
    }
}