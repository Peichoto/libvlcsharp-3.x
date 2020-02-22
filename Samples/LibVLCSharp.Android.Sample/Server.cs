using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LibVLCSharp.Android.Sample
{
    public class Server
    {
        public bool IsReady { private set; get; } = false;
        private Queue<byte[]> listStream = new Queue<byte[]>();

        public Server()
        {
            var thread = new Thread(() =>
            {
                Start();
            });
            thread.Start();
        }

        public void AppendStream(byte[] data)
        {
            listStream.Enqueue(data);
        }
        private async void Start()
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                var port = 13000;
                var localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();
                IsReady = true;
                //Console.Write("Waiting for a connection... ");
                var _client = await server.AcceptTcpClientAsync();


                _client.ReceiveTimeout = int.MaxValue;
                var header = new StreamWriter(_client.GetStream());
                header.Write("HTTP/1.1 200 OK");
                header.Write(Environment.NewLine);
                header.Write("Content-Type: video/mp2t");
                header.Write(Environment.NewLine);
                header.Write("Access-Control-Allow-Origin: *");
                header.Write(Environment.NewLine);
                header.Write("Connection: close");
                header.Write(Environment.NewLine);
                header.Write(Environment.NewLine);
                header.Flush();

                while (true)
                {
                    if (listStream.Count > 0)
                    {
                        var stream = listStream.Dequeue();
                        //var bytes = stream.ToArray();
                        var netStream = _client.GetStream();
                        if (netStream.CanWrite)
                        {
                            netStream.Write(stream);
                            netStream.Flush();
                            //await netStream.WriteAsync(stream);
                            //await netStream.FlushAsync();
                        }
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            catch (Exception ex)
            {
                Console.WriteLine("SocketException: {0}", ex.Message);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
        }
    }
}
