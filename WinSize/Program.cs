using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace WinSize
{
    static class Program
    {
        public static string FILE_NAME = "SGF.jnlp";

        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string jnlp = loadFile();
            if(jnlp==null)
            {
                MessageBox.Show("File risorse corrotto o mancante. Reinstallare l'applicazione.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MainForm f = new MainForm();
            Screen screenFormIsOn = Screen.FromControl(f);
            if (args.Length > 0 && args[0] == "/firstrun") {
                if(jnlp.IndexOf("width=\"x\"")!=-1 && jnlp.IndexOf("height=\"x\"") != -1)
                {
                    jnlp = jnlp.Replace("width=\"x\"", "width=\"" + screenFormIsOn.WorkingArea.Width + "\"");
                    jnlp = jnlp.Replace("height=\"x\"", "height=\"" + screenFormIsOn.WorkingArea.Height + "\"");
                    File.WriteAllText(FILE_NAME, jnlp);
                    return;
                }  
            }
           
            f.currentScreen = screenFormIsOn;
            int p = jnlp.IndexOf("width=\"") + "width=\"".Length;
            f.w = getNum(jnlp, p);
            p = jnlp.IndexOf("height=\"") + "height=\"".Length;
            f.h = getNum(jnlp, p);
            f.jnlp = jnlp;
            Application.Run(f);
        }

        static string loadFile()
        {
            try
            {
                return File.ReadAllText(FILE_NAME);
            }
            catch(Exception)
            {
                return null;
            }
        }

        static int getNum(string s, int pos)
        {
            string t = "";
            while(s[pos]!='\"')
            {
                t += s[pos];
                pos++;
            }
            return int.Parse(t);
        }
    }
}
