namespace udp_test
{
    public class HandshakeResponseModel
    {
        private string _carName;
        public string CarName
        {
            get => CleanRawString(_carName);
            set => _carName = value;
        }

        private string _driverName;
        public string DriverName
        {
            get => CleanRawString(_driverName);
            set => _driverName = value;
        }

        public int Identifier { get; set; }

        public int Version { get; set; }

        private string _trackName;
        public string TrackName
        {
            get => CleanRawString(_trackName);
            set => _trackName = value;
        }

        private string _trackConfig;
        public string TrackConfig
        {
            get => CleanRawString(_trackConfig);
            set => _trackConfig = value;
        }

        public override string ToString()
            => $"Car Name: {CarName}\t Driver Name: {DriverName}\t Track Name: {TrackName}\t Track Config: {TrackConfig}";

        /// <summary>
        /// AC Seems to send handshake responses delemited oddly with % and some UTF-16LE garbage following it. 
        /// Method returns everything after Unicode 37 if found, if not returns the raw string
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        private string CleanRawString(string rawString)
            => rawString.Substring(0, rawString.IndexOf("%") > 0 ? rawString.IndexOf("%") : rawString.Length);
    }
}