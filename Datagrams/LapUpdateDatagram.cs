using System;
using System.Runtime.InteropServices;

namespace AiTelemetry.UdpReceiver.Datagrams
{
    /// <summary>
    /// Datagram structure for receiving updates when a spot event is triggered
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct LapUpdateDatagram
    {
        /// <summary>
        /// Car identifier number 
        /// </summary>
        public UInt32 nCarIdentifier;

        /// <summary>
        /// Lap number event occured on
        /// </summary>
        public UInt32 nLap;

        /// <summary>
        /// Name of the driver running on the AC Server
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string driverName;

        /// <summary>
        /// Name of the car that the player is driving on the AC Server
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string carName;

        /// <summary>
        /// Lap time for the lap indicated by nLap
        /// </summary>
        public UInt32 tLap;
    }
}