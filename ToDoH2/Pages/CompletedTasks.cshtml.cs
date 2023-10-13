using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo_Domain.Connection;
using ToDo_Domain.Entities;

namespace ToDoH2.Pages
{
    public class CompletedTasksModel : PageModel
    {
        private readonly Connection _connection;
        public CompletedTasksModel(Connection connection)
        {
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
        public List<ToDo> toDosCompleted { get; set; }

        public void OnGet(string username)
        {
            username = HttpContext.Session.GetString("username");
            User found = _connection.GetUserByUsername(username);
            userid = found.userid;
            name = found.username;
            toDosCompleted = _connection.GetCompletedTodosFromDb(userid);

        }
        public IActionResult OnPostMarkUncomplete(int todoid)
        {
            _connection.MarkTodoUncompleted(todoid);
            return RedirectToPage("/CompletedTasks");
        }
    }
}

