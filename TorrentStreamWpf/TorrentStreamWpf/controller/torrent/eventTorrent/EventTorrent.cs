using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrentStreamWpf.controller.mainWindows.support.torrent;

namespace TorrentStreamWpf.controller.torrent.eventTorrent
{
    public class EventTorrent
    {
        private ClientTorrent _clientTorrent;

        public EventTorrent(ClientTorrent clientTorrent)
        {
            _clientTorrent = clientTorrent;
        }

        public void StopTorrent()
        {
            if (_clientTorrent.getEngine() != null)
            {
                _clientTorrent.getEngine().StopAllAsync();
                _clientTorrent.setEngine(null);

                if (_clientTorrent.getProvider() != null)
                {
                    _clientTorrent.getProvider().StopAsync();
                    _clientTorrent.setProvider(null);
                }
            }

            

        }
    }
}
