namespace NavBuilder.Core.Navmesh.Struct
{
    internal enum NavEdgeDirection : byte
    {
        /// <summary>
        /// North
        /// </summary>
        North = 0,

        /// <summary>
        /// East
        /// </summary>
        East = 1,

        /// <summary>
        /// South
        /// </summary>
        South = 2,

        /// <summary>
        /// West
        /// </summary>
        West = 3,

        /// <summary>
        /// None
        /// </summary>
        X = byte.MaxValue
    }
}