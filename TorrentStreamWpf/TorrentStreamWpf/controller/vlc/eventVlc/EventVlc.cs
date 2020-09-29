using LibVLCSharp.Shared;
using LibVLCSharp.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrentStreamWpf.controller.mainWindows.support.torrent;
using TorrentStreamWpf.controller.torrent.eventTorrent;
using TorrentStreamWpf.model;

namespace TorrentStreamWpf.controller.vlc.eventVlc
{
    public class EventVlc
    {
        private MainObjModel _mom;

        public EventVlc(MainObjModel mom)
        {
            _mom = mom;
        }
        public void VlcStop()
        {

            _mom.main.setSliderValuePlayer(0);

          
            if (_mom.videoView.MediaPlayer != null)
            {
                _mom.videoView.MediaPlayer.Pause();

                System.Threading.Tasks.Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(100); //wait for last frame decoding over.
                    App.Current.Dispatcher.Invoke(new System.Action(() => StopVlc(_mom.videoView)));

                });

                if (_mom.videoStream != null)
                {

                    _mom.videoStream.Dispose();
                    _mom.videoStream = null;
                }
            }
        }

        private void StopVlc(VideoView videoView)
        {

            videoView.MediaPlayer.Stop();
            videoView.MediaPlayer = null;
        

        }

    }
}
