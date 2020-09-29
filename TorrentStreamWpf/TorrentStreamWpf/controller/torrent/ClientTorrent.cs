
using MonoTorrent;
using MonoTorrent.Client;
using MonoTorrent.Streaming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using TorrentStreamWpf.model;
using TorrentStreamWpf.variable;

namespace TorrentStreamWpf.controller.mainWindows.support.torrent
{
    public class ClientTorrent
    {
     
        private StreamProvider _provider;
        private ClientEngine _engine;
        private MainObjModel _mom;
        public ClientTorrent()
        {

        }

        public ClientEngine getEngine()
        {
            return _engine;
        }

        public void setEngine(ClientEngine cli)
        {
            _engine = cli;
        }

        public void setProvider(StreamProvider provider)
        {
            _provider = provider;
        }

        public StreamProvider getProvider()
        {
            return _provider;
        }
        public async Task<Stream> StartTorrenting(Options cliOptions , MainObjModel mom)
        {
            try
            {
                _mom = mom;

                if (_engine == null)
                {
                    _engine = new ClientEngine();
                }

                _mom.timer =  createTime(_engine, _mom.main);




                _mom.main.setInfoTextPlayer("Загрузка torrent файла");
                deleteAllTempFolder(StaticVariable.getRootDirProgramm() + "\\" + StaticVariable.tempFolder);
                string tempFolder = getTempFolder(StaticVariable.getRootDirProgramm() + "\\" + StaticVariable.tempFolder);
                var torrent = Torrent.Load(new Uri(cliOptions.Torrent), System.IO.Path.Combine(tempFolder, "video.torrent"));



                _mom.timer.Start();

                _mom.main.setInfoTextPlayer("Получение нового потока данных");
                _provider = new StreamProvider(_engine, cliOptions.Path, torrent);

                if (cliOptions.Verbose)
                {

                    _provider.Manager.PeerConnected += (o, e) => Console.WriteLine($"MonoTorrent -> Connection succeeded: {e.Peer.Uri}");
                    //provider.Manager.PeerConnected += (o, e) => setInfoTextPlayer("Подключение открыто");
                    // provider.Manager.ConnectionAttemptFailed += (o, e) => Console.WriteLine($"MonoTorrent -> Connection failed: {e.Peer.ConnectionUri} - {e.Reason} - {e.Peer.AllowedEncryption}");
                }


                _mom.main.setInfoTextPlayer("Поиск пиров");
                await _provider.StartAsync();
                

                Stream stream = await _provider.CreateStreamAsync(_engine.Torrents[0].Files[0], true, new CancellationToken());


                return stream;


            }
            catch (MonoTorrent.TorrentException a)
            {
                Console.WriteLine("Критическая ошибка файлов " + a.ToString());
                _mom.main.setInfoTextPlayer("Критическая ошибка запуска трансляции");
                return new MemoryStream();
            }
            catch (System.ArgumentOutOfRangeException a)
            {
                Console.WriteLine("Критическая ошибка файлов " + a.ToString());
                _mom.main.setInfoTextPlayer("Критическая ошибка запуска трансляции: \n" + a);
                Console.WriteLine(a);
                return new MemoryStream();
            }
            catch (System.InvalidOperationException a)
            {
                Console.WriteLine("Критическая ошибка файлов " + a.ToString());
                _mom.main.setInfoTextPlayer("Критическая ошибка запуска трансляции: \n" + a);
                Console.WriteLine(a);
                return new MemoryStream();
            }


        }

      
        private string getTempFolder(string tempFolder)
        {
            if (!Directory.Exists(tempFolder)) Directory.CreateDirectory(tempFolder);

            return tempFolder;

        }
        private void deleteAllTempFolder(string deleteAllTempFolder)
        {
            if (Directory.Exists(deleteAllTempFolder)) Directory.Delete(deleteAllTempFolder, true);
        }
            
        private DispatcherTimer createTime(ClientEngine engine , MainWindow main)
        {
            DispatcherTimer  timer = InitializingTimer();
            TimeTick t = new TimeTick(_mom);
            timer.Tick += t.timer_Tick;

            return timer;
        }

        public DispatcherTimer InitializingTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);

            return timer;
        }


    }
}
