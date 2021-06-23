using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WinSize
{
    public partial class MainForm : Form
    {
        public int w;
        public int h;
        private int org_w;
        private int org_h;
        public string jnlp;

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Size = new Size(w, h);
            org_w = w;
            org_h = h;

            updateLabel();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            w = this.Size.Width;
            h = this.Size.Height;

            updateLabel();
        }

        private void updateLabel()
        {
            labelWH.Text = w + " x " + h;
        }

        private void quantoLoSchermoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            w = currentScreen.WorkingArea.Width;
            h = currentScreen.WorkingArea.Height;

            this.Size = new Size(w, h);
            updateLabel();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            string[] p = mi.Text.Split('x');
            w = int.Parse(p[0]);
            h = int.Parse(p[1]);

            this.Size = new Size(w, h);
            updateLabel();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
            ab.Dispose();
        }

        private void uscitaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool Changed()
        {
            return (w != org_w) || (h != org_h);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Changed())
            {
                DialogResult res = MessageBox.Show("Salvare le modifiche?", "Attenzione", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                if (res == DialogResult.Yes)
                {
                    jnlp = jnlp.Replace("width=\"" + org_w + "\"", "width=\"" + w + "\"");
                    jnlp = jnlp.Replace("height=\"" + org_h + "\"", "height=\"" + h + "\"");
                    File.WriteAllText(Program.FILE_NAME, jnlp);
                }
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            int perc = Convert.ToInt32(mi.Text.Substring(0, mi.Text.Length - 1));
            this.Opacity = 1- (perc / 100.0);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Per utilizzare al meglio l'applicazione impostare un livello di trasparenza adeguata dal menu e sovrappore l'applicazione alla finestra di SGF impostando la dimensione più consona (si raccomanda di lasciare almeno 2cm a destra e in basso per eventuali form che potrebbero aprirsi in SGF.", "Suggerimenti", MessageBoxButtons.OK);
        }
    }
}
