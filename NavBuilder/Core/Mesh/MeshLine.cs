using NavBuilder.Core.Navmesh.Struct;

namespace NavBuilder.Core.Mesh
{
    internal class MeshLine
    {
        #region Properties

        /// <summary>
        /// The index of the point A.
        ///
        /// <see cref="MeshObject.Points"/>
        /// </summary>
        public ushort PointIndexA;

        /// <summary>
        /// The index of the point B.
        ///
        /// <see cref="MeshObject.Points"/>
        /// </summary>
        public ushort PointIndexB;

        /// <summary>
        /// The flag of this mesh line.
        /// </summary>
        public NavEdgeFlag Flag;
        
        #endregion Properties
    }
}