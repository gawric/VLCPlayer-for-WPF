using LibVLCSharp.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrentStreamWpf.controller.dialog;
using TorrentStreamWpf.controller.mainWindows.support.torrent;
using TorrentStreamWpf.controller.torrent.eventTorrent;
using TorrentStreamWpf.controller.vlc.eventVlc;
using TorrentStreamWpf.model;

namespace TorrentStreamWpf.controller.mainWindows.eventForm
{
    public class TorrentClickEvent
    {
     
        private EventVlc _eventVlc;
        private RunningVideoEvent _rve;
        private MainObjModel _mom;

        public TorrentClickEvent(MainObjModel _mainObjectModel)
        {
          _mom = _mainObjectModel;
          _rve = new RunningVideoEvent(_mainObjectModel);
        }
        public void Start(ClientTorrent clientTorrent, Stream videoStream)
        {
            MediaStop(clientTorrent, videoStream);
            _mom.pathTorrent = GetPathFile();

            if (!_mom.pathTorrent.Equals("")) _rve.Start(_mom.pathTorrent);
        }

        private string GetPathFile()
        {
            IOpenDialog dialog = new OpenDialog();
            dialog.CreateDialog();
            string[] path = dialog.FilePath;
            if (path == null) return "";
            return path[0];
        }

        public void MediaStop(ClientTorrent clientTorrent, Stream videoStream)
        {
            _eventVlc = new EventVlc(_mom);
            _eventVlc.VlcStop();

            EventTorrent _eventTorrent = new EventTorrent(clientTorrent);
            _eventTorrent.StopTorrent();
        }


    }
}
