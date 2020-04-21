using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct HandshakeResponseModel
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
    public char[] carName;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string driverName;

    public UInt32 identifier;

    public UInt32 version;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string trackName;

    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
    public string trackConfig;
}