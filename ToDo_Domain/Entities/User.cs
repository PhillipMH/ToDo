namespace ToDo_Domain.Entities
{
    public class User
    {
        public int userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public User(int Userid, string Username, string Password)
        {
            userid = Userid;
            username = Username;
            password = Password;
        }

    }
}
