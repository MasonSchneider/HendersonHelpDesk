using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Henderson_Help_Desk.Code;

namespace Henderson_Help_Desk
{
    public partial class frmCreate : Form
    {

        Database db;
        string sql;
        SqlDataReader reader;
        User thisUser;

        public frmCreate()
        {
            InitializeComponent();
        }

        public frmCreate(User user)
        {
            InitializeComponent();
            thisUser = user;
        }

        private void frmCreate_Load(object sender, EventArgs e)
        {
            db = new Database();
            txtFirstName.Focus();
        }

        private void frmCreate_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.dispose();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (validateMe())
            {

                string name, computer, type, issue, fixzed, date, address, phone, os, zip, state, city, model;
                name = txtFirstName.Text.Replace("'", "''") + ", " + txtLastName.Text.Replace("'", "''");
                computer = txtMaker.Text.Replace("'", "''");
                type = cbType.Text.Replace("'", "''");
                issue = txtProblem.Text.Replace("'", "''");
                fixzed = "False";
                date = DateTime.Now.ToString("M/d/yyyy hh:mm:ss tt").Replace("'", "''");
                address = txtAddress.Text.Replace("'", "''");
                phone = txtPhone.Text.Replace("'", "''");
                os = cbOS.Text.Replace("'", "''");
                zip = txtZip.Text.Replace("'", "''");
                state = txtState.Text.Replace("'", "''");
                city = txtCity.Text.Replace("'", "''");
                model = txtModel.Text.ToString().Replace("'", "''");

                sql = "INSERT INTO Tickets (name, computer, type, model, issue, fixed, date, address, phone, os, zip, state, city)";
                string values = string.Format("'{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}'",
                    name, computer, type, model, issue, fixzed, date, address, phone, os, zip, state, city);
                sql = sql + " VALUES ( " + values + " )";
                reader = db.query(sql);

                thisUser.newTicket((Convert.ToInt32(db.getInsertId("Tickets"))+1).ToString());

                MessageBox.Show("Ticket Created Succesfully!");
            }
            else
                MessageBox.Show("Please fill out all information!");
        }

        private bool validateMe()
        {
            bool safe = true;
            if (txtModel.Text == string.Empty ||txtAddress.Text == string.Empty || txtCity.Text == string.Empty || txtFirstName.Text == string.Empty || txtLastName.Text == string.Empty || txtMaker.Text == string.Empty || !txtPhone.MaskCompleted || txtProblem.Text == string.Empty || txtState.Text == string.Empty || txtZip.Text == string.Empty)
            {
                safe = false;
            }
            return safe;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtAddress.Text = "";
            txtCity.Text = "Henderson";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtMaker.Text = "";
            txtPhone.Text = "270";
            txtProblem.Text = "";
            txtState.Text = "Kentucky";
            txtZip.Text = "42420";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmStart f = new frmStart(thisUser);
            f.Show();
            this.Close();
        }
    }
}
