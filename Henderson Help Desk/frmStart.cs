using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Henderson_Help_Desk.Properties;
using Henderson_Help_Desk.Code;

namespace Henderson_Help_Desk
{
    public partial class frmStart : Form
    {
        User thisUser;

        public frmStart()
        {
            InitializeComponent();
        }

        public frmStart(User user)
        {
            InitializeComponent();
            thisUser = user;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            frmBrowse f = new frmBrowse(thisUser);
            f.Show();
            this.Hide();
        }

        private void findDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog db = new OpenFileDialog();
            db.Filter = "Database (.mdf)|*.mdf";
            if (db.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.DBpath = db.FileName;
                Settings.Default.Save();
            }
        }

        private void frmStart_Load(object sender, EventArgs e)
        {
            
        }

        private void frmStart_MouseEnter(object sender, EventArgs e)
        {
            if (thisUser.level == "99")
            {
                btnUsers.Enabled = true;
                btnUsers.Visible = true;

                while (this.Height < 215)
                {
                    this.Height++;
                    System.Threading.Thread.Sleep(10);
                }
            }
        }

        private void frmStart_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmCreate creator = new frmCreate(thisUser);
            creator.Show();
            this.Hide();
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCredits f = new frmCredits();
            f.Show();
        }

        private void helpPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHelp f = new frmHelp();
            f.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            frmUsers f = new frmUsers(thisUser);
            f.Show();
            this.Hide();
        }
    }
}
