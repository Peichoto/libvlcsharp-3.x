using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Android.App;
using Android.Content.Res;
using Android.OS;
using Android.Views;
using Android.Widget;
using LibVLCSharp.Shared;
using VideoView = LibVLCSharp.Platforms.Android.VideoView;

namespace LibVLCSharp.Android.Sample
{
    [Activity(Label = "LibVLCSharp.Android.Sample", MainLauncher = true)]
    public class MainActivity : Activity
    {
        VideoView _videoView;
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);            
        }

        protected override void OnResume()
        {
            base.OnResume();

            Core.Initialize();

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC)
            {
                EnableHardwareDecoding = true
            };

            _videoView = new VideoView(this) { MediaPlayer = _mediaPlayer };



            //_videoView = new VideoView(this) { MediaPlayer = _mediaPlayer };
            //AddContentView(_videoView, new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
            //var media = new Media(_libVLC, "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", FromType.FromLocation);
            //_videoView.MediaPlayer.Play(media);

            var server = new Server();
            var stream1 = new MemoryStream();
            var stream2 = new MemoryStream();
            var stream3 = new MemoryStream();
            var assets = Assets;

            using (var sr = new StreamReader(assets.Open("stream1.mp4")))
            {
                sr.BaseStream.CopyTo(stream1);
            }

            using (var sr = new StreamReader(assets.Open("stream2.mp4")))
            {
                sr.BaseStream.CopyTo(stream2);
            }

            using (var sr = new StreamReader(assets.Open("stream3.mp4")))
            {
                sr.BaseStream.CopyTo(stream3);
            }

            var data = stream1.ToArray();
            var total = data.Length;
            var buffer = 10240;
            var i = 1;

            while (total > 0)
            {
                var skip = (buffer * i);
                var take = (total >= buffer) ? buffer : total;
                total -= take;
                var newData = data.Skip(skip).Take(take).ToArray();

                server.AppendStream(newData);
            }

            //server.AppendStream(stream1);



            while (!server.IsReady)
            {

            }
            _videoView = new VideoView(this) { MediaPlayer = _mediaPlayer };
            AddContentView(_videoView, new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
            var media = new Media(_libVLC, "http://127.0.0.1:13000", FromType.FromLocation);
            _videoView.MediaPlayer.Play(media);







            //var server = new Server();
            //var libVLC = new LibVLC();

            //server.AppendStream(stream1);

            //while (!server.IsReady)
            //{

            //}

            //var media = new Media(libVLC, "http://127.0.0.1:13000", FromType.FromLocation);
            //////Passa o objeto media para o player
            //var mp = new MediaPlayer(media);
            //mp.Play();










            //server.AppendStream(stream1);
            ////Server server = null;
            ////var thread = new Thread(() =>
            ////{
            ////    server = new Server();
            ////});
            ////thread.Start();

            ////Thread.Sleep(10000);

            ////server.AppendStream(stream1);
            ////server.AppendStream(stream2);
            ////server.AppendStream(stream3);


            //////Cria um objeto media passando a Url do video ou Url do Live Stream
            ////var media = new Media(libVLC, "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", FromType.FromLocation);
            ////var media = new Media(libVLC, "http://srv.xtv.group:8880/YuriPeichoto/102030/3166", FromType.FromLocation);

            ////// OU: 
            //////Cria um stream lendo o arquivo e cria um objeto media
            //////var stream = new MemoryStream(File.ReadAllBytes(@"C:\xtv\web\stream1.mp4"));
            ////var media = new Media(libVLC, stream);

            //while (!server.IsReady)
            //{

            //}

            //var media = new Media(libVLC, "http://127.0.0.1:13000", FromType.FromLocation);
            //////Passa o objeto media para o player
            //var mp = new MediaPlayer(media);
            //mp.Play();

            //server.AppendStream(stream2);
            //server.AppendStream(stream3);


            //var server = new SocketServer(13000);
            //var remote = new Socket(SocketType.Stream, ProtocolType.Tcp);
            //var remoteEndPoint = new IPEndPoint(IPAddress.Loopback, 13000);






























            //var server = new Server(stream1);

            ////if (!File.Exists(Path.Combine(CacheDir.Path, fileName)))
            ////{

            ////    var assets = Assets;
            ////    assets.open
            ////    using (StreamReader sr = new StreamReader(assets.Open(fileName)))
            ////    using (StreamWriter sw = new StreamWriter(Path.Combine(CacheDir.Path, fileName), append: false))
            ////        sw.Write(sr.ReadToEnd());
            ////}
            ////string content;
            ////using (StreamReader sr = new StreamReader(Path.Combine(CacheDir.Path, fileName)))
            ////{
            ////    content = sr.ReadToEnd();
            ////}








            //////var stream1 = new MemoryStream(File.ReadAllBytes(@"C:\xtv\web\stream1.mp4"));
            ////var stream2 = new MemoryStream(File.ReadAllBytes(@"C:\xtv\web\stream2.mp4"));
            ////var stream3 = new MemoryStream(File.ReadAllBytes(@"C:\xtv\web\stream3.mp4"));
            ////var server = new Server();
            ////DisplayAlert("Alert", "You have been alerted", "OK");
            ////var dialog = new AlertDialog.Builder(this);
            ////var alert = dialog.Create();
            ////alert.SetTitle("Title");
            ////alert.SetMessage("Complex Alert");
            ////alert.Show();
            ////var thread = new Thread(() => { Server.Main(stream1); });
            ////thread.Start();



            ////server.AppendStream(stream1);
            ////var stream = new MemoryStream(File.ReadAllBytes(@"C:\xtv\web\stream.mp4"));
            ////"http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"
            //AddContentView(_videoView, new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent));
            ////var media = new Media(_libVLC, stream, null);

            //while (!server.IsReady)
            //{ 

            //}

            //var media = new Media(_libVLC, "http://0.0.0.0:3000", FromType.FromLocation);
            ////var media = new Media(_libVLC, "http://srv.xtv.group:8880/YuriPeichoto/102030/3166", FromType.FromLocation);
            //_videoView.MediaPlayer.Play(media);

            ////server.AppendStream(stream2);
            ////server.AppendStream(stream3);
        }

        protected override void OnPause()
        {
            base.OnPause();

            _videoView.MediaPlayer.Stop();
            _videoView.Dispose();
        }
    }
}
