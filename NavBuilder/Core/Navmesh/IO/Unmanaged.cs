using System;
using System.Runtime.InteropServices;

namespace NavBuilder.Core.Navmesh.IO
{
    public static class Unmanaged
    {
        #region Methods

        /// <summary>
        /// Buffers to structure.
        /// </summary>
        /// <typeparam name="TStruct">The type of the structure.</typeparam>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <returns></returns>
        public static unsafe TStruct BufferToStruct<TStruct>(byte[] buffer, int offset = 0) where TStruct : struct
        {
            fixed (byte* ptr = &buffer[offset])
                return (TStruct)Marshal.PtrToStructure((IntPtr)ptr, typeof(TStruct));
        }

        #endregion Methods
    }
}