using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using AutoMapper;

namespace udp_test
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            // udp server configuration
            var serverIpAddress = IPAddress.Parse("192.168.1.203");
            var serverPort = 9996;
            var serverEndpoint = new IPEndPoint(serverIpAddress, serverPort);

            // automapper initialization for mapping interop DTOs => Models 
            // NTS: Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection
            MapperConfiguration config = new MapperConfiguration(
                cfg => cfg.CreateMap<HandshakeResponseDTO, HandshakeResponseModel>()
            );

            var mapper = new Mapper(config);

            // set operationId to 0 to perform handshake with AC Server
            var handshakeRequest = new HandshakeRequestDTO()
            {
                identifier = 1,
                version = 1,
                operationId = 0
            };

            // serialize request 
            var requestByteArray = StructTools.RawSerialize(handshakeRequest);

            // send request and block for response 
            var client = new UdpClient();
            client.Connect(serverEndpoint);
            client.Send(requestByteArray, Marshal.SizeOf(typeof(HandshakeRequestDTO)));
            var responseByteArray = client.Receive(ref serverEndpoint);

            // deserialize and map to model 
            var handshakeResponse = StructTools.RawDeserialize<HandshakeResponseDTO>(responseByteArray, 0);
            var handshakeOutput = mapper.Map<HandshakeResponseDTO, HandshakeResponseModel>(handshakeResponse);

            // print result of handshake
            Console.WriteLine(handshakeOutput);
        }
    }
}
