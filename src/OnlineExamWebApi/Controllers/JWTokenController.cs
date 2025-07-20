using Microsoft.AspNetCore.Mvc;

namespace OnlineExamWebApi.Controllers
{
    public class JWTokenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
