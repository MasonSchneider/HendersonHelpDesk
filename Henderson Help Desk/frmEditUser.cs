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

namespace Henderson_Help_Desk
{
    public partial class frmEditUser : Form
    {
        Database db;
        string sql;
        SqlDataReader reader;
        User thisUser;
        string SelectedId;

        public frmEditUser()
        {
            InitializeComponent();
        }

        public frmEditUser(User user, string id)
        {
            thisUser = user;
            SelectedId = id;
        }
    }
}
