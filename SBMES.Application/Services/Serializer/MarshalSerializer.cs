//using SBMES.Application.Contracts;
//using SBMES.Application.Models;
//using System.Runtime.InteropServices;

//namespace SBMES.Application.Services.Serializer
//{
//    //code inspired by : https://www.robokiwi.com/wiki/dotnet/interop/marshaling/
//    public class MarshalSerializer<T>
//       where T : struct
//    {
//        /// <summary>
//        /// Serializes a structure to a byte array.
//        /// </summary>
//        /// <param name="value">The structure to serialize to a byte array.</param>
//        /// <returns>The object serialized to a byte array, ready to write to a stream.</returns>
//        /// 
//        public byte[] Serialize(T value)
//        {
//            // Get the size of our structure in bytes
//            var structSize = Marshal.SizeOf(value);

//            // This will contain the result, and be returned
//            var bytes = new byte[structSize];

//            // Allocate some unmanaged memory for our structure
//            var pointer = IntPtr.Zero;

//            try
//            {
//                pointer = Marshal.AllocHGlobal(structSize);

//                // Write the structure to the unmanaged memory
//                Marshal.StructureToPtr(value, pointer, false);

//                // Copy the resulting bytes from unmanaged memory to our result array
//                Marshal.Copy(pointer, bytes, 0, structSize);

//                return bytes;
//            }
//            finally
//            {
//                if (pointer != IntPtr.Zero)
//                {
//                    // Free up our unmanaged memory
//                    Marshal.FreeHGlobal(pointer);
//                }
//            }
//        }

//        public T Deserialize(byte[] data)
//        {
//            var pinnedPacket = new GCHandle();

//            T result;

//            try
//            {
//                pinnedPacket = GCHandle.Alloc(data, GCHandleType.Pinned);
//                result = (T)Marshal.PtrToStructure(pinnedPacket.AddrOfPinnedObject(), typeof(T));
//            }
//            finally
//            {
//                pinnedPacket.Free();
//            }

//            return result;
//        }
//    }
//}
