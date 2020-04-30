using System;
using System.Net;
using System.Net.Sockets;
using System.IO.Pipelines;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AiTelemetry.UdpReceiver.Datagrams;
using AiTelemetry.UdpReceiver.Models;
using AiTelemetry.UdpReceiver.Utils;
using AutoMapper;
using System.Buffers;

namespace AiTelemetry.UdpReceiver
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // udp server configuration
            var serverIpAddress = IPAddress.Parse("192.168.1.53");
            var serverPort = 9996;
            var serverEndpoint = new IPEndPoint(serverIpAddress, serverPort);

            // automapper initialization for mapping interop Datagrams => Models 
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
            using var client = new UdpClient();
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

            Console.CancelKeyPress += (sender, e) =>
            {
                var disconnect = new TelemetryServerConfigurationDatagram()
                {
                    identifier = PlatformType.Web,
                    operationId = OperationType.Dismiss,
                    version = 1
                };

                requestByteArray = StructSerializer.Serialize(disconnect);
                client.Send(requestByteArray, Marshal.SizeOf(typeof(TelemetryServerConfigurationDatagram)));
                Console.WriteLine("Exiting Gracefully...");
                Environment.Exit(0);
            };

            await ProcessSubscriberUpdates(client);
        }

        static async Task ProcessSubscriberUpdates(UdpClient client)
        {
            var pipe = new Pipe();
            var writeTask = FillPipeAsync(client, pipe.Writer);
            var readTask = ReadPipeAsync(pipe.Reader);

            await Task.WhenAll(writeTask, readTask);
        }

        static async Task FillPipeAsync(UdpClient client, PipeWriter writer)
        {
            // size of a single CarUpdateDatagram
            const int minBufferSize = 328;

            while (true)
            {
                var memory = writer.GetMemory(minBufferSize);

                var udpResult = await client.ReceiveAsync();
                udpResult.Buffer.CopyTo(memory);
                writer.Advance(udpResult.Buffer.Length);

                var result = writer.FlushAsync();

                if (result.IsCompleted && false)
                    break;
            }

            await writer.CompleteAsync();
        }

        static async Task ReadPipeAsync(PipeReader reader)
        {
            while (true)
            {
                var result = await reader.ReadAsync();
                ReadOnlySequence<byte> buffer = result.Buffer;

                while (TryReadCarUpdateDatagram(ref buffer, out var completeDatagram))
                {
                    var carData = StructSerializer
                        .Deserialize<CarUpdateDatagram>(completeDatagram.ToArray(), 0);

                    // simulate some IO backpressure to see if pipeline works 
                    // Thread.Sleep(100);
                    Console.WriteLine(carData.nEngineRpm);
                }

                reader.AdvanceTo(buffer.Start, buffer.End);

                if (result.IsCompleted)
                {
                    break;
                }
            }

            await reader.CompleteAsync();
        }

        static bool TryReadCarUpdateDatagram(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> completeDatagram)
        {
            var size = buffer.Length;

            if (buffer.Length < 328)
            {
                completeDatagram = default;
                return false;
            }

            completeDatagram = buffer.Slice(0, 328);
            buffer = buffer.Slice(328, buffer.End);
            return true;
        }
    }
}
