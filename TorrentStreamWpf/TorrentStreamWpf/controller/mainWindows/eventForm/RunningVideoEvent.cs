using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrentStreamWpf.controller.mainWindows.support;
using TorrentStreamWpf.controller.mainWindows.support.vlc;
using TorrentStreamWpf.model;

namespace TorrentStreamWpf.controller.mainWindows.eventForm
{
    public class RunningVideoEvent
    {
        private MainObjModel _mom;

        public RunningVideoEvent(MainObjModel mainObjectModel)
        {
            _mom = mainObjectModel;
        }

        public void Start(string path)
        {
            string[] args = { "dotnet run", "-t" + path };
            var result = Parser.Default.ParseArguments<Options>(args).WithParsedAsync(RunOptions);
            //result.WithNotParsed(HandleParseError);
        }

        private async Task RunOptions(Options cliOptions)
        {
            try
            {


                if (!isExistsFiles(cliOptions.Torrent))
                {

                    _mom.videoStream = await _mom.clientTorrent.StartTorrenting(cliOptions , _mom);
                    VlcMedia vlcMedia = new VlcMedia(_mom);
                    await vlcMedia.StartPlayback(_mom.videoStream, cliOptions);
                }
                else
                {
                    if (!isTorrent(cliOptions.Torrent))
                    {

                        VlcMedia vlcMedia = new VlcMedia(_mom);
                        vlcMedia.StartPlayBackLocal(cliOptions.Torrent);
                        _mom.main.setInfoTextPlayer("");
                    }
                    else
                    {

                        _mom.videoStream = await _mom.clientTorrent.StartTorrenting(cliOptions , _mom);
                        VlcMedia vlcMedia = new VlcMedia(_mom);
                        await vlcMedia.StartPlayback(_mom.videoStream, cliOptions);
                    }

                }

            }
            catch (Exception a)
            {
                Console.WriteLine(a);
                _mom.timer.Stop();
                _mom.main.setInfoTextPlayer("Ошибка запуска файла: \n" + a.ToString());
            }


           // _mom.main.ReadKey();
        }

        public bool isExistsFiles(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool isTorrent(string path)
        {
            if (path.LastIndexOf(".torrent") > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void HandleParseError(IEnumerable<Error> error)
        {
            Console.WriteLine($"Error while parsing...");
        }

    }
}
