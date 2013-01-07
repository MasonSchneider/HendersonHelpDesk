using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Henderson_Help_Desk.Properties;

namespace Henderson_Help_Desk.Code
{
    public class Database
    {
        SqlConnection con;
        SqlCommand db;
        SqlDataReader reader;

        public Database()
        {
            con = new SqlConnection();
            con.ConnectionString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=\""+Properties.Settings.Default.DBpath+"\";Integrated Security=True;Connect Timeout=30;User Instance=True";
            con.Open();
            db = con.CreateCommand();
            
        }

        public string getInsertId(string table)
        {
            string sql = "SELECT * FROM " +table+ " ORDER BY id DESC";
            SqlDataReader temp;
            temp = query(sql);
            temp.Read();
            return temp["id"].ToString();
        }

        public SqlDataReader query(string sql)
        {
            if(reader != null)
                reader.Close();
            db.CommandText = sql;
			reader = db.ExecuteReader();
                //while (reader.Read())
                //{
                //Console.WriteLine("\t{0}\t{1}", reader["CustomerID"], reader["CompanyName"]);
                //    }
				
            return reader;
        }

        public void dispose()
        {
			con.Close();
        }
    }
}
