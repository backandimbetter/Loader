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
    public partial class Form2 : MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        string HWID;

        private void Form2_Load(object sender, EventArgs e)
        {
            HWID = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            WebClient wb = new WebClient();
            string HWIDLIST = wb.DownloadString("");//keep your hwid here brother
            if (HWIDLIST.Contains(HWID))
            {
                this.Hide();
                var form3 = new Form3();
                form3.Closed += (s, args) => this.Close();
                form3.Show();
            }
            else
            {
                DialogResult dialog = MessageBox.Show("HWID Incorrect", "Promethe.us", MessageBoxButtons.OK);
                if (dialog == DialogResult.OK)
                {
                    MessageBox.Show("Please copy your HWID using the button under the login button and send it accordingly.", "Promethe.us", MessageBoxButtons.OK);
                }
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(HWID);
            metroButton2.Enabled = false;
            metroButton2.Text = "Copied";
        }
    }
}