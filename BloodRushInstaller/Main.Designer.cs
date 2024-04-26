namespace BloodRushInstaller
{
    partial class Main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param version="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.steamPath = new System.Windows.Forms.TextBox();
            this.labelSteamPath = new System.Windows.Forms.Label();
            this.labelGameVersion = new System.Windows.Forms.Label();
            this.browseFolderSteam = new System.Windows.Forms.Button();
            this.steamBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(292, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Lancer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(292, 79);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(211, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // steamPath
            // 
            this.steamPath.Location = new System.Drawing.Point(292, 28);
            this.steamPath.Name = "steamPath";
            this.steamPath.Size = new System.Drawing.Size(293, 20);
            this.steamPath.TabIndex = 2;
            // 
            // labelSteamPath
            // 
            this.labelSteamPath.AutoSize = true;
            this.labelSteamPath.Location = new System.Drawing.Point(12, 31);
            this.labelSteamPath.Name = "labelSteamPath";
            this.labelSteamPath.Size = new System.Drawing.Size(298, 13);
            this.labelSteamPath.TabIndex = 3;
            this.labelSteamPath.Text = "Chemin vers le répertoire de la démo (si besoin d\'être différent)";
            // 
            // labelGameVersion
            // 
            this.labelGameVersion.AutoSize = true;
            this.labelGameVersion.Location = new System.Drawing.Point(191, 82);
            this.labelGameVersion.Name = "labelGameVersion";
            this.labelGameVersion.Size = new System.Drawing.Size(95, 13);
            this.labelGameVersion.TabIndex = 4;
            this.labelGameVersion.Text = "Choisir la version...";
            // 
            // browseFolderSteam
            // 
            this.browseFolderSteam.Location = new System.Drawing.Point(579, 28);
            this.browseFolderSteam.Name = "browseFolderSteam";
            this.browseFolderSteam.Size = new System.Drawing.Size(76, 21);
            this.browseFolderSteam.TabIndex = 5;
            this.browseFolderSteam.Text = "Parcourir...";
            this.browseFolderSteam.UseVisualStyleBackColor = true;
            this.browseFolderSteam.Click += new System.EventHandler(this.browseFolderSteam_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.browseFolderSteam);
            this.Controls.Add(this.labelGameVersion);
            this.Controls.Add(this.labelSteamPath);
            this.Controls.Add(this.steamPath);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.Name = "Main";
            this.Text = "BloodRush Installer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox steamPath;
        private System.Windows.Forms.Label labelSteamPath;
        private System.Windows.Forms.Label labelGameVersion;
        private System.Windows.Forms.Button browseFolderSteam;
        private System.Windows.Forms.FolderBrowserDialog steamBrowseDialog;
    }
}

