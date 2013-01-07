using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Henderson_Help_Desk.Code;
using Henderson_Help_Desk.Properties;

namespace Henderson_Help_Desk
{
    public class User
    {

        Database db;
        string sql;
        SqlDataReader reader;

        public string level { get; set; }
        public string ip { get; set; }
        public string name { get; set; }
        public DateTime lastLogin { get; set; }
        public string tickets { get; set; }
        public string username {get; set;}
        public string id {get; set;}

        public User()
        {
            db = new Database();            
        }

        public bool login(string user, string pass, string ip)
        {
                sql = "SELECT * FROM Users WHERE ";
                sql = sql + string.Format("username='{0}' AND password='{1}'", user, pass);
                reader = db.query(sql);
                if (reader.HasRows)
                {
                    string newSql = "UPDATE Users SET ip='" + ip + "' , lastLogin='" + DateTime.Now + "' WHERE username='" + user + "' AND password='" + pass + "'";
                    db.query(newSql);

                    sql = "SELECT * FROM Users WHERE ";
                    sql = sql + string.Format("username='{0}' AND password='{1}'", user, pass);
                    reader = db.query(sql);
                    reader.Read();
                    level = reader["level"].ToString();
                    ip = reader["ip"].ToString();
                    lastLogin = Convert.ToDateTime(reader["lastLogin"]);
                    name = reader["name"].ToString();
                    tickets = reader["tickets"].ToString();
                    username = reader["username"].ToString();
                    id = reader["id"].ToString();

                    return true;
                }
                else
                    return false;
        }

        public void newTicket(string id)
        {
            tickets = tickets + id + ", ";
            sql = "UPDATE Users SET tickets='"+tickets+"' WHERE username='"+username+"'";
            reader = db.query(sql);
        }
    }
}
