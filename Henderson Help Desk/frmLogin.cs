using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Henderson_Help_Desk.Code;
using Henderson_Help_Desk.Properties;
using System.Net;

namespace Henderson_Help_Desk
{
    public partial class frmLogin : Form
    {

        User user;

        public frmLogin()
        {
            InitializeComponent();
            txtPass.Text = Settings.Default.password;
            txtUser.Text = Settings.Default.username;
            if (txtUser.Text != string.Empty)
                chkRemember.Checked = true;
        }

        private void btnDatabase_Click(object sender, EventArgs e)
        {
            OpenFileDialog db = new OpenFileDialog();
            db.Filter = "Database (.mdf)|*.mdf";
            if (db.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.DBpath = db.FileName;
                Settings.Default.Save();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (validForm())
            {
                user = new User();
                if (user.login(txtUser.Text.ToString().ToLower(), txtPass.Text.ToString(), getIP()))
                {
                    if (chkRemember.Checked)
                    {
                        Settings.Default.username = txtUser.Text.ToString().ToLower();
                        Settings.Default.password = txtPass.Text.ToString();
                        Settings.Default.Save();
                    }
                    else
                    {
                        Settings.Default.username = "";
                        Settings.Default.password = "";
                        Settings.Default.Save();
                    }

                    frmStart form = new frmStart(user);
                    this.Visible = false;
                    form.Show();
                    this.Close();
                }
                else
                    lblError.Text = "There was an error while logging in";
            }
        }

        private bool validForm()
        {
            if (txtUser.Text == string.Empty || txtPass.Text == string.Empty)
            {
                return false;
            }
            else
                return true;
        }

        private string getIP()
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
    }
}
