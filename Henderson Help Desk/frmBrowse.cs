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
    public partial class frmBrowse : Form
    {
        Database db;
        string sql;
        SqlDataReader reader;
        User thisUser;

        public frmBrowse()
        {
            InitializeComponent();
        }

        public frmBrowse(User user)
        {
            thisUser = user;
            InitializeComponent();
        }

        private void frmBrowse_Load(object sender, EventArgs e)
        {
            db = new Database();
            sql = "SELECT * FROM Tickets";
            reader = db.query(sql);
            string[] test;
            while (reader.Read())
            {
                string fixedIt = "No";
                if (Convert.ToBoolean(reader["fixed"]))
                {
                    fixedIt = "Yes";
                }
                test = new string[] { reader["id"].ToString(), reader["name"].ToString().Replace("''", "'"), reader["phone"].ToString().Replace("''", "'"), reader["address"].ToString().Replace("''", "'"), reader["city"].ToString().Replace("''", "'"), reader["state"].ToString().Replace("''", "'"), reader["zip"].ToString().Replace("''", "'"), reader["computer"].ToString().Replace("''", "'"), reader["type"].ToString().Replace("''", "'"), reader["os"].ToString().Replace("''", "'"), reader["model"].ToString().Replace("''", "'"), reader["issue"].ToString().Replace("''", "'"), fixedIt, reader["solution"].ToString().Replace("''", "'"), DateTime.Parse(reader["date"].ToString().Replace("''", "'")).ToString("d") };
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1, test);
                dataGridView1.Rows.Add(row);
            }
            dataGridView1.AutoResizeRows();
            dataGridView1.AutoResizeColumns();

            SelectedId = dataGridView1.FirstDisplayedCell.Value.ToString();
        }

        private void frmBrowse_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.dispose();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            frmStart f = new frmStart(thisUser);
            f.Show();
            this.Close();
        }

        private void createToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCreate f = new frmCreate(thisUser);
            f.Show();
            this.Close();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreate f = new frmCreate(thisUser);
            f.Show();
            Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            frmCreate f = new frmCreate(thisUser);
            f.Show();
            Close();
        }

        string SelectedId;

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (SelectedId != null)
            {
                frmEdit fEdit = new frmEdit(SelectedId, thisUser);
                fEdit.Show();
                this.Close();
            }
            else
                MessageBox.Show("Please select a ticket!");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = dataGridView1.CurrentCell.OwningRow;
            DataGridViewCell c = r.Cells[0];
            SelectedId = c.Value.ToString();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow r = dataGridView1.CurrentCell.OwningRow;
            DataGridViewCell c = r.Cells[0];
            SelectedId = c.Value.ToString();
            frmEdit f = new frmEdit(SelectedId, thisUser);
            f.Show();
            this.Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedId != null)
            {
                frmEdit fEdit = new frmEdit(SelectedId, thisUser);
                fEdit.Show();
                this.Close();
            }
            else
                MessageBox.Show("Please select a ticket!");
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SelectedId != null)
            {
                frmEdit fEdit = new frmEdit(SelectedId, thisUser);
                fEdit.Show();
                this.Close();
            }
            else
                MessageBox.Show("Please select a ticket!");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                sql = "DELETE FROM Tickets WHERE id='" + SelectedId + "'";
                reader = db.query(sql);
                refill();
            }
        }

        private void refill()
        {
            dataGridView1.Rows.Clear();
            sql = "SELECT * FROM Tickets";
            reader = db.query(sql);
            string[] test;
            while (reader.Read())
            {
                string fixedIt = "No";
                if (Convert.ToBoolean(reader["fixed"]))
                {
                    fixedIt = "Yes";
                }
                test = new string[] { reader["id"].ToString(), reader["name"].ToString().Replace("''", "'"), reader["phone"].ToString().Replace("''", "'"), reader["address"].ToString().Replace("''", "'"), reader["city"].ToString().Replace("''", "'"), reader["state"].ToString().Replace("''", "'"), reader["zip"].ToString().Replace("''", "'"), reader["computer"].ToString().Replace("''", "'"), reader["type"].ToString().Replace("''", "'"), reader["os"].ToString().Replace("''", "'"), reader["model"].ToString().Replace("''", "'"), reader["issue"].ToString().Replace("''", "'"), fixedIt, reader["solution"].ToString().Replace("''", "'"), reader["date"].ToString().Replace("''", "'") };
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1, test);
                dataGridView1.Rows.Add(row);
            }
            dataGridView1.AutoResizeRows();
            dataGridView1.AutoResizeColumns();

            SelectedId = dataGridView1.FirstDisplayedCell.Value.ToString();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                sql = "DELETE FROM Tickets WHERE id='" + SelectedId + "'";
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
                sql = "DELETE FROM Tickets WHERE id='" + SelectedId + "'";
                reader = db.query(sql);
                refill();
            }
        }
    }
}