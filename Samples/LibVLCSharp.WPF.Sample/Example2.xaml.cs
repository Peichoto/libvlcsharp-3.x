﻿using System;
using LibVLCSharp.Shared;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MediaPlayer = LibVLCSharp.Shared.MediaPlayer;

namespace LibVLCSharp.WPF.Sample
{
    public partial class Example2 : Window
    {
        LibVLC _libVLC;
        MediaPlayer _mediaPlayer;

        public Example2()
        {
            InitializeComponent();

            var label = new Label
            {
                Content = "TEST",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Foreground = new SolidColorBrush(Colors.Red)
            };
            test.Children.Add(label);

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            // we need the VideoView to be fully loaded before setting a MediaPlayer on it.
            VideoView.Loaded += (sender, e) => VideoView.MediaPlayer = _mediaPlayer;
        }
        
        void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (VideoView.MediaPlayer.IsPlaying)
            {
                VideoView.MediaPlayer.Stop();
            }
        }

        void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (!VideoView.MediaPlayer.IsPlaying)
            {
                VideoView.MediaPlayer.Play(new Media(_libVLC,
                    "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4", FromType.FromLocation));
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            VideoView.Dispose();
        }
    }
}
