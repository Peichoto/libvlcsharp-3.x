using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LibVLCSharp.Android.Sample
{
    public class SocketServer
    {
        private readonly Socket _listen;
        private Queue<MemoryStream> listStream = new Queue<MemoryStream>();
        public bool IsReady { private set; get; } = false;
        public SocketServer(int port)
        {
            var thread = new Thread(() =>
                   {
                       Init(_listen, port);
                   });
            thread.Start();

        }

        public void Init(Socket listen, int port)
        {
            var listenEndPoint = new IPEndPoint(IPAddress.Loopback, port);
            listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //_listen.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.KeepAlive, true);
            var uri = new Uri("http://127.0.0.1:13000");
            //ServicePointManager.DefaultConnectionLimit = 10;
            //{
            //    var servicePoint = ServicePointManager.FindServicePoint(uri);
            //    servicePoint.SetTcpKeepAlive(true, 86400000, 86400000);
            //}
            listen.Bind(listenEndPoint);
            listen.Listen(1);
            listen.BeginAccept(AcceptConnectionAsync, null);
            IsReady = true;

        }
        public void AppendStream(MemoryStream stream)
        {
            listStream.Enqueue(stream);
        }

        public void Stop()
        {
            _listen.Close();
        }

        private async void AcceptConnectionAsync(IAsyncResult result)
        {
            try
            {
                using (var client = _listen.EndAccept(result))
                using (var stream = new NetworkStream(client))
                using (var writer = new BinaryWriter(stream))
                {
                    //Console.WriteLine("SERVER: accepted new client");

                    //string text;

                    //while ((text = await reader.ReadLineAsync()) != null)
                    //{
                    //    Console.WriteLine("SERVER: received \"" + text + "\"");
                    //    writer.WriteLine(text);
                    //    writer.Flush();
                    //}


                    var header = new StreamWriter(stream);
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
                            var myStream = listStream.Dequeue();
                            var binaryStream = new BinaryWriter(stream);
                            binaryStream.Write(myStream.ToArray());
                            binaryStream.Flush();
                        }
                    }

                }

                //Console.WriteLine("SERVER: end-of-stream");

                // Don't accept a new client until the previous one is done
                //_listen.BeginAccept(_Accept, null);
            }
            catch (ObjectDisposedException)
            {
                Console.WriteLine("SERVER: server was closed");
            }
            catch (SocketException e)
            {
                Console.WriteLine("SERVER: Exception: " + e);
            }
        }
    }
}
