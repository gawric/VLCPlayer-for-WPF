using MonoTorrent.Client;
using MonoTorrent.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TorrentStreamWpf.model;
using TorrentStreamWpf.variable;

namespace TorrentStreamWpf.controller.mainWindows.support
{
    public class TimeTick
    {
        MainObjModel _mom;
        public TimeTick(MainObjModel mom)
        {
            _mom = mom;

        }
        public void timer_Tick(object sender, EventArgs e)
        {
           
                if (_mom.clientTorrent.getEngine() != null)
                {
                    // Console.WriteLine("Скачано на диск "+ provider.Manager.Torrent.Files[0].BytesDownloaded);
                    long bytes = _mom.clientTorrent.getEngine().Torrents[0].Files[0].BytesDownloaded();

                    if (bytes > 0)
                    {
                    _mom.main.setInfoTextPlayer("Загрузка данных  \n     " + StaticVariable.FormatBytes(bytes));
                        if (_mom.isCansel)
                        {

                            _mom.isCansel = false;
                            _mom.main.setInfoTextPlayer("");
                            _mom.timer.Stop();

                        }

                    }
                }


          
        }
    }
}
