using System;
using System.Collections;
using System.Collections.Generic;
using MySqlConnector;

namespace WebApi
{
	public class ListService
	{
        public static List<ListItem> Get()
        {
            var list = new List<ListItem>();
            var sql = "SELECT * FROM list";

            using (var connection = DBConnection.CreateConnection())
            {
                connection.Open();

                using (var command = new MySqlCommand(sql, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new ListItem { listId = reader["listId"] as string, name = reader["list_name"] as string });
                    }
                }
            }
            return list;
        }

        public static void Get(List<ListItem> list, string listId)
        {
            var sql = @"SELECT * FROM list WHERE listId = @id";

            using (var connection = DBConnection.CreateConnection())
            {
                connection.Open();

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", listId);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(new ListItem { listId = reader["listId"] as string , name = reader["list_name"] as string });
                    }
                }
            }
        }

        public static void Insert(ListItem listItems)
        {
            //var sql1 = $"INSERT INTO List (list_id, list_name) VALUES ('{todoItem.Id}', '{todoItem.Name}')";
            var sql = @"INSERT INTO List (listId, list_name)
                        VALUES (@id, @name)";
            //var sql = @"INSERT INTO List (list_name) VALUES (@name)";

            using (var connection = DBConnection.CreateConnection())
            {
                connection.Open();

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", listItems.listId);
                    command.Parameters.AddWithValue("@name", listItems.name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateList(ListItem listItem)
        {
            var sql = @"UPDATE list
                        SET list_name = @name WHERE listId = @id";

            using (var connection = DBConnection.CreateConnection())
            {
                connection.Open();

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", listItem.name);
                    command.Parameters.AddWithValue("@id", listItem.listId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteListRow(string listId)
        {
            var sql = @"DELETE FROM list WHERE listId = @id";

            using (var connection = DBConnection.CreateConnection())
            {
                connection.Open();

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", listId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}

