using LibVLCSharp.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TorrentStreamWpf.controller.mainWindows.support.torrent;

namespace TorrentStreamWpf.model
{
    public class MainObjModel
    {
        public ClientTorrent clientTorrent { get; set; }
        public Stream videoStream { get; set; }
        public MainWindow main { get; set; }
        public VideoView videoView { get; set; }

        public PlayerModel playerModel { get; set; }

        public bool isPause = false;

        public string pathFiles { get; set; }
        public string pathTorrent { get; set; }

        public bool isFullScreen { get; set; }

        public DispatcherTimer timer { get; set; }

        public bool isCansel { get; set; }
    }

}
