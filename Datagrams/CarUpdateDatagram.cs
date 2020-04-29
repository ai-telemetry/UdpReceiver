using System;
using System.Runtime.InteropServices;

namespace AiTelemetry.UdpReceiver.Datagrams
{
    /// <summary>
    /// Datagram structure for receiving updates of the car model per physics 
    /// step on the ACServer
    /// 
    /// All quantities are SAE? car coordiantes and directions unless otherwise
    /// specified.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct CarUpdateDatagram
    {
        /// <summary>
        /// Unknown identifier. Always outputs "a"
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
        public string identifier;

        /// <summary>
        /// Size of the struct in bytes
        /// </summary>
        public UInt32 size;

        /// <summary>
        /// Velocity of the car
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] vCar;

        /// <summary>
        /// Flag if ABS (anti-lock braking system) is currently enabled for the
        /// car
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool isAbsEnabled;

        /// <summary>
        /// Flag if ABS is engaged during this physics step
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool isAbsInAction;

        /// <summary>
        /// Flag if TC (traction control) is currently enabled
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool isTcEnabled;

        /// <summary>
        /// Flag if TC is engaged during this physics step
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool isTcInAction;

        /// <summary>
        /// Flag if the car is currently in the pits
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool isInPit;

        /// <summary>
        /// Flag if the car's engine limmiter is currenly on
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool isEngineLimiterOn;

        /// <summary>
        /// Unknown flag.!-- Marshaled as string to pad struct
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
        public string UnknownFlag1;

        /// <summary>
        /// Car acceleration in the y direction in g's
        /// </summary>
        public float gLat;

        /// <summary>
        /// Car acceleration in the x direction in g's
        /// </summary>
        public float gLong;

        /// <summary>
        /// Car acceleration in the z direction in g's
        /// </summary>
        public float gVert;

        /// <summary>
        /// Current lap time in milliseconds
        /// </summary>
        public UInt32 tLap;

        /// <summary>
        /// Last lap time in milliseconds
        /// </summary>
        public UInt32 tLastLap;

        /// <summary>
        /// Best lap time of the session in milliseconds
        /// </summary>
        public UInt32 tBestLap;

        /// <summary>
        /// Current lap number
        /// </summary>
        public UInt32 nLap;

        /// <summary>
        /// Throttle position ratio 
        /// </summary>
        public float rThrottle;

        /// <summary>
        /// Brake position ratio 
        /// </summary>
        public float rBrake;

        /// <summary>
        /// Clutch position ratio 
        /// </summary>
        public float rClutch;

        /// <summary>
        /// Engine speed in number of RPM 
        /// </summary>
        public float nEngineRpm;

        /// <summary>
        /// Average steer angle at the front axel
        /// </summary>
        public float aSteer;

        /// <summary>
        /// Currently selected gear (R=0, N=1, First=2)
        /// </summary>
        public UInt32 nGear;

        /// <summary>
        /// Center of gravity height from the ground plane in meters?
        /// </summary>
        public float hCoG;

        /// <summary>
        /// Rotational speed of the wheels in RPM
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] nWheelSpeed;

        /// <summary>
        /// Tyre slip angle
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] aTyreSlip;

        /// <summary>
        /// Probably the pneumatic trail - TODO: confirm and rename
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] aContactPatchSlip;

        /// <summary>
        /// Tyre longitudinal slip ratio 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] rTyreSlip;

        /// <summary>
        /// Some slip based measurement - Always outputs 0
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] UnknownSlipParam1;

        /// <summary>
        /// Non directional slip
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] ndSlip;

        /// <summary>
        /// Tyre vertical load in newtons 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] fZTyre;

        /// <summary>
        /// Linear displacement of dampers in meters? - TODO: verify units
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] dYDamper;

        /// <summary>
        /// Tyre self aligning torque in N*m
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] mZTyre;

        /// <summary>
        /// Amount of dirt on the tyres; seems to max out around 5
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] nTyreDirtLevel;

        /// <summary>
        /// Tyre camber angle in radians 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] aTyreCamber;

        /// <summary>
        /// Tyre radius with no load
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] tyreRadius;

        /// <summary>
        /// Tyre's current loaded radius 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] tyreLoadedRadius;

        /// <summary>
        /// Car's current ride height from the ground in meters
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public float[] hRide;

        /// <summary>
        /// Ratio of the lap distance completed 
        /// </summary>
        public float rLap;

        /// <summary>
        /// Pitch angle of the car - Currently always outputs 0
        /// </summary>
        public float aPitch;

        /// <summary>
        /// Car position in world coordinates 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public float[] sCar; // x,y,z
    }
}