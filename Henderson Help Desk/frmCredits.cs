using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Henderson_Help_Desk
{
    public partial class frmCredits : Form
    {
        public frmCredits()
        {
            InitializeComponent();
        }

        private void frmCredits_Load(object sender, EventArgs e)
        {
           
        }

        private void frmCredits_Shown(object sender, EventArgs e)
        {
            
        }

        private void frmCredits_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void frmCredits_MouseMove(object sender, MouseEventArgs e)
        {
            int times = 0;
            while (times <= 100)
            {
                System.Threading.Thread.Sleep(100);
                progressBar1.Value = times;
                times += 1;
            }
            this.Close();
        }
    }
}
