using System;
using System.Runtime.InteropServices;

namespace AiTelemetry.UdpReceiver.Datagrams
{
    /// <summary>
    /// [not used in the current Remote Telemtry version by AC] In future 
    /// versions it will identify the platform type of the client. This will 
    /// be used to adjust a specific behaviour for each platform
    /// </summary>
    public enum PlatformType : UInt32
    {
        IPhone = 0,
        IPad = 1,
        AndroidPhone = 2,
        AndroidTablet = 3,
        Web = 1,
        Default = 1
    }

    /// <summary>
    /// Type of operations to be requested by the client
    /// </summary>
    public enum OperationType : UInt32
    {
        // This operation identifier must be set when the client wants to start
        // the comunication.
        Handshake = 0,
        // This operation identifier must be set when the client wants to be 
        // updated from the specific ACServer
        SubscriberUpdate = 1,
        // This operation identifier must be set when the client wants to be 
        // updated from the specific ACServer just for SPOT Events (e.g.: the 
        // end of a lap)
        SubscriberSpot = 2,
        // This operation identifier must be set when the client wants to leave 
        // the comunication with ACServer
        Dismiss = 3
    }

    /// <summary>
    /// Datagram structure for sending configuration requests to the ACServer
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TelemetryServerConfigurationDatagram
    {
        public PlatformType identifier;

        /// <summary>
        /// [not used in the current Remote Telemtry version by AC] In future 
        /// version this field will identify the AC Remote Telemetry version 
        /// that the device expects to speak with.
        /// </summary>
        public UInt32 version;

        public OperationType operationId;
    }

}
