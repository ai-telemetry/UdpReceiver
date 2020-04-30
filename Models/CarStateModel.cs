using System;

namespace AiTelemetry.UdpReceiver.Models
{
    /// <summary>
    /// Car model per physics step on the ACServer
    /// 
    /// All quantities are SAE? car coordiantes and directions unless otherwise
    /// specified.
    /// </summary>
    public class CarStateModel
    {

        /// <summary>
        /// Velocity of the car
        /// </summary>
        public float vCar { get => _vCar[2]; }
        public float vCar_kph { get => _vCar[0]; }
        public float vCar_mph { get => _vCar[1]; }
        public float[] _vCar { get; protected set; }

        /// <summary>
        /// Flag if ABS (anti-lock braking system) is currently enabled for the
        /// car
        /// </summary>
        public bool isAbsEnabled { get; protected set; }

        /// <summary>
        /// Flag if ABS is engaged during this physics step
        /// </summary>
        public bool isAbsInAction { get; protected set; }

        /// <summary>
        /// Flag if TC (traction control) is currently enabled
        /// </summary>
        public bool isTcEnabled { get; protected set; }

        /// <summary>
        /// Flag if TC is engaged during this physics step
        /// </summary>
        public bool isTcInAction { get; protected set; }

        /// <summary>
        /// Flag if the car is currently in the pits
        /// </summary>
        public bool isInPit { get; protected set; }

        /// <summary>
        /// Flag if the car's engine limmiter is currenly on
        /// </summary>
        public bool isEngineLimiterOn { get; protected set; }

        /// <summary>
        /// Car acceleration in the y direction in g's
        /// </summary>
        public float gLat { get; protected set; }

        /// <summary>
        /// Car acceleration in the x direction in g's
        /// </summary>
        public float gLong { get; protected set; }

        /// <summary>
        /// Car acceleration in the z direction in g's
        /// </summary>
        public float gVert { get; protected set; }

        /// <summary>
        /// Current lap time in milliseconds
        /// </summary>
        public UInt32 tLap { get; protected set; }

        /// <summary>
        /// Last lap time in milliseconds
        /// </summary>
        public UInt32 tLastLap { get; protected set; }

        /// <summary>
        /// Best lap time of the session in milliseconds
        /// </summary>
        public UInt32 tBestLap { get; protected set; }

        /// <summary>
        /// Current lap number
        /// </summary>
        public UInt32 nLap { get; protected set; }

        /// <summary>
        /// Throttle position ratio 
        /// </summary>
        public float rThrottle { get; protected set; }

        /// <summary>
        /// Brake position ratio 
        /// </summary>
        public float rBrake { get; protected set; }

        /// <summary>
        /// Clutch position ratio 
        /// </summary>
        public float rClutch { get; protected set; }

        /// <summary>
        /// Engine speed in number of RPM 
        /// </summary>
        public float nEngineRpm { get; protected set; }

        /// <summary>
        /// Average steer angle at the front axel
        /// </summary>
        public float aSteer { get; protected set; }

        /// <summary>
        /// Currently selected gear (R=0, N=1, First=2)
        /// </summary>
        public UInt32 nGear { get; protected set; }

        /// <summary>
        /// Center of gravity height from the ground plane in meters?
        /// </summary>
        public float hCoG { get; protected set; }

        /// <summary>
        /// Rotational speed of the wheels in RPM
        /// </summary>
        public float nWheelSpeedFL { get => _nWheelSpeed[0]; }
        public float nWheelSpeedFR { get => _nWheelSpeed[1]; }
        public float nWheelSpeedRL { get => _nWheelSpeed[2]; }
        public float nWheelSpeedRR { get => _nWheelSpeed[3]; }
        public float[] _nWheelSpeed { get; protected set; }

        /// <summary>
        /// Tyre slip angle
        /// </summary>
        public float aTyreSlipFL { get => _aTyreSlip[0]; }
        public float aTyreSlipFR { get => _aTyreSlip[1]; }
        public float aTyreSlipRL { get => _aTyreSlip[2]; }
        public float aTyreSlipRR { get => _aTyreSlip[3]; }
        public float[] _aTyreSlip { get; protected set; }

        /// <summary>
        /// Probably the pneumatic trail - TODO: confirm and rename
        /// </summary>
        public float aContactPatchSlipFL { get => _aContactPatchSlip[0]; }
        public float aContactPatchSlipFR { get => _aContactPatchSlip[1]; }
        public float aContactPatchSlipRL { get => _aContactPatchSlip[2]; }
        public float aContactPatchSlipRR { get => _aContactPatchSlip[3]; }
        public float[] _aContactPatchSlip { get; protected set; }

        /// <summary>
        /// Tyre longitudinal slip ratio 
        /// </summary>
        public float rTyreSlipFL { get => _rTyreSlip[0]; }
        public float rTyreSlipFR { get => _rTyreSlip[1]; }
        public float rTyreSlipRL { get => _rTyreSlip[2]; }
        public float rTyreSlipRR { get => _rTyreSlip[3]; }
        public float[] _rTyreSlip { get; protected set; }

        /// <summary>
        /// Non directional slip
        /// </summary>
        public float ndSlipFL { get => _ndSlip[0]; }
        public float ndSlipFR { get => _ndSlip[1]; }
        public float ndSlipRL { get => _ndSlip[2]; }
        public float ndSlipRR { get => _ndSlip[3]; }
        public float[] _ndSlip { get; protected set; }

        /// <summary>
        /// Tyre vertical load in newtons 
        /// </summary>
        public float fZTyreFL { get => _fZTyre[0]; }
        public float fZTyreFR { get => _fZTyre[1]; }
        public float fZTyreRL { get => _fZTyre[2]; }
        public float fZTyreRR { get => _fZTyre[3]; }
        public float[] _fZTyre { get; protected set; }

        /// <summary>
        /// Linear displacement of dampers in meters? - TODO: verify units
        /// </summary>
        public float dYDamperFL { get => _dYDamper[0]; }
        public float dYDamperFR { get => _dYDamper[1]; }
        public float dYDamperRL { get => _dYDamper[2]; }
        public float dYDamperRR { get => _dYDamper[3]; }
        public float[] _dYDamper { get; protected set; }

        /// <summary>
        /// Tyre self aligning torque in N*m
        /// </summary>
        public float mZTyreFL { get => _mZTyre[0]; }
        public float mZTyreFR { get => _mZTyre[1]; }
        public float mZTyreRL { get => _mZTyre[2]; }
        public float mZTyreRR { get => _mZTyre[3]; }
        public float[] _mZTyre { get; protected set; }

        /// <summary>
        /// Amount of dirt on the tyres; seems to max out around 5
        /// </summary>
        public float nTyreDirtLevelFL { get => _nTyreDirtLevel[0]; }
        public float nTyreDirtLevelFR { get => _nTyreDirtLevel[1]; }
        public float nTyreDirtLevelRL { get => _nTyreDirtLevel[2]; }
        public float nTyreDirtLevelRR { get => _nTyreDirtLevel[3]; }
        public float[] _nTyreDirtLevel { get; protected set; }

        /// <summary>
        /// Tyre camber angle in radians 
        /// </summary>
        public float aTyreCamberFL { get => _aTyreCamber[0]; }
        public float aTyreCamberFR { get => _aTyreCamber[1]; }
        public float aTyreCamberRL { get => _aTyreCamber[2]; }
        public float aTyreCamberRR { get => _aTyreCamber[3]; }
        public float[] _aTyreCamber { get; protected set; }

        /// <summary>
        /// Tyre radius with no load
        /// </summary>
        public float tyreRadiusFL { get => _tyreRadius[0]; }
        public float tyreRadiusFR { get => _tyreRadius[1]; }
        public float tyreRadiusRL { get => _tyreRadius[2]; }
        public float tyreRadiusRR { get => _tyreRadius[3]; }
        public float[] _tyreRadius { get; protected set; }

        /// <summary>
        /// Tyre's current loaded radius 
        /// </summary>
        public float tyreLoadedRadiusFL { get => _tyreLoadedRadius[0]; }
        public float tyreLoadedRadiusFR { get => _tyreLoadedRadius[1]; }
        public float tyreLoadedRadiusRL { get => _tyreLoadedRadius[2]; }
        public float tyreLoadedRadiusRR { get => _tyreLoadedRadius[3]; }
        public float[] _tyreLoadedRadius { get; protected set; }

        /// <summary>
        /// Car's current ride height from the ground in meters
        /// </summary>
        public float hRideFL { get => _hRide[0]; }
        public float hRideFR { get => _hRide[1]; }
        public float hRideRL { get => _hRide[2]; }
        public float hRideRR { get => _hRide[3]; }
        public float[] _hRide { get; protected set; }

        /// <summary>
        /// Ratio of the lap distance completed 
        /// </summary>
        public float rLap { get; protected set; }

        /// <summary>
        /// Pitch angle of the car - Currently always outputs 0
        /// </summary>
        public float aPitch { get; protected set; }

        /// <summary>
        /// Car position in world coordinates 
        /// </summary>
        public float sCarX { get => _sCar[0]; }
        public float sCarY { get => _sCar[1]; }
        public float sCarZ { get => _sCar[2]; }
        public float[] _sCar { get; protected set; }
    }
}