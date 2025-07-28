using Microsoft.AspNetCore.Mvc;

namespace OnlineExamWeb.Controllers
{
    public class CardOperations : Controller
    {
        public IActionResult CardInfo()
        {
            return View();
        }
    }
}
