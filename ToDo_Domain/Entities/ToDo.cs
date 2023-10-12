using Microsoft.Identity.Client;

namespace ToDo_Domain.Entities
{
    public class ToDo
    {
        public int todoId { get; set; }
        public DateTime DateCreated { get; set; }
        public string ToDoTitle { get; set; }
        public string ToDoDescription {get; set;}
        public int userid { get; set; }

        public ToDo()
        {

        }
    }
}
