using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Si vous lisez ceci, oui le code est trop moche bonne journée
namespace Steins_Gate_Translator
{
    public partial class Form1 : Form
    {
        string path_file_now = "";

        private string gameAlias = "sg0";

        private string scxPath;
        private string txtPath;
        private string sc3ToolsExe;

        private bool bold_text = true;

        List<string> finish_translation = new List<string>();

        public Form1()
        {
            InitializeComponent();
            flowLayoutPanel2.WrapContents = false;

            if (File.Exists("save_marks.txt"))
            {
                finish_translation = File.ReadAllLines("save_marks.txt").ToList();
            }
            else
            {
                File.WriteAllLines("save_marks.txt", finish_translation);
            }

            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            scxPath = Path.Combine(basePath, "dialogues", "scx original");
            txtPath = Path.Combine(basePath, "dialogues", "script");
            sc3ToolsExe = Path.Combine(basePath, "dialogues", "sc3tools.exe");


            // Vérifier que tous les dossiers/fichiers existent
            if (!Directory.Exists(txtPath))
                MessageBox.Show($"Le dossier '{txtPath}' n'existe pas. Vérifie que tu as copié le dossier 'dialogues' avec l'exe.");
            if (!Directory.Exists(scxPath))
                MessageBox.Show($"Le dossier '{scxPath}' n'existe pas.");
            if (!File.Exists(sc3ToolsExe))
                MessageBox.Show($"Le fichier '{sc3ToolsExe}' n'existe pas.");

            LaunchFileScript();
        }

        private void LaunchFileScript()
        {
            flowLayoutPanel2.Controls.Clear();
            
            if (Directory.Exists(txtPath))
            {
                string[] allFilesSCX = Directory.GetFiles(txtPath);
                
                foreach (var file in allFilesSCX)
                {
                    Button btn_fichier = new Button();
                    btn_fichier.Click += btn_fichier_Click;
                    btn_fichier.MouseDown += btn_fichier_MouseDown;
                    btn_fichier.Text = Path.GetFileName(file);
                    btn_fichier.Font = new Font("Consolas", 10);
                    btn_fichier.Size = new Size(170, 30);

                    if (finish_translation.Contains(btn_fichier.Text))
                    {
                        btn_fichier.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        btn_fichier.BackColor = Color.FromArgb(1, 180, 180, 180);
                    }
                    
                    flowLayoutPanel2.Controls.Add(btn_fichier);
                }
            }
            else
            {
                MessageBox.Show($"Le dossier {txtPath} n'existe pas !");
            }
        }

        private void btn_fichier_Click(object sender, EventArgs e)
        {
            Button btn_file_scx = sender as Button;
            string new_path = Path.Combine(txtPath, btn_file_scx.Text);
            path_file_now = new_path;
            string lines = File.ReadAllText(new_path);
            lines = lines.Replace("\r", "").Replace("\n", Environment.NewLine);
            richTextBox1.Text = lines;
            label4.Text = btn_file_scx.Text;

            if (bold_text)
            {
                BoldAllP();
            }
        }

        private void btn_fichier_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Button btn_file_scx = sender as Button;
                
                if (finish_translation.Contains(btn_file_scx.Text))
                {
                    finish_translation.Remove(btn_file_scx.Text);
                    btn_file_scx.BackColor = Color.FromArgb(1, 180, 180, 180);
                }
                else
                {
                    finish_translation.Add(btn_file_scx.Text);
                    btn_file_scx.BackColor = Color.LightGreen;
                }
                File.WriteAllLines("save_marks.txt", finish_translation);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if(path_file_now != "")
            {
                File.WriteAllText(path_file_now, richTextBox1.Text);
            }
        }

        private void BoldAllP()
        {
            string text = richTextBox1.Text;

            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);

            int startIndex = 0;

            while (startIndex < text.Length)
            {
                int openBracketIndex = text.IndexOf('[', startIndex);
                if (openBracketIndex == -1)
                    break;

                int closeBracketIndex = text.IndexOf(']', openBracketIndex + 1);
                if (closeBracketIndex == -1)
                    break;

                int length = closeBracketIndex - openBracketIndex + 1;
                richTextBox1.Select(openBracketIndex, length);
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Bold);

                startIndex = closeBracketIndex + 1;
            }

            richTextBox1.Select(0, 0);
        }

        private void DownloadTranslation(string txtPath_final)
        {
            string[] collect_all_file = Directory.GetFiles(txtPath);
            foreach (var file in collect_all_file)
            {
                string lines = File.ReadAllText(file);
                lines = lines.Replace("\r", "").Replace("\n", Environment.NewLine);
                File.WriteAllText(Path.Combine(txtPath_final, Path.GetFileName(file)), lines);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var scxFiles = Directory.GetFiles(scxPath, "*.scx").OrderBy(f => f).ToArray();
                var txtFiles = Directory.GetFiles(txtPath, "*.txt").OrderBy(f => f).ToArray();

                if (scxFiles.Length != txtFiles.Length)
                {
                    MessageBox.Show("Le nombre de fichiers .scx et .txt ne correspond pas !");
                    return;
                }

                for (int i = 0; i < scxFiles.Length; i++)
                {
                    string scxFile = scxFiles[i];
                    string txtFile = txtFiles[i];

                    // Commande pour sc3tools : replace-text fichier.scx fichier.txt alias_jeu
                    string arguments = $"replace-text \"{scxFile}\" \"{txtFile}\" {gameAlias}";

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = sc3ToolsExe,
                        Arguments = arguments,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true
                    };

                    using (Process proc = Process.Start(psi))
                    {
                        string output = proc.StandardOutput.ReadToEnd();
                        string error = proc.StandardError.ReadToEnd();
                        proc.WaitForExit();

                        if (!string.IsNullOrEmpty(output))
                            Console.WriteLine(output);
                        if (!string.IsNullOrEmpty(error))
                            Console.WriteLine("ERREUR: " + error);
                    }
                }

                MessageBox.Show("Compilation des fichiers terminé!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bold_text = checkBox1.Checked;

            if (bold_text)
            {
                BoldAllP();
            }
            else
            {
                richTextBox1.SelectAll();
                richTextBox1.SelectionFont = new Font(richTextBox1.Font, FontStyle.Regular);
                richTextBox1.Select(0, 0);
            }
        }


    }
}
