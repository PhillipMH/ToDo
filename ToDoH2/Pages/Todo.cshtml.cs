using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
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
        [BindProperty]
        public int todoid { get; set; }
        [BindProperty]
        public string todotitle { get; set; }
        [BindProperty]
        public string tododesc { get; set; }
        [BindProperty]
        public string todoprio { get; set; }

        public int userid { get; set; }
        public string name { get; set; }
        [BindProperty]
        public List<ToDo> toDos { get; set; }

        public void OnGet(string username) {
		    username = HttpContext.Session.GetString("username");
			User found = _connection.GetUserByUsername(username);
            userid = found.userid;
            name = found.username;
		    toDos = _connection.GetTodosFromDb(userid);
            
        }
        public IActionResult OnPostAddToDo(string username)
        {
            username = HttpContext.Session.GetString("username");
            User found = _connection.GetUserByUsername(username);
            userid = found.userid;
            bool cock;
            cock = _connection.AddToDoItemToUser(userid, todotitle, tododesc, todoprio);
            return RedirectToPage("/Todo");
        }
        public IActionResult OnPostMarkComplete(int todoid)
        {
            _connection.MarkTodoCompleted(todoid);
            return RedirectToPage("/Todo");
        }
    }
}
