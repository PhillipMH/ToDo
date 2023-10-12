using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo_Domain.Connection;
using ToDo_Domain.Entities;
using ToDoH2.SessionHelper;

namespace ToDoH2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Connection _connection;
        public IndexModel(Connection connection)
        {
            _connection = connection;
        }

        string successmessage = "";
        string errormessage = "";
        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string password { get; set; }
        public IActionResult OnPost()
        {
            Console.WriteLine(username);

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                errormessage = "Something went wrong";
                return Page();
            }
            User founduser;
            founduser = _connection.LoginforUsers(username, password);
            if (founduser == null)
            {
                return Page();
            }
            else if (founduser.username == username && founduser.password == password)
            {
                HttpContext.Session.SetSessionString(username, "username");
                return RedirectToPage("/Todo");
            }
            return Page();
        }

    }
}