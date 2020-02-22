using LibVLCSharp.Shared;
using Microsoft.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace LibVLCSharp.Windows.Net40.Sample
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Core.Initialize();
            var stream1 = new MemoryStream(File.ReadAllBytes(@"E:\xtv\vlc\libvlcsharp-3.x\Samples\LibVLCSharp.Windows.Net40.Sample\media\stream1.mp4"));
            var stream2 = new MemoryStream(File.ReadAllBytes(@"E:\xtv\vlc\libvlcsharp-3.x\Samples\LibVLCSharp.Windows.Net40.Sample\media\stream2.mp4"));
            var stream3 = new MemoryStream(File.ReadAllBytes(@"E:\xtv\vlc\libvlcsharp-3.x\Samples\LibVLCSharp.Windows.Net40.Sample\media\stream3.mp4"));
            var server = new Server();

            server.AppendStream(stream1);
            var libVLC = new LibVLC();

            var media = new Media(libVLC, "http://127.0.0.1:13000", FromType.FromLocation);
            var mp = new MediaPlayer(media);
            mp.Play();

            server.AppendStream(stream2);
            server.AppendStream(stream3);


            Console.ReadKey();
        }
    }
}
