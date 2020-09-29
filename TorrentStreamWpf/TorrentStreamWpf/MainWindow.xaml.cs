using CommandLine;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Error = CommandLine.Error;
using static System.Console;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TorrentStreamWpf.model;
using System.Globalization;
using System.Windows.Threading;
using System.Threading;
using MonoTorrent.Streaming;
using MonoTorrent.Client;
using MonoTorrent;
using TorrentStreamWpf.controller.mainWindows.support;
using TorrentStreamWpf.variable;
using System.Windows.Controls;
using TorrentStreamWpf.controller.mainWindows.support.torrent;
using TorrentStreamWpf.controller.mainWindows.support.vlc;
using System.Windows.Controls.Primitives;
using TorrentStreamWpf.controller.dialog;
using TorrentStreamWpf.controller.mainWindows;
using TorrentStreamWpf.controller.mainWindows.eventForm;

namespace TorrentStreamWpf
{

    public partial class MainWindow : Window
    {
     
        private static LibVLC libVLC;
        private PlayerModel playerModel;
  
        private MainEvent _mainEvent;
        private MainObjModel _mainObjectModel;
        public MainWindow()
        {
            InitializeComponent();
            BindingDataContext();
            Initializing();
            CreateObject();
            
        }

        
        private void Initializing()
        {
            MediaPlayer mediaPlayer = VideoView.MediaPlayer;
            InitializingMain init = new InitializingMain(this , libVLC , ref mediaPlayer, playerModel);
            InitializingSliderClick();
        }

        

        private void Files_Click(object sender, RoutedEventArgs e)
        {
            _mainEvent.FileClick();
        }

        private void Files_Torrent(object sender, RoutedEventArgs e)
        {
            _mainEvent.TorrentClick();
        }

        private void CreateObject()
        {
             ClientTorrent _clientTorrent = CreateClientTorrent();
            _mainObjectModel = createMainModel(_clientTorrent, null);
            _mainEvent = new MainEvent(_mainObjectModel);
            
        }
       private void Slider_ValueChanged( object sender, RoutedPropertyChangedEventArgs<double> e)
       {
            _mainEvent.TimeSliderChange(sender , e); 
       }

        private void Exit_Clicked(object sender, RoutedEventArgs e)
        {
            _mainEvent.ExitClickEvent();

        }

        private void Play_Clicked(object sender, RoutedEventArgs e)
        {
            _mainEvent.PlayEvent();

        }


        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            _mainEvent.EscKeyClick(sender, e);
        }

        private void FullScreen_Clicked(object sender, RoutedEventArgs e)
        {

            _mainEvent.FullScreen();
        }

        private void Stop_Clicked(object sender, RoutedEventArgs e)
        {
            _mainEvent.StopClickEvent();
        }

        private void Pause_Clicked(object sender, RoutedEventArgs e)
        {

            _mainEvent.PauseClickEvent();
        }

       
        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(VideoView.MediaPlayer != null)
            {
                VideoView.MediaPlayer.Volume = getSliderVolumeValuePlayer();
            }
          
        }
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            _mainEvent.OnMouseMove( sender,  e);
           
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            _mainEvent.OnMouseLeave(sender, e);

        }

        public void setInfoTextPlayer(string text)
        {
           
            playerModel.InfoText = text;
        }

        public void setSliderValuePlayer(double value)
        {
            
            playerModel.SliderValue = value.ToString();
        }

        private void setSliderVolumeValuePlayer(double value)
        {
          
            playerModel.SliderVolumeValue = value.ToString();
        }

        private int getSliderVolumeValuePlayer()
        {
            string procent = playerModel.SliderVolumeValue;
            double val = double.Parse(procent, CultureInfo.InvariantCulture);
            return (int)val;
        }

     
        private void BindingDataContext()
        {
            playerModel = new PlayerModel();
            playerModel.InfoText = "";
            DataContext = playerModel;
        }

        private MainObjModel createMainModel(ClientTorrent clientTorrent , Stream videoStream)
        {
            MainObjModel obj = new MainObjModel();
            obj.clientTorrent = clientTorrent;
            obj.main = this;
            obj.videoStream = videoStream;
            obj.videoView = VideoView;
            obj.playerModel = playerModel;
            obj.pathFiles = "";
            obj.pathTorrent = "";
            return obj;
        }

        private void SliderClickMouse(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed && e.MouseDevice.Captured == null)
            {
                MouseButtonEventArgs args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left);
                args.RoutedEvent = MouseLeftButtonDownEvent;
                (sender as Thumb).RaiseEvent(args);
            }
          
        }

        public void InitializingSliderClick()
        {
            SliderVlc.ApplyTemplate();
            Thumb thumb = (SliderVlc.Template.FindName("PART_Track", SliderVlc) as Track).Thumb;
            thumb.MouseEnter += new MouseEventHandler(SliderClickMouse);
        }

        private ClientTorrent CreateClientTorrent()
        {
            return new ClientTorrent();
        }



    }
}
