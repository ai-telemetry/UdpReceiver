using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace udp_test
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            var serverIpAddress = IPAddress.Parse("192.168.1.203");
            var serverPort = 9996;
            var serverEndpoint = new IPEndPoint(serverIpAddress, serverPort);

            var handshakeRequest = new HandshakeRequestModel()
            {
                identifier = 1,
                version = 1,
                operationId = 0
            };

            var requestByteArray = StructTools.RawSerialize(handshakeRequest);

            var client = new UdpClient();
            client.Connect(serverEndpoint);
            client.Send(requestByteArray, sizeof(HandshakeRequestModel));
            var responseByteArray = client.Receive(ref serverEndpoint);
            var handshakeResponse = StructTools.RawDeserialize<HandshakeResponseModel>(responseByteArray, 0);

            Console.WriteLine(new string(handshakeResponse.carName));
        }
    }
}
