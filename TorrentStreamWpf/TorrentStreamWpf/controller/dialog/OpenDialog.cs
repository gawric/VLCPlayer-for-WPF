using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TorrentStreamWpf.controller.dialog
{
    public class OpenDialog : IOpenDialog
    {
       // private string loadingName = "Выбрать файл";
        public string[] FilePath { get; set; }
        public string FileFolderPath { get; set; }


        public void CreateDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ValidateNames = false;
            openFileDialog.CheckFileExists = false;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = true;

            //Вставляет имя выбранного файла в самом начале
           // openFileDialog.FileName = loadingName;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine("Click OpenFileDialog " + openFileDialog.FileNames);
                string[] selectedFiles = openFileDialog.FileNames;
                FilePath = CheckHref(selectedFiles);
            }

        }

        public void CreateFolderDialog(string path)
        {
            using (var openFolderdialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                openFolderdialog.SelectedPath = path;
                System.Windows.Forms.DialogResult result = openFolderdialog.ShowDialog();
                FileFolderPath = openFolderdialog.SelectedPath;
            }
        }



        private string[] CheckHref(string[] FilePath)
        {
            string[] checkedHref = new string[FilePath.Length];

            for (int d = 0; d < FilePath.Length; d++)
            {
                string originalHref = FilePath[d];
              //  string replace = originalHref.Replace(loadingName, "");
               // string replaceM = replacePath(replace);

                checkedHref[d] = originalHref;
            }

            return checkedHref;
        }

        private string replacePath(string href)
        {
            return href.Replace("Выбрать папку", "");
        }
    }
}
