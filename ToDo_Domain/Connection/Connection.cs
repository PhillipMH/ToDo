using System.Data;
using System.Data.SqlClient;
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
    }
}