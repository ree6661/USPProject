using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;

namespace FindPhone
{
    internal class DBConnection
    {
        string server = "localhost";
        string dataBase = "phonedb";
        string username = "root";
        string password = "root";
        string constring = "";

        public MySqlConnection conn;

        public DBConnection()
        {
            string constring = "SERVER=" + server +
                                ";DATABASE=" + dataBase +
                                ";UID=" + username +
                                ";PASSWORD=" + password;
            conn = new MySqlConnection(constring);
        }
        public void open()
        {
            conn.Open();
        }
        public void close()
        {
            conn.Close();
        }
    }
}