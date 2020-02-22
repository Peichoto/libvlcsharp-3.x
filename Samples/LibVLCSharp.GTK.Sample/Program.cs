﻿using Gtk;
using LibVLCSharp.Shared;

namespace LibVLCSharp.GTK.Sample
{
    public class Program
    {
        public static void Main()
        {
            Core.Initialize();

            // Initializes the GTK# app
            Application.Init();

            using (var libvlc = new LibVLC())
            using (var mediaPlayer = new MediaPlayer(libvlc))
            {
                // Create the window in code. This could be done in glade as well, I guess...
                Window myWin = new Window("LibVLCSharp.GTK.Sample");
                myWin.Resize(800, 450);

                // Creates the video view, and adds it to the window
                VideoView videoView = new VideoView { MediaPlayer = mediaPlayer };
                myWin.Add(videoView);

                //Show Everything
                myWin.ShowAll();

                //Starts playing
                using (var media = new Media(libvlc,
                    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4",
                    FromType.FromLocation))
                {
                    mediaPlayer.Play(media);
                }

                myWin.DeleteEvent += (sender, args) => 
                {
                    mediaPlayer.Stop();
                    videoView.Dispose(); 
                    Application.Quit(); 
                };
                Application.Run();
            }
        }
    }
}
