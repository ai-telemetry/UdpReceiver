using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct HandshakeRequestModel
{
    public uint identifier;
    public uint version;
    public uint operationId;
}