using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ToDo_Domain.Entities;

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

            public User LoginforUsers(string username, string password)
            {
                SqlCommand command = MySqlCommand("spLoginUser");
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@pass", password);
                try
                {
                    _sqlConnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        if (username == reader.GetString("username") && password == reader.GetString("Pass"))
                        {
                            User userinfo = new User(
                            reader.GetInt32("Userid"),
                            reader.GetString("username"),
                            reader.GetString("Pass"));
                            return userinfo;
                        }
                        if (username != reader.GetString("username") || password != reader.GetString("Pass"))
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
        }
    }