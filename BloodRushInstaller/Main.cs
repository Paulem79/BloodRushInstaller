using BloodRushInstaller.utils;
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
        public static string steamDemoFolderPath;

        public static List<GameLocation> versions = new List<GameLocation>();

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
            versions = GetLocalGameVersions();

            steamDemoFolderPath = GetSteamDemo();
            if(steamDemoFolderPath != null)
            {
                versions.Add(new GameLocation(steamDemoFolderPath, "Demo"));
            }

            comboBox1.DataSource = versions.ConvertAll(version => version.version).ToArray();
        }

        private string GetSteamDemo()
        {
            string directory;
            if (steamPath.Text == "")
            {
                directory = ProgramFilesx86() + @"\Steam\steamapps\common\Blood Rush Demo";
            } else
            {
                directory = steamPath.Text;
            }

            if (!Directory.Exists(directory))
            {
                return null;
            }

            return directory;
        }

        static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProcessStartInfo pro = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = versions.Find(v => {
                    return v.version == comboBox1.SelectedValue.ToString();
                }).path + "/Hellscape.exe"
            };
            Console.WriteLine("Lancement de " + pro.FileName);
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

        public List<GameLocation> GetLocalGameVersions()
        {
            List<GameLocation> gamesVersions = new List<GameLocation>();

            string[] folders = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + @"files/BloodRush" + zipVersion, "BloodRush*");

            foreach (string folder in folders)
            {
                string folderVersion = FindGameVersion(folder);
                if (folderVersion != null)
                    gamesVersions.Add(new GameLocation(folder, folderVersion));
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
