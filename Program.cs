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
using System.IO;
using CsvHelper;
using System.Globalization;

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
                cfg =>
                {
                    cfg.CreateMap<HandshakeResponseDatagram, HandshakeResponseModel>();
                    cfg.CreateMap<CarUpdateDatagram, CarStateModel>();
                }
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
            var handshakeOutput = mapper.Map<HandshakeResponseModel>(handshakeResponse);

            // print result of handshake
            Console.WriteLine(handshakeOutput);

            var updateRequest = new TelemetryServerConfigurationDatagram()
            {
                identifier = PlatformType.Web,
                operationId = OperationType.SubscriberUpdate,
                version = 1
            };

            requestByteArray = StructSerializer.Serialize(updateRequest);
            await client.SendAsync(requestByteArray, Marshal.SizeOf(typeof(TelemetryServerConfigurationDatagram)));

            var path = $"telemetry/" +
                $"{handshakeOutput.DriverName}/" +
                $"{handshakeOutput.TrackName}/" +
                $"{handshakeOutput.CarName}/";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using var streamWriter = new StreamWriter($"{path}/{DateTime.Now.ToFileTime()}.csv");
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteHeader(typeof(CarStateModel));

            Console.CancelKeyPress += (sender, e) =>
            {
                csvWriter.Flush();
                streamWriter.Close();
                csvWriter.Dispose();
                streamWriter.Dispose();

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

            await ProcessSubscriberUpdates(client, csvWriter, mapper);
        }

        static async Task ProcessSubscriberUpdates(UdpClient client, CsvWriter csvWriter, Mapper mapper)
        {
            var pipe = new Pipe();
            var writeTask = FillPipeAsync(client, pipe.Writer);
            var readTask = ReadPipeAsync(pipe.Reader, csvWriter, mapper);

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

        static async Task ReadPipeAsync(PipeReader reader, CsvWriter csvWriter, Mapper mapper)
        {
            while (true)
            {
                var result = await reader.ReadAsync();
                ReadOnlySequence<byte> buffer = result.Buffer;

                while (TryReadCarUpdateDatagram(ref buffer, out var completeDatagram))
                {
                    var carData = StructSerializer
                        .Deserialize<CarUpdateDatagram>(completeDatagram.ToArray(), 0);

                    var carModel = mapper.Map<CarStateModel>(carData);
                    csvWriter.NextRecord();
                    csvWriter.WriteRecord(carModel);
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
