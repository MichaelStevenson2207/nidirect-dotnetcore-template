using Microsoft.AspNetCore.Mvc;

namespace nidirect_app_frontend.Controllers
{
    public class CheckAnswersController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}