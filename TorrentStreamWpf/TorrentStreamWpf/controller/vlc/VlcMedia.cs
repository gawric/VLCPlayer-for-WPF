
using LibVLCSharp.Shared;
using LibVLCSharp.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using TorrentStreamWpf.model;

namespace TorrentStreamWpf.controller.mainWindows.support.vlc
{
    public class VlcMedia
    {
        MainObjModel _mom;
        private static List<RendererItem> renderers = new List<RendererItem>();
        public VlcMedia(MainObjModel mom)
        {
            _mom = mom;
        }

        public void StartPlayBackLocal(string path)
        {
            try
            {
                LibVLC libVLC = new LibVLC();
                //string pathEscape = replacePath(path);
                Console.WriteLine(path);
                Media media = new Media(libVLC, path);

                System.Threading.Tasks.Task.Run(() =>
                {
                    App.Current.Dispatcher.Invoke(new System.Action(() => _mom.videoView.MediaPlayer = new LibVLCSharp.Shared.MediaPlayer(media)));
                    App.Current.Dispatcher.Invoke(new System.Action(() => _mom.videoView.MediaPlayer.Play()));
                    
                    //_videoView.MediaPlayer.Play();
                });
                 

            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public string replacePath(string path)
        {
            return path.Replace(" " , "");

        }
        public async Task StartPlayback(Stream stream, Options cliOptions)
        {
            try
            {
                Console.WriteLine("LibVLCSharp -> Loading LibVLC core library...");

                Core.Initialize();

                LibVLC libVLC = new LibVLC();
                if (cliOptions.Verbose)
                    libVLC.Log += (s, e) => Console.WriteLine($"LibVLC -> {e.Module}: {e.Message}");

                var media = new Media(libVLC, new StreamMediaInput(stream));
                MediaPlayer  mediaPlayer = new LibVLCSharp.Shared.MediaPlayer(media);
             

                if (cliOptions.Chromecast)
                {
                    var result = await FindAndUseChromecast(libVLC ,  mediaPlayer);
                    if (!result)
                        return;
                }

                Console.WriteLine("LibVLCSharp -> Starting playback...");
                _mom.isCansel = true;
                _mom.main. setInfoTextPlayer("Запуск Трасляции");
                _mom.videoView.MediaPlayer = mediaPlayer;



                _mom.videoView.MediaPlayer.TimeChanged += c_ThresholdReached;
                _mom.videoView.MediaPlayer.Playing += c_Play;

                await System.Threading.Tasks.Task.Run(() =>
                {
                    App.Current.Dispatcher.Invoke(new System.Action(() => _mom.videoView.MediaPlayer.Play()));
                });


            }
            catch (Exception a)
            {
                Console.WriteLine(a.ToString());
            }


        }

      
        private async Task<bool> FindAndUseChromecast(LibVLC libVLC , MediaPlayer mediaPlayer)
        {
            var rendererDiscoverer = new RendererDiscoverer(libVLC);
            rendererDiscoverer.ItemAdded += RendererDiscoverer_ItemAdded;

            if (rendererDiscoverer.Start())
            {
                Console.WriteLine("LibVLCSharp -> Searching for chromecasts...");
                // give it some time...
                await Task.Delay(2000);
            }
            else
            {
                Console.WriteLine("LibVLCSharp -> Failed starting the chromecast discovery");
            }

            rendererDiscoverer.ItemAdded -= RendererDiscoverer_ItemAdded;

            if (!renderers.Any())
            {
                Console.WriteLine("LibVLCSharp -> No chromecast found... aborting.");
                return false;
            }

            mediaPlayer.SetRenderer(renderers.First());
            return true;
        }

        void c_ThresholdReached(object sender, MediaPlayerTimeChangedEventArgs e)
        {

            //Dispatcher.Invoke(() => Console.WriteLine(_mainWindow.SliderVlc.Value = e.Time));
            App.Current.Dispatcher.Invoke(new System.Action(() => _mom.main.SliderVlc.Value = e.Time));
            Console.WriteLine("The threshold of {0} was reached at {1}.");
            Console.WriteLine(e.Time);

        }

        private static void RendererDiscoverer_ItemAdded(object sender, RendererDiscovererItemAddedEventArgs e)
        {
            Console.WriteLine($"LibVLCSharp -> Found a new renderer {e.RendererItem.Name} of type {e.RendererItem.Type}!");
            renderers.Add(e.RendererItem);
        }

        void c_Play(object sender, EventArgs e)
        {
            try
            {

                _mom.main.setInfoTextPlayer("");
            }
            catch (System.InvalidOperationException a)
            {
                Console.WriteLine("Воспроизведение ошибка " + a.ToString());
                _mom.main.setInfoTextPlayer("Ошибка воспроизведения!!!");
            }


        }



    }
}
