using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrentStreamWpf.model;

namespace TorrentStreamWpf.controller.mainWindows.eventForm
{
    public class PlayClickEvent
    {
        private MainObjModel _mom;
        private RunningVideoEvent _rve;
        public PlayClickEvent(MainObjModel mom)
        {
             _mom =  mom;
             _rve = new RunningVideoEvent(_mom);
        }
        public void Start()
        {
            _mom.playerModel.InfoText = "Запуск";

            if (!_mom.isPause)
            {
                string pathFiles = _mom.pathFiles;
                string pathTorent = _mom.pathTorrent;

                if (!String.IsNullOrEmpty(pathFiles) &  !_mom.pathFiles.Equals(""))
                {

                    _rve.Start(_mom.pathFiles);
                }
                else
                {
                    if (!String.IsNullOrEmpty(pathTorent) & !_mom.pathTorrent.Equals(""))
                    {

                        _rve.Start(_mom.pathTorrent);
                    }
                    else
                    {
                        _mom.main.setInfoTextPlayer("Не найден файл на вашем диске");
                    }
                }


            }
            else
            {
                _mom.main.setSliderValuePlayer(0);
                _mom.main.VideoView.MediaPlayer.Volume = 70;
                _mom.main.VideoView.MediaPlayer.Play();
                _mom.isPause = false;
            }

        }
    }
}
