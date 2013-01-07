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
    public partial class frmUsers : Form
    {

        Database db;
        string sql;
        SqlDataReader reader;
        User thisUser;
        string SelectedId;

        public frmUsers()
        {
            InitializeComponent();
        }

        public frmUsers(User user)
        {
            thisUser = user;
            InitializeComponent();
        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            db = new Database();
            sql = "SELECT * FROM Users";
            reader = db.query(sql);
            string[] test;
            while (reader.Read())
            {
                string tickets;
                if(reader["tickets"].ToString().Length > 2)
                {
                    tickets = reader["tickets"].ToString().Replace("''", "'").Substring(2, reader["tickets"].ToString().Replace("''", "'").Length-2);
                } else
                    tickets = "";
                test = new string[] { reader["id"].ToString(), 
                    reader["name"].ToString().Replace("''", "'"), 
                    reader["username"].ToString().Replace("''", "'"), 
                    tickets, 
                    reader["level"].ToString().Replace("''", "'"), 
                    DateTime.Parse(reader["lastLogin"].ToString().Replace("''", "'")).ToString("d"), 
                    reader["ip"].ToString().Replace("''", "'") };
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1, test);
                dataGridView1.Rows.Add(row);
            }
            
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoResizeRows();

            SelectedId = dataGridView1.FirstDisplayedCell.Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmEditUser f = new frmEditUser(thisUser, SelectedId);
            f.Show();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                sql = "DELETE FROM Users WHERE id='" + SelectedId + "'";
                reader = db.query(sql);
                refill();
            }
        }

        private void refill()
        {
            dataGridView1.Rows.Clear();
            sql = "SELECT * FROM Users";
            reader = db.query(sql);
            string[] test;
            while (reader.Read())
            {
                string tickets;
                if (reader["tickets"].ToString().Length > 2)
                {
                    tickets = reader["tickets"].ToString().Replace("''", "'").Substring(2, reader["tickets"].ToString().Replace("''", "'").Length - 2);
                }
                else
                    tickets = "";
                test = new string[] { reader["id"].ToString(), 
                    reader["name"].ToString().Replace("''", "'"), 
                    reader["username"].ToString().Replace("''", "'"), 
                    tickets, 
                    reader["level"].ToString().Replace("''", "'"), 
                    DateTime.Parse(reader["lastLogin"].ToString().Replace("''", "'")).ToString("d"), 
                    reader["ip"].ToString().Replace("''", "'") };
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1, test);
                dataGridView1.Rows.Add(row);
            }

            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoResizeRows();

            SelectedId = dataGridView1.FirstDisplayedCell.Value.ToString();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                sql = "DELETE FROM Users WHERE id='" + SelectedId + "'";
                reader = db.query(sql);
                refill();
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                sql = "DELETE FROM Users WHERE id='" + SelectedId + "'";
                reader = db.query(sql);
                refill();
            }
        }
    }
}
