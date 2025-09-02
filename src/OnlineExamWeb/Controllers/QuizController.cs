using Microsoft.AspNetCore.Mvc;

namespace OnlineExamWeb.Controllers
{
    public class QuizController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
