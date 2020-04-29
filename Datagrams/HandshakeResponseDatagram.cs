using System;
using System.Runtime.InteropServices;

namespace AiTelemetry.UdpReceiver.Datagrams
{
    /// <summary>
    /// Datagram structure for receiving a response from a handshake request
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct HandshakeResponseDatagram
    {
        /// <summary>
        /// Name of the car that the player is driving on the AC Server
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string carName;

        /// <summary>
        /// Name of the driver running on the AC Server
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string driverName;

        /// <summary>
        /// Code used for idenfiying status.
        /// Currently expect 4242 as "Not Avaliable" for connection
        /// </summary>
        public UInt32 identifier;

        /// <summary>
        /// Version running on the AC Server
        /// </summary>
        public UInt32 version;

        /// <summary>
        /// Name of the track on the AC Server
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string trackName;

        /// <summary>
        /// Name of the track configuration (such as indy or gp for brands_hatch).
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string trackConfig;
    }
}
