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
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        public string identifier;

        /// <summary>
        /// Size of the struct in bytes
        /// </summary>
        public UInt32 size;

        /// <summary>
        /// Velocity of the car
        /// </summary>
        public struct vCar
        {
            public float kph;
            public float mph;
            public float ms;
        }

        /// <summary>
        /// Flag if ABS (anti-lock braking system) is currently enabled for the
        /// car
        /// </summary>
        [MarshalAs(UnmanagedType.U8)]
        public bool isAbsEnabled;

        /// <summary>
        /// Flag if ABS is engaged during this physics step
        /// </summary>
        [MarshalAs(UnmanagedType.U8)]
        public bool isAbsInAction;

        /// <summary>
        /// Flag if TC (traction control) is currently enabled
        /// </summary>
        [MarshalAs(UnmanagedType.U8)]
        public bool isTcEnabled;

        /// <summary>
        /// Flag if TC is engaged during this physics step
        /// </summary>
        [MarshalAs(UnmanagedType.U8)]
        public bool isTcInAction;

        /// <summary>
        /// Flag if the car is currently in the pits
        /// </summary>
        [MarshalAs(UnmanagedType.U8)]
        public bool isInPit;

        /// <summary>
        /// Flag if the car's engine limmiter is currenly on
        /// </summary>
        [MarshalAs(UnmanagedType.U8)]
        public bool isEngineLimiterOn;

        /// <summary>
        /// Unknown flag.!-- Marshaled as string to pad struct
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
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
        public struct nWheelSpeed
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Tyre slip angle
        /// </summary>
        public struct aTyreSlip
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Probably the pneumatic trail - TODO: confirm and rename
        /// </summary>
        public struct aContactPatchSlip
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Tyre longitudinal slip ratio 
        /// </summary>
        public struct rTyreSlip
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Some slip based measurement - Always outputs 0
        /// </summary>
        public struct UnknownWheelStruct1
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Non directional slip
        /// </summary>
        public struct ndSlip
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Tyre vertical load in newtons 
        /// </summary>
        public struct fZTyre
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Linear displacement of dampers in meters? - TODO: verify units
        /// </summary>
        public struct dYDamper
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Tyre self aligning torque in N*m
        /// </summary>
        public struct mZTyre
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Amount of dirt on the tyres; seems to max out around 5
        /// </summary>
        public struct nTyreDirtLevel
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Tyre camber angle in radians 
        /// </summary>
        public struct aTyreCamber
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Tyre radius with no load
        /// </summary>
        public struct tyreRadius
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Tyre's current loaded radius 
        /// </summary>
        public struct tyreLoadedRadius
        {
            public float fL, fR, rL, rR;
        }

        /// <summary>
        /// Car's current ride height from the ground in meters
        /// </summary>
        public struct hRide
        {
            public float fL, fR, rL, rR;
        }

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
        public struct sCar
        {
            public float x;
            public float y;
            public float z;
        }
    }
}