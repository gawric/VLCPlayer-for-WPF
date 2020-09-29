using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorrentStreamWpf.variable;

namespace TorrentStreamWpf.controller.mainWindows.support
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; } = true;

        [Option('t', "torrent", Required = false, HelpText = "The torrent link to download and play")]
        public string Torrent { get; set; } = "";

        // TODO: If multiple chromecast on the network, allow selecting it interactively via the CLI
        [Option('c', "cast", Required = false, HelpText = "Cast to the chromecast")]
        public bool Chromecast { get; set; }

        [Option('s', "save", Required = false, HelpText = "Whether to save the media file. Defaults to true.")]
        public bool Save { get; set; } = false;

        [Option('p', "path", Required = false, HelpText = "Set the path where to save the media file.")]
        public string Path { get; set; } = StaticVariable.getRootDirProgramm() + "\\" + StaticVariable.tempFolder;
    }
}
