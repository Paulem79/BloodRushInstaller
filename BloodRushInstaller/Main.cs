using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

// Compatible Windows 7+

namespace BloodRushInstaller
{
    public partial class Main : Form
    {
        public static string zipVersion = null;

        public static List<string> versions = new List<string>();

        public Main()
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"files");
            FindZipFileVersion();
            if (!AreVersionsEquals())
            {
                if(zipVersion == null) new DownloadPopup(PopupActionType.Download).ShowDialog();
                else new DownloadPopup(PopupActionType.Extract).ShowDialog();
            }
            InitializeComponent();
            versions = GetGameVersions();
            comboBox1.DataSource = versions.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo pro = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = AppDomain.CurrentDomain.BaseDirectory + @"files/BloodRush" + zipVersion + "/" + versions.Find(v => v == comboBox1.SelectedValue.ToString()) + "/Hellscape.exe"
            };
            Process.Start(pro);
        }

        public static void FindZipFileVersion()
        {

            string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"files", "BloodRush*.7z");

            if (files.Length > 0)
                foreach (string file in files)
                {
                    string fileName = Path.GetFileName(file).Replace("BloodRush", "").Replace(".7z", "");

                    if (fileName != "" && ToInt(fileName) != null)
                        zipVersion = fileName;
                }

            Console.WriteLine("Version du zip : " + zipVersion);
        }

        public string FindFolderVersion(string folderPath)
        {
            string file = folderPath.Replace("BloodRush", "");
            string fileName = file.Substring(file.LastIndexOf("\\") + 1);

            if (fileName != "")
            {
                Console.WriteLine("Version du dossier : " + fileName);
                return fileName;
            }

            return null;
        }

        public string FindGameVersion(string folderPath)
        {
            string file = folderPath.Replace("BloodRush ", "");
            string fileName = file.Substring(file.LastIndexOf("\\") + 1);

            if (fileName != "")
            {
                Console.WriteLine("Version du jeu : " + fileName);
                return fileName;
            }

            return null;
        }

        public bool AreVersionsEquals()
        {
            string[] folders = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + @"files", "BloodRush*");

            foreach (string folder in folders)
            {
                string folderVersion = FindFolderVersion(folder);
                if (folderVersion != null && folderVersion == zipVersion)
                    return true;
            }

            return false;
        }

        public List<string> GetGameVersions()
        {
            List<string> gamesVersions = new List<string>();

            string[] folders = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + @"files/BloodRush" + zipVersion, "BloodRush*");

            foreach (string folder in folders)
            {
                string folderVersion = FindGameVersion(folder);
                if (folderVersion != null)
                    gamesVersions.Add(folderVersion);
            }

            return gamesVersions;
        }

        public static int? ToInt(string s)
        {
            if (!int.TryParse(s, out int n))
            {
                return null;
            }

            return n;
        }
    }
}
