using System;
using System.Runtime.InteropServices;

namespace udp_test
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct HandshakeResponseDTO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string carName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string driverName;

        public UInt32 identifier;

        public UInt32 version;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string trackName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string trackConfig;
    }
}
