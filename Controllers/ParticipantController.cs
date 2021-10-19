using Microsoft.AspNetCore.Mvc;
using nidirect_app_frontend.ViewModels.Participant;

namespace nidirect_app_frontend.Controllers
{
    public class ParticipantController : Controller
    {
        private const string SectionName = "Participant";

        [HttpGet]
        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel
            {
                SectionName = SectionName,
                TitleTagName = "What is your full name?"
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(IndexViewModel model)
        {
            model.SectionName = SectionName;
            model.TitleTagName = "What is your full name?";

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Document");
        }
    }
}