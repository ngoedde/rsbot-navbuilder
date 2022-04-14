using System;
using System.Drawing;
using System.IO;
using NavBuilder.Client;
using RoadShark.Pk2.Types;

namespace NavBuilder.Core
{
    internal static class Pk2Extensions
    {
        /// <summary>
        /// Gets the stream from a DDJ file in the Pk2 archive and converts the DDS Format to System.Image.
        /// </summary>
        /// <param name="file">The archive.</param>
        /// <returns></returns>
        public static Image ToImage(this PK2File file)
        {
            if (file == null) return new Bitmap(256, 256);

            var ddjBuffer = file.GetData();
            var ddsBuffer = new byte[ddjBuffer.Length - 20];
            Array.ConstrainedCopy(ddjBuffer, 20, ddsBuffer, 0, ddjBuffer.Length - 20); //Cuts the first 20 bytes.
            
            return DDSImage.ToBitmap(ddsBuffer);
        }

    }
}
