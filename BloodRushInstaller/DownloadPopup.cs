using BloodRushInstaller.utils;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace BloodRushInstaller
{
    public partial class DownloadPopup : Form
    {
        public static readonly string versionsDownloadURL =
            "https://drive.usercontent.google.com/download?id=1deJVXEmBhotZS-Qv3imM0QurAf52L45s&export=download&authuser=0&confirm=t&at=APZUnTVzYdoGnBzkgIamjvcBKE8V%3A1707578935613";
        public static PopupActionType currentActionType = PopupActionType.Download;

        private DateTime lastUpdate;
        private long lastBytes = 0;

        public DownloadPopup(PopupActionType actionType)
        {
            currentActionType = actionType;
            InitializeComponent();
            if(actionType == PopupActionType.Download)
            {
                label1.Text = "Téléchargement des données du jeu...";
                informations.Text = "Vous pouvez faire autre chose en attendant";

                string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"files", "BloodRush*");

                foreach (string file in files)
                {
                    File.Delete(file);
                }

                Thread thread = new Thread(() =>
                {
                    WebClient client = new WebClient();
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    client.DownloadProgressChanged += (sender, e) => downloadSpeed(e.BytesReceived);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    client.DownloadFileAsync(new Uri(versionsDownloadURL), AppDomain.CurrentDomain.BaseDirectory + @"files/BloodRush1.7z");
                });
                thread.Start();
            } else if(actionType== PopupActionType.Extract)
            {
                label1.Text = "Extraction des données du jeu...";
                informations.Text = "Vous pouvez faire autre chose en attendant";

                string[] folders = Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + @"files", "BloodRush*");

                foreach (string folder in folders)
                {
                    Directory.Delete(folder, true);
                }
                Thread t1 = new Thread(() =>
                {
                    ZipManager.ExtractFile("files/BloodRush" + Main.zipVersion + ".7z", "files/BloodRush" + Main.zipVersion, label2, progressBar1, this);
                });
                t1.Start();

                new Thread(() =>
                {
                    t1.Join();
                    this.Invoke((MethodInvoker)delegate
                    {
                        currentActionType = PopupActionType.Play;
                        this.Close();
                    });
                }).Start();
            }

            void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
            {
                this.BeginInvoke((MethodInvoker)delegate {
                    double bytesIn = double.Parse(e.BytesReceived.ToString());
                    double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                    double percentage = bytesIn / totalBytes * 100;
                    int progression = int.Parse(Math.Truncate(percentage).ToString());
                    label2.Text = percentage.ToString("N2") + "%";
                    progressBar1.Value = progression;
                });
            }

            void downloadSpeed(long bytes)
            {
                if (lastBytes == 0)
                {
                    lastUpdate = DateTime.Now;
                    lastBytes = bytes;
                }
                else
                {
                    var now = DateTime.Now;
                    var timeSpan = now - lastUpdate;
                    var bytesChange = bytes - lastBytes;

                    if (timeSpan.Seconds != 0) { 
                        var bytesPerSecond = bytesChange / timeSpan.Seconds;

                        lastBytes = bytes;
                        lastUpdate = now;

                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            informations.Text = (bytesPerSecond / 1e+6).ToString("N2") + "Mb/s";
                        });
                    }
                }
            }

            void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
            {
                this.BeginInvoke((MethodInvoker)delegate {
                    label2.Text = "Terminé !";

                    Main.FindZipFileVersion();

                    currentActionType = PopupActionType.Play;
                    this.Close();
                    new DownloadPopup(PopupActionType.Extract).ShowDialog();
                });
            }
        }

        private void DownloadPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(currentActionType == PopupActionType.Extract || currentActionType == PopupActionType.Download)
            {
                DialogResult d =
                    MessageBox.Show("Fermer l'application pendant cet opération causera un problème, souhaitez-vous vraiment quitter ?", "Fermeture", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if(d == DialogResult.Yes)
                {
                    Application.Exit();
                } else if(d == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }

    public enum PopupActionType {
        None,
        Download,
        Extract,
        Play
    }
}
