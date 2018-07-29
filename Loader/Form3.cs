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
using ManualMapInjection.Injection;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Security.Cryptography;
using System.Net;

namespace Loader
{
    public partial class Form3 : MetroForm
    {
        public Form3()
        {
            InitializeComponent();
        }

        string HWID;
        string version = "v2.3";
        WebClient wb = new WebClient();
        private void Form3_Load(object sender, EventArgs e)
        {

            string webversion = wb.DownloadString("");//version

            if (webversion.Contains(version))
            {
                label2.ForeColor = Color.FromArgb(255, 0, 255, 0);
            }
            else
            {
                label2.ForeColor = Color.FromArgb(255, 216, 17, 65);
            }



            var webRequest2 = WebRequest.Create("");//update date on the csgo cheat
            using (var response = webRequest2.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var date = reader.ReadToEnd();

                label6.Text = date;

            }




            label2.Text = version;

            HWID = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;

            metroComboBox1.Items.Add("Promethe.us");

            if (HWID == "S-1-5-21-94120637-1858883559-677733549-1000")
                label5.Text = "mordekazor";
            else
                label5.Text = "UNKNOWN";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you would like to exit the loader?", "Promethe.us", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();
            var name = "csgo";
            var target = Process.GetProcessesByName(name).FirstOrDefault();
            if (target == null)
            {
                MessageBox.Show("Error: CS:GO is not open! Please start CS:GO then inject", "Promethe.us");
                return;
            }
            if (metroComboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Error: Nothing Selected", "Promethe.us");
                return;
            }
            else if (metroComboBox1.SelectedIndex == 0) // Promethe.us
            {
                var path = @"C:\Users\Public\virus.dll";

                client.DownloadFile("", path); // dll

                var file = File.ReadAllBytes(path);

                if (!File.Exists(path))
                {
                    MessageBox.Show("Error: An unexpected error happened, loader will now restart", "Promethe.us");
                    Application.Restart();
                }

                var injector = new ManualMapInjector(target) { AsyncInjection = true };
                label1.Text = $"hmodule = 0x{injector.Inject(file).ToInt64():x8}";

                if (File.Exists(path))
                    File.Delete(path);
            }

            if (metroCheckBox1.Checked == true)
            {
                Application.Exit();
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)//VAC Bans
        {

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}