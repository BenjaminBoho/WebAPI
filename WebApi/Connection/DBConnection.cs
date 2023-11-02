using System;
using MySqlConnector;

namespace WebApi
{
	public class DBConnection
	{
        public static MySqlConnection CreateConnection()
        {
            var ConnectionString = "Server=localhost;User ID=user123;Password=qwerqwer;Database=todo_app";
            return new MySqlConnection(ConnectionString);
        }
    }
}

