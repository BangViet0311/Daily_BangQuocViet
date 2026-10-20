using Microsoft.AspNetCore.Mvc;

namespace Day2.Controllers
{
    public class HelloController : Controller
    {
        public string Greeting()
        {
            return "chao mung cac ban den voi mon hoc ASP.NET Core MVC";
        }
        public string Welcome(int id, string name)
        {
            return $"Xin chao {id} - {name}, chuc ban hoc tot mon hoc ASP.NET!";
        }
        public IActionResult Welcome1(int id1, string name)
        {
            ViewBag.Id = id1;
            ViewData["Name"] = name;
            return View();
        }  
    }
}
