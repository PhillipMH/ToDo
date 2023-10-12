using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ToDo_Domain.Entities;
using static System.Reflection.Metadata.BlobBuilder;

namespace ToDo_Domain.Connection
{
    public class Connection
    {
        private readonly string _connectionString;
        private readonly SqlConnection _sqlConnection;

        public Connection(string connection)
        {
            _connectionString = connection;
            _sqlConnection = new SqlConnection(_connectionString);
        }
        private SqlCommand MySqlCommand(string StoredProcedure)
        {
            SqlCommand myCommand = new(StoredProcedure)
            {
                CommandType = CommandType.StoredProcedure,
                Connection = _sqlConnection
            };
            return myCommand;
        }

        public User LoginforUsers(string username, string password, int userid)
        {
            SqlCommand command = MySqlCommand("spLoginUser");
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@pass", password);
            command.Parameters.AddWithValue("@userid", userid);
            try
            {
                _sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    if (username == reader.GetString("username") && password == reader.GetString("pass"))
                    {
                        User userinfo = new User(
                        reader.GetInt32("userid"),
                        reader.GetString("username"),
                        reader.GetString("pass"));
                        return userinfo;
                    }
                    if (username != reader.GetString("username") || password != reader.GetString("pass"))
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }

            finally { _sqlConnection.Close(); }
            return null;
        }
        public bool AddUser(string username, string password)
        {
            bool usercreated = false;
            SqlCommand _command = MySqlCommand("SPAddLoaner");
            _command.Parameters.AddWithValue("@username", username);
            _command.Parameters.AddWithValue("@Password", password);
            try
            {
                _sqlConnection.Open();
                _command.ExecuteNonQuery();
                usercreated = true;
            }
            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }
            finally { _sqlConnection.Close(); }
            return usercreated;
        }

        public List<string> CheckForUsedUsernames(string username)
        {
            List<string> usernames = new List<string>();
            SqlCommand command = MySqlCommand("SPSearchForUsedUsernames");
            try
            {
                _sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    usernames.Add(reader.GetString(0));
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }
            finally { _sqlConnection.Close(); }
            return usernames;

        }
        public List<ToDo> GetTodosFromDb(int userid)
        {
            List<ToDo> todos = new List<ToDo>();
            SqlCommand command = MySqlCommand("spGetAllUserTodos");
            command.Parameters.AddWithValue("@userid", userid);
            try
            {
                _sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ToDo todo = new();
                    todo.todoId = reader.GetInt32("todoid");
                    todo.ToDoTitle = reader.GetString("TaskTitle");
                    todo.ToDoDescription = reader.GetString("TaskDesc");
                    todo.DateCreated = reader.GetDateTime("DateCreated");
                    todos.Add(todo);
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }
            finally { _sqlConnection.Close(); }
            return todos;

        }
        public User GetUserByUsername(string username)
        {
            SqlCommand command = MySqlCommand("spGetAllUserInfo");
            command.Parameters.AddWithValue("@username", username);

            try
            {
                _sqlConnection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User founduser = new User(reader.GetInt32("userid"), reader.GetString("username"), reader.GetString("pass"));
                    return founduser;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally { _sqlConnection.Close(); }
            return null;

        }
    }
}