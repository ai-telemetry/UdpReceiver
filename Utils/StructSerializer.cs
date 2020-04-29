using System;
using System.Runtime.InteropServices;

namespace AiTelemetry.UdpReceiver.Utils
{
    public static class StructSerializer
    {
        /// <summary>
        /// converts byte[] to struct
        /// </summary>
        public static T Deserialize<T>(byte[] rawData, int position, int rawSize = 0)
        {
            if (rawSize == 0)
                rawSize = Marshal.SizeOf(typeof(T));
            if (rawSize > rawData.Length - position)
                throw new ArgumentException("Not enough data to fill struct. Array length from position: "
                    + (rawData.Length - position)
                    + ", Struct length: "
                    + rawSize);
            IntPtr buffer = Marshal.AllocHGlobal(rawSize);
            Marshal.Copy(rawData, position, buffer, rawSize);
            T retobj = (T)Marshal.PtrToStructure(buffer, typeof(T));
            Marshal.FreeHGlobal(buffer);
            return retobj;
        }

        /// <summary>
        /// converts a struct to byte[]
        /// </summary>
        public static byte[] Serialize(object anything)
        {
            int rawSize = Marshal.SizeOf(anything);
            IntPtr buffer = Marshal.AllocHGlobal(rawSize);
            Marshal.StructureToPtr(anything, buffer, false);
            byte[] rawDatas = new byte[rawSize];
            Marshal.Copy(buffer, rawDatas, 0, rawSize);
            Marshal.FreeHGlobal(buffer);
            return rawDatas;
        }
    }
}
