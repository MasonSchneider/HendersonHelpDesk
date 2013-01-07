using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Henderson_Help_Desk
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmStart f = new frmStart();
            Application.Run(new MyContext());
        }

        public class MyContext : ApplicationContext
        {
            public MyContext()
            {
                frmLogin f = new frmLogin();
                f.Show();
            }

            void Application_Idle(object sender, EventArgs e)
            {
                if (Application.OpenForms.Count == 0)
                    Application.Exit();
            }
        }
    }
}
