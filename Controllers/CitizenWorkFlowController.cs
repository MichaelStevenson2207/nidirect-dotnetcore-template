using Microsoft.AspNetCore.Mvc;
using nidirect_app_frontend.ViewModels;

namespace nidirect_app_frontend.Controllers
{
    public class CitizenWorkFlowController : Controller
    {
        private const string SectionName = "Workflows";

        [HttpGet]
        public IActionResult Index()
        {
            BaseViewModel model = new BaseViewModel
            {
                SectionName = SectionName,
                TitleTagName = "Workflows"
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Start()
        {
            BaseViewModel model = new BaseViewModel
            {
                SectionName = SectionName,
                TitleTagName = "Apply for your first provisional driving licence"
            };

            return View(model);
        }
    }
}