using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TorrentStreamWpf.model;

namespace TorrentStreamWpf.controller.mainWindows.eventForm
{
    public class SliderValueEvent
    {
        MainObjModel _mom;
        public SliderValueEvent(MainObjModel mainObjectModel)
        {
            _mom = mainObjectModel;
        }
        public void Start(object sender , RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            double value = slider.Value;
            long valueLong = Convert.ToInt64(value);

            if (_mom.videoView.MediaPlayer != null)
            {
                if (_mom.videoView.MediaPlayer.Time != valueLong)
                {
                    _mom.videoView.MediaPlayer.Time = Convert.ToInt64(value);
                }
                else
                {
                   // Console.WriteLine("Не изменен");
                }
            }
            else
            {
               // Console.WriteLine("Video SLider Changed не создан");
            }
        }
    }
}
