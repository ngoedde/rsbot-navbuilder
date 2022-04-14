using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NavBuilder.Core.Navmesh.IO;

namespace NavBuilder.Core
{
    internal static class BinaryReaderExtensions
    {
        public static string ReadJoymaxString(this BinaryReader reader)
        {
            return Encoding.GetEncoding(949).GetString(reader.ReadBytes(reader.ReadInt32()));
        }
        
        /// <summary>
        /// Reads the structure.
        /// </summary>
        /// <typeparam name="TStruct">The type of the structure.</typeparam>
        /// <returns></returns>
        public static TStruct ReadStruct<TStruct>(this BinaryReader reader) where TStruct : struct
        {
            var structSize = Marshal.SizeOf(typeof(TStruct));
            return Unmanaged.BufferToStruct<TStruct>(reader.ReadBytes(structSize));
        }

    }
}
