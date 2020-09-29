using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorrentStreamWpf.controller.dialog
{
    public interface IOpenDialog
    {
        void CreateDialog();
        void CreateFolderDialog(string path);
        string[] FilePath { get; set; }   // путь к выбранному файлу
        string FileFolderPath { get; set; } // путь к выбранному папке

    }
}
