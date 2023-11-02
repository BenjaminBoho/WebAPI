using System.Collections.Generic;
using MySqlConnector;

namespace WebApi
{
	public class TaskService
	{
        public static List<TaskItem> Get(string listId)
        {
            var list = new List<TaskItem>();
            var sql = "SELECT * FROM Task WHERE listId = @listId";

            using (var connection = DBConnection.CreateConnection())
            {
                connection.Open();

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@listId", listId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new TaskItem
                        {
                            taskId = reader["taskId"] as string,
                            tasks = reader["task_name"] as string,
                            completed = Convert.ToBoolean(reader["completed"]),
                            listId = reader["listId"] as string
                        });
                    }
                }
            }
            return list;
        }


        public static void InsertTask(TaskItem task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            var sql = @"INSERT INTO Task (taskId, task_name, listId) VALUES (@id, @name, @listId)";

            using (var connection = DBConnection.CreateConnection())
            {
                connection.Open();

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", task.taskId);
                    command.Parameters.AddWithValue("@name", task.tasks);
                    command.Parameters.AddWithValue("@listId", task.listId);
                    command.ExecuteNonQuery();
                }
            }
        }


        public static void UpdateTask(TaskItem taskItem)
        {
            var sql = @"UPDATE Task SET task_name = @name WHERE taskId = @id AND listId = @listId";

            using (var connection = DBConnection.CreateConnection())

            {
                connection.Open();

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", taskItem.tasks);
                    command.Parameters.AddWithValue("@id", taskItem.taskId);
                    command.Parameters.AddWithValue("@listId", taskItem.listId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteTaskRow(string taskId)
        {
            var sql = @"DELETE FROM Task WHERE taskId = @id";

            using (var connection = DBConnection.CreateConnection())
            {
                connection.Open();

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", taskId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteTasksByListId(string listId)
        {
            var sql = @"DELETE FROM Task WHERE listId = @listId";

            using (var connection = DBConnection.CreateConnection())
            {
                connection.Open();

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@listId", listId);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}

