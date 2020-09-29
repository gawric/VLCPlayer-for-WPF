using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TorrentStreamWpf.variable
{
    public static class StaticVariable
    {
        public static string tempFolder = "temp";
        public static string getRootDirProgramm()
        {

            string firstFileFir = System.AppDomain.CurrentDomain.BaseDirectory;

            int end = firstFileFir.Length;
            string endstr = firstFileFir.Substring(end - 1);

            if (endstr.Equals("\\"))
            {
                return System.AppDomain.CurrentDomain.BaseDirectory;
            }
            else
            {
                return System.AppDomain.CurrentDomain.BaseDirectory + "\\";
            }


        }

        public static BitmapImage BitmapToBitmapSource(Uri uriSource)
        {
         

            BitmapImage source2 = new BitmapImage(uriSource);
            using (Stream stream = source2.StreamSource)
            {
                try
                {

                    source2.StreamSource = stream;
                    source2.CacheOption = BitmapCacheOption.None;    // not a mistake - see below

                }
                catch (System.InvalidOperationException a)
                {
                    Console.WriteLine("Variable->BitmapToBitmapSource " + a);
                }

            }

            return source2;
        }


       


        public static string FormatBytes(long bytes)
        {
            string[] Suffix = { "Б", "КБ", "МБ", "ГБ", "ТБ" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

    }
}
