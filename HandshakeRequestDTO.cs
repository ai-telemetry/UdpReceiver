using System.Runtime.InteropServices;

namespace udp_test
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct HandshakeRequestDTO
    {
        public uint identifier;
        public uint version;
        public uint operationId;
    }

}
