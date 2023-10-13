using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo_Domain.Entities;

namespace ToDoH2.Pages
{
    public class EditTodoModel : PageModel
    {
        [BindProperty]
        public int Todoid { get; set; }

        [BindProperty]
        public string Todotitle { get; set; }

        [BindProperty]
        public string Tododesc { get; set; }

        [BindProperty]
        public string Prio { get; set; }

        public void OnGet(ToDo todo)
        {
            Todoid = todo.todoId;
            Todotitle = todo.ToDoTitle;
            Tododesc = todo.ToDoDescription;
            Prio = todo.Prio;
            
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return null;
        }
    }
}
