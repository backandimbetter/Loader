using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Loader
{
    public partial class Form1 : MetroForm
    {
        string oldexepath;

        string version = "v2.3";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WebClient wb = new WebClient();

            var ping = new System.Net.NetworkInformation.Ping();

            var result = ping.Send("www.github.com");

            if (result.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                WebRequest request = WebRequest.Create(""); // version
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show("Could Not Connect");
                }
                else
                {
                    timer1.Start();
                }
            }
            else
            {
                MessageBox.Show("Could not connect");
            }

            string path = @"C:\Users\Public\LoaderPath.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        oldexepath = s;
                    }

                    sr.Close();
                }
            }

            if (File.Exists(oldexepath))
            {
                File.Delete(oldexepath);
                File.Delete(path);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            WebClient wb = new WebClient();
            string latestversion = wb.DownloadString(""); //version

            if (!latestversion.Contains(version))
            {
                pictureBox1.Visible = true;
                pictureBox4.Visible = false;
                metroLabel3.Text = "Status: Update Needed!";
                timer1.Stop();
                DialogResult dialogResult = MessageBox.Show("Download Update?", "Promethe.us", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    timer2.Start();
                }
                else
                {
                    timer3.Start();
                }

            }
            else
            {
                pictureBox1.Visible = false;
                pictureBox4.Visible = true;
                metroLabel2.Text = "Up to date!";
                metroLabel3.Text = "Status: No updates found";
                timer3.Start();
                timer1.Stop();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            metroLabel3.Text = "Status: Downloading update";

            timer2.Stop();

            string path = @"C:\Users\Public\LoaderPath.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(System.Reflection.Assembly.GetEntryAssembly().Location);
                }
            }

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);

            // Downloading the new version
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile("", Directory.GetCurrentDirectory() + "/" + finalString + ".exe"); //loader
            Process.Start(Directory.GetCurrentDirectory() + "/" + finalString + ".exe");
            Application.Exit();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
            pictureBox1.Visible = false;
            pictureBox4.Visible = false;
            metroLabel2.Text = "Program is on appropriate Drive!";
            metroLabel3.Text = "Status: File-Path Verified";
            timer3.Stop();
            timer4.Start();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Stop();
            this.Hide();
            var form2 = new Form2();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }
    }
}