using MySqlConnector;
using System.Data;

namespace Monitoring
{
    public class MySQL
    {
        MySqlConnectionStringBuilder builder;
        //private string serverName = "127.0.0.1"; // Адрес сервера (для локальной базы пишите "localhost")
        //private string userName = "root"; // Имя пользователя
        //private string dbName = "ithelper"; //Имя базы данных
        //private string password = ""; // Пароль для подключения
        //private uint port = 3306; // Порт для подключения

        private string serverName = "127.0.0.1"; // Адрес сервера (для локальной базы пишите "localhost")
        private string userName = "root"; // Имя пользователя
        private string dbName = "ithelper"; //Имя базы данных
        private string password = ""; // Пароль для подключения
        private uint port = 3306; // Порт для подключения
        public MySQL()
        {
            builder = new MySqlConnectionStringBuilder()
            {
                UserID = userName,
                Password = password,
                Server = serverName,
                Database = dbName,
                Port = port
            };
        }
        public DataTable GetDataTableSQL(string sql)
        {
            Console.WriteLine(sql);
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(builder.ConnectionString))
            {
                conn.Open();
                MySqlCommand sqlCom = new MySqlCommand()
                {
                    Connection = conn,
                    CommandText = sql
                };
                sqlCom.ExecuteNonQuery();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCom);
                dataAdapter.Fill(dt);
            }
            return dt;
        }
        public void SendSQL(string sql)
        {
            Console.WriteLine(sql);
            using (MySqlConnection conn = new MySqlConnection(builder.ConnectionString))
            {
                conn.Open();
                MySqlCommand sqlCom = new MySqlCommand()
                {
                    Connection = conn,
                    CommandText = sql
                };
                sqlCom.ExecuteNonQuery();
            }
        }

        //private string serverName = "astf3-stp5"; // Адрес сервера (для локальной базы пишите "localhost")
        //private string userName = "root"; // Имя пользователя
        //private string dbName = "zabbix"; //Имя базы данных
        //private uint port = 3307; // Порт для подключения
        //private string password = "Fralkon"; // Пароль для подключения
    }
}
