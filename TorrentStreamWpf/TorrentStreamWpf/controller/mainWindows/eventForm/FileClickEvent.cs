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
    public class FileClickEvent
    {
        private MainObjModel _mom;
        private EventVlc _eventVlc;
        private RunningVideoEvent rve;
        public FileClickEvent(MainObjModel mainObjectModel)
        {
            _mom = mainObjectModel;
            rve = new RunningVideoEvent(_mom);
           
        }
        public void Start()
        {
            MediaStop(_mom);
            _mom.pathFiles = GetPathFile();

            if (!_mom.pathFiles.Equals("")) rve.Start(_mom.pathFiles);
        }

        public void MediaStop(MainObjModel _mom)
        {
            _eventVlc = new EventVlc(_mom);
            _eventVlc.VlcStop();

            EventTorrent _eventTorrent = new EventTorrent(_mom.clientTorrent);
            _eventTorrent.StopTorrent();
        }

        private string GetPathFile()
        {
            IOpenDialog dialog = new OpenDialog();
            dialog.CreateDialog();
            string[] path = dialog.FilePath;
            if (path == null) return "";
            return path[0];
        }
    }
}
