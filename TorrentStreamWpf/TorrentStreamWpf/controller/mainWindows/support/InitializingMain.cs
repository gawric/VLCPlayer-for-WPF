using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml.Serialization;
using TorrentStreamWpf.model;
using TorrentStreamWpf.variable;

namespace TorrentStreamWpf.controller.mainWindows.support
{
    public class InitializingMain
    {
        private MainWindow _main;
        private PlayerModel _player;
        private double msVideo = 5460000;
        public InitializingMain(MainWindow main, LibVLC libVLC , ref MediaPlayer mediaPlayer , PlayerModel player)
        {
            _main = main;
            _player = player;
            setImgMainWindows();
            libVLC = createLibVlc();
            createMediaPlayer(ref mediaPlayer, libVLC);
            setSliderValuePlayer(0);
            setSliderVolumeValuePlayer(70);
            createSlider(msVideo);
        }

       
        private void setSliderVolumeValuePlayer(double value)
        {

            _player.SliderVolumeValue = value.ToString();
        }



        private void setImgMainWindows()
        {
            _main.PlayImg.Source = StaticVariable.BitmapToBitmapSource(new Uri(getPathImg("play.png")));
            _main.PauseImg.Source = StaticVariable.BitmapToBitmapSource(new Uri(getPathImg("pause.png")));
            _main.StopImg.Source = StaticVariable.BitmapToBitmapSource(new Uri(getPathImg("stop.png")));
            _main.FullScreen.Source = StaticVariable.BitmapToBitmapSource(new Uri(getPathImg("full_screen.png")));

        }
        private void setSliderValuePlayer(double value)
        {
            //Console.WriteLine(text);
            _player.SliderValue = value.ToString();
        }


        public LibVLC createLibVlc()
        {
            Core.Initialize();
            return  new LibVLC();
        }

        private void createMediaPlayer(ref MediaPlayer mediaPlayer , LibVLC libVLC)
        {
            mediaPlayer = new MediaPlayer(libVLC);
        }

        private string getPathImg(string nameImg)
        {
            string rootPath = StaticVariable.getRootDirProgramm();
            string imgPath = rootPath + "\\icon\\" + nameImg;

            return imgPath;
        }


        public void createSlider(double timevideo)
        {
            _main.SliderVlc.Maximum = timevideo;

        }
    }
}

