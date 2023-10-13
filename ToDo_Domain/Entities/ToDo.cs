using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ToDo_Domain.Entities
{
    public class ToDo
    {
        [BindProperty]
        public int todoId { get; set; }
        [BindProperty]
        public DateTime DateCreated { get; set; }
        [BindProperty]
        public string ToDoTitle { get; set; }
        [BindProperty]
        public string ToDoDescription {get; set;}
        [BindProperty]
        public bool iscompleted { get; set; } = false;
        [BindProperty]
        public string Prio {get; set;}
    }
}
