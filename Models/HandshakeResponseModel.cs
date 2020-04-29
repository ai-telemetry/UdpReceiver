namespace AiTelemetry.UdpReceiver.Models
{
    /// <summary>
    /// Model for representing the response of the handshake operation with the
    /// Assetto Corsa remote telemetry server
    /// </summary>
    public class HandshakeResponseModel
    {
        /// <summary>
        /// Name of the car that the player is driving on the AC Server
        /// </summary>
        /// <value></value>
        public string CarName
        {
            get => CleanRawString(_carName);
            set => _carName = value;
        }
        private string _carName;

        /// <summary>
        /// The name of the driver currently selected
        /// </summary>
        /// <value></value>
        public string DriverName
        {
            get => CleanRawString(_driverName);
            set => _driverName = value;
        }
        private string _driverName;

        /// <summary>
        /// Code used for idenfiying status.
        /// Currently expect 4242 as "Not Avaliable" for connection
        /// </summary>
        /// <value></value>
        public int Identifier { get; set; }

        /// <summary>
        /// Version running on the AC Server
        /// </summary>
        /// <value></value>
        public int Version { get; set; }

        /// <summary>
        /// Name of the track on the AC Server
        /// </summary>
        /// <value></value>
        public string TrackName
        {
            get => CleanRawString(_trackName);
            set => _trackName = value;
        }
        private string _trackName;

        /// <summary>
        /// Name of the track configuration (such as indy or gp for brands_hatch).
        /// </summary>
        /// <value></value>
        public string TrackConfig
        {
            get => CleanRawString(_trackConfig);
            set => _trackConfig = value;
        }
        private string _trackConfig;

        public override string ToString()
            => $"Car Name: {CarName}\t Driver Name: {DriverName}\t Track Name: {TrackName}\t Track Config: {TrackConfig}";

        /// <summary>
        /// AC Seems to send handshake responses delemited oddly with % and some garbage following it. 
        /// Method returns everything after Unicode 37 if found, if not returns the raw string
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        private string CleanRawString(string rawString)
            => rawString.Substring(0, rawString.IndexOf("%") > 0 ? rawString.IndexOf("%") : rawString.Length);
    }
}