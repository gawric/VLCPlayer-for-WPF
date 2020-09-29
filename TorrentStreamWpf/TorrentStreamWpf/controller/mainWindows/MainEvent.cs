using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TorrentStreamWpf.controller.mainWindows.eventForm;
using TorrentStreamWpf.controller.torrent.eventTorrent;
using TorrentStreamWpf.controller.vlc.eventVlc;
using TorrentStreamWpf.model;

namespace TorrentStreamWpf.controller.mainWindows
{
    public class MainEvent
    {
        private MainObjModel _mainObjectModel;
        public MainEvent(MainObjModel mainObjectModel)
        {
            _mainObjectModel = mainObjectModel;
        }


        public void FileClick()
        {
            FileClickEvent _fce = new FileClickEvent(_mainObjectModel);
            _fce.Start();
        }

        public void TorrentClick()
        {
            TorrentClickEvent tce = new TorrentClickEvent(_mainObjectModel);
            tce.Start(_mainObjectModel.clientTorrent, _mainObjectModel.videoStream);
        }

        public void PlayEvent()
        {
            PlayClickEvent pce = new PlayClickEvent(_mainObjectModel);
            pce.Start();
        }

        public void TimeSliderChange(object ob , RoutedPropertyChangedEventArgs<double> e)
        {
            SliderValueEvent slider = new SliderValueEvent(_mainObjectModel);
            slider.Start(ob , e);
        }

      
        public void PauseClickEvent()
        {
            _mainObjectModel.videoView.MediaPlayer.Pause();
            _mainObjectModel.isPause = true;
        }
        public void ExitClickEvent()
        {
            MediaStop(_mainObjectModel);
            Environment.Exit(0);
        }

        public void StopClickEvent()
        {
            MediaStop(_mainObjectModel);
        }

        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_mainObjectModel.isFullScreen == true)
            {
                VisiblePanelEvent visible = new VisiblePanelEvent(_mainObjectModel);
                visible.VisiblePanel();
            }

        }



        public void OnMouseLeave(object sender, MouseEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            if (_mainObjectModel.isFullScreen == true)
            {
                VisiblePanelEvent visible = new VisiblePanelEvent(_mainObjectModel);
                visible.HidePanel(); 
            }

        }
        public void EscKeyClick(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                VisiblePanelEvent visible = new VisiblePanelEvent(_mainObjectModel);
                _mainObjectModel.main.WindowState = WindowState.Normal;
                _mainObjectModel.main.WindowStyle = WindowStyle.SingleBorderWindow;
                visible.VisiblePanel();
                _mainObjectModel.isFullScreen = false;
            }
        }
        public void FullScreen()
        {
            VisiblePanelEvent visible = new VisiblePanelEvent(_mainObjectModel);

            if (_mainObjectModel.main.WindowState == WindowState.Maximized)
            {
                _mainObjectModel.main.WindowState = WindowState.Normal;
                _mainObjectModel.main.WindowStyle = WindowStyle.SingleBorderWindow;
                visible.VisiblePanel();
                _mainObjectModel.isFullScreen = false;
            }
            else
            {
                _mainObjectModel.main.ResizeMode = ResizeMode.NoResize;
                _mainObjectModel.main.WindowState = WindowState.Normal;
                _mainObjectModel.main.WindowStyle = WindowStyle.None;
                _mainObjectModel.main.WindowState = WindowState.Maximized;
                visible.HidePanel();
                _mainObjectModel.main.Activate();
                _mainObjectModel.isFullScreen = true;


            }
        }



        public void MediaStop(MainObjModel _mom)
        {
            EventVlc _eventVlc = new EventVlc(_mainObjectModel);
            _eventVlc.VlcStop();

            EventTorrent _eventTorrent = new EventTorrent(_mom.clientTorrent);
            _eventTorrent.StopTorrent();
        }
    }
}
