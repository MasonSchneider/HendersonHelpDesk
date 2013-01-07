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
    public partial class frmEdit : Form
    {

        string id;
        Database db;
        string sql;
        SqlDataReader reader;
        User thisUser;

        public frmEdit()
        {
            InitializeComponent();
        }

        public frmEdit(string TempId, User user)
        {
            id = TempId;
            thisUser = user;
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (validateMe())
            {
                string name, computer, type, issue, fixzed, address, phone, os, zip, state, city, model, solution;
                name = txtFirstName.Text.Replace("'", "''") + ", " + txtLastName.Text.Replace("'", "''");
                computer = txtMaker.Text.Replace("'", "''");
                type = cbType.Text.Replace("'", "''");
                issue = txtProblem.Text.Replace("'", "''");
                fixzed = cbFixed.Text == "Yes" ? "true" : "false";
                address = txtAddress.Text.Replace("'", "''");
                phone = txtPhone.Text.Replace("'", "''");
                os = cbOS.Text.Replace("'", "''");
                zip = txtZip.Text.Replace("'", "''");
                state = txtState.Text.Replace("'", "''");
                city = txtCity.Text.Replace("'", "''");
                model = txtModel.Text.ToString().Replace("'", "''");
                solution = txtSolution.Text.ToString().Replace("'", "''");
                
                sql = "UPDATE Tickets SET ";
                string values = string.Format("name='{0}', computer='{1}', type='{2}', model='{3}', issue='{4}', fixed='{5}', solution='{6}', address='{7}', phone='{8}', os='{9}', zip='{10}', state='{11}', city='{12}'",
                    name, computer, type, model, issue, fixzed, solution, address, phone, os, zip, state, city);
                sql = sql + values + " WHERE id='"+id+"'";
                reader = db.query(sql);

                thisUser.newTicket(db.getInsertId("Tickets"));

                MessageBox.Show("Ticket Updated Succesfully!");
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmBrowse f = new frmBrowse(thisUser);
            f.Show();
            this.Close();
        }

        private void frmEdit_Load(object sender, EventArgs e)
        {
            db = new Database();
            sql = "SELECT * FROM Tickets WHERE id='"+id+"'";
            reader = db.query(sql);
            reader.Read();

            txtAddress.Text = reader["address"].ToString();
            txtCity.Text = reader["city"].ToString();

            //Needs to seperate with comma for names
            string first = reader["name"].ToString().Split(new char[] { ',' })[0].Replace("''", "'");
            string last = reader["name"].ToString().Split(new char[] { ',' })[1].Trim().Replace("''", "'");
            txtFirstName.Text = first;
            txtLastName.Text = last;

            txtModel.Text = reader["model"].ToString().Replace("''", "'");
            txtPhone.Text = reader["phone"].ToString().Replace("''", "'");
            txtProblem.Text = reader["issue"].ToString().Replace("''", "'");
            txtSolution.Text = reader["solution"].ToString().Replace("''", "'");
            txtState.Text = reader["state"].ToString().Replace("''", "'");
            txtZip.Text = reader["zip"].ToString().Replace("''", "'");
            txtMaker.Text = reader["computer"].ToString().Replace("''", "'");
            cbFixed.Text = Convert.ToBoolean(reader["fixed"])? "Yes":"No";
            cbOS.Text = reader["os"].ToString().Replace("''", "'");
            cbType.Text = reader["type"].ToString().Replace("''", "'");
            txtDate.Text = DateTime.Parse(reader["date"].ToString().Replace("''", "'")).ToString("d");

            //Info for creator USING like operator
            sql = "SELECT * FROM Users WHERE tickets LIKE '%, " + id + ", %'";
            SqlDataReader crea = db.query(sql);
            crea.Read();
            txtCreator.Text = crea["name"].ToString().Replace("''", "'");
        }
    }
}
