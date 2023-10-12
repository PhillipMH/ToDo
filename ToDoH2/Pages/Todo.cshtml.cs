using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using ToDo_Domain.Connection;
using ToDo_Domain.Entities;

namespace ToDoH2.Pages
{
    public class TodoModel : PageModel
    {
        private readonly Connection _connection;
        public TodoModel(Connection connection) {
            _connection = connection;
        }
        public int userid { get; set; }
        public string name { get; set; }
        public List<ToDo> toDos { get; set; }

        public void OnGet(string username) {
		    username = HttpContext.Session.GetString("username");
			User found = _connection.GetUserByUsername(username);
            userid = found.userid;
            name = found.username;
			toDos = _connection.GetTodosFromDb(userid);
        }
    }
}
