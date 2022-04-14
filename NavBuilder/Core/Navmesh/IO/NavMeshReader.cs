using NavBuilder.Core.Navmesh.Struct;
using SharpDX;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using RectangleF = SharpDX.RectangleF;

namespace NavBuilder.Core.Navmesh.IO
{
    internal class NavMeshReader : BinaryReader
    {
        #region Fields

        private readonly Encoding _encoding;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NavMeshReader"/> class.
        /// </summary>
        /// <param name="input">The input stream.</param>
        public NavMeshReader(Stream input) : base(input)
        {
            _encoding = Encoding.Default;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Reads a string from the current stream. The string is prefixed with the length, encoded as an integer seven bits at a time.
        /// </summary>
        /// <returns>
        /// The string being read.
        /// </returns>
        public override string ReadString()
        {
            var byteCount = ReadInt32();

            return ReadString(byteCount);
        }

        /// <summary>
        /// Reads the string.
        /// </summary>
        /// <param name="byteCount">The byte count.</param>
        /// <returns></returns>
        public string ReadString(int byteCount)
        {
            return _encoding.GetString(ReadBytes(byteCount)).TrimEnd('\0');
        }

        /// <summary>
        /// Reads the structure.
        /// </summary>
        /// <typeparam name="TStruct">The type of the structure.</typeparam>
        /// <returns></returns>
        public TStruct ReadStruct<TStruct>() where TStruct : struct
        {
            var structSize = Marshal.SizeOf(typeof(TStruct));
            return Unmanaged.BufferToStruct<TStruct>(ReadBytes(structSize));
        }

        /// <summary>
        /// Reads the rectangleF.
        /// </summary>
        /// <returns></returns>
        public RectangleF ReadRectangleF()
        {
            var xMin = ReadSingle(); //Left
            var yMin = ReadSingle(); //Top
            var xMax = ReadSingle(); //Right
            var yMax = ReadSingle(); //Bottom
            return new RectangleF(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        /// <summary>
        /// Reads the angle.
        /// </summary>
        /// <returns></returns>
        public AngleSingle ReadAngle() => ReadStruct<AngleSingle>();

        /// <summary>
        /// Reads the vector2.
        /// </summary>
        /// <returns></returns>
        public Vector2 ReadVector2() => ReadStruct<Vector2>();

        /// <summary>
        /// Reads the vector3.
        /// </summary>
        /// <returns></returns>
        public Vector3 ReadVector3() => ReadStruct<Vector3>();

        /// <summary>
        /// Reads the data.
        /// </summary>
        /// <typeparam name="TData">The type of the data.</typeparam>
        /// <returns></returns>
        public TData ReadData<TData>() where TData : INavData, new()
        {
            var source = new TData();
            source.Read(this);
            return source;
        }

        #endregion Methods
    }
}