using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AiTelemetry.UdpReceiver.Datagrams;
using AiTelemetry.UdpReceiver.Models;
using AiTelemetry.UdpReceiver.Utils;
using AutoMapper;

namespace AiTelemetry.UdpReceiver
{
    class Program
    {
        // currently disabling will make main actually use async later
#pragma warning disable CS1998
        static async Task Main(string[] args)
        {
#pragma warning restore CS19998
            // udp server configuration
            var serverIpAddress = IPAddress.Parse("192.168.1.53");
            var serverPort = 9996;
            var serverEndpoint = new IPEndPoint(serverIpAddress, serverPort);

            // automapper initialization for mapping interop DTOs => Models 
            // NTS: Install-Package AutoMapper.Extensions.Microsoft.DependencyInjection
            MapperConfiguration config = new MapperConfiguration(
                cfg => cfg.CreateMap<HandshakeResponseDatagram, HandshakeResponseModel>()
            );

            var mapper = new Mapper(config);

            var handshakeRequest = new TelemetryServerConfigurationDatagram()
            {
                identifier = PlatformType.Web,
                version = 1,
                operationId = OperationType.Handshake
            };

            // serialize request 
            var requestByteArray = StructSerializer.Serialize(handshakeRequest);

            // send request and block for response 
            var client = new UdpClient();
            client.Connect(serverEndpoint);
            client.Send(requestByteArray, Marshal.SizeOf(typeof(TelemetryServerConfigurationDatagram)));
            var responseByteArray = client.Receive(ref serverEndpoint);

            // deserialize and map to model 
            var handshakeResponse = StructSerializer.Deserialize<HandshakeResponseDatagram>(responseByteArray, 0);
            var handshakeOutput = mapper.Map<HandshakeResponseDatagram, HandshakeResponseModel>(handshakeResponse);

            // print result of handshake
            Console.WriteLine(handshakeOutput);

            var updateRequest = new TelemetryServerConfigurationDatagram()
            {
                identifier = PlatformType.Web,
                operationId = OperationType.SubscriberUpdate,
                version = 1
            };

            requestByteArray = StructSerializer.Serialize(updateRequest);
            client.Send(requestByteArray, Marshal.SizeOf(typeof(TelemetryServerConfigurationDatagram)));

            responseByteArray = client.Receive(ref serverEndpoint);
            var carData = StructSerializer.Deserialize<CarUpdateDatagram>(responseByteArray, 0);

            var disconnect = new TelemetryServerConfigurationDatagram()
            {
                identifier = PlatformType.Web,
                operationId = OperationType.Dismiss,
                version = 1
            };

            requestByteArray = StructSerializer.Serialize(disconnect);
            client.Send(requestByteArray, Marshal.SizeOf(typeof(TelemetryServerConfigurationDatagram)));
        }
    }
}
