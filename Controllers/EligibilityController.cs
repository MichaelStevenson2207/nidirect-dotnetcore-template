using Microsoft.AspNetCore.Mvc;
using nidirect_app_frontend.ViewModels;
using nidirect_app_frontend.ViewModels.Eligibility;
using nidirect_app_frontend.ViewModels.NewFolder;

namespace nidirect_app_frontend.Controllers
{
    public class EligibilityController : Controller
    {
        private const string SectionName = "Eligibility";

        [HttpGet]
        public IActionResult Eligibility()
        {
            EligibilityViewModel model = new EligibilityViewModel
            {
                SectionName = SectionName,
                TitleTagName = "Are you currently resident in Northern Ireland?"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eligibility(EligibilityViewModel model)
        {
            model.SectionName = SectionName;
            model.TitleTagName = "Are you currently resident in Northern Ireland?";

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return model.IsResidentNi.HasValue && model.IsResidentNi.Value ? RedirectToAction("EyeSight", "Eligibility") :
            RedirectToAction("End", "Eligibility");
        }

        [HttpGet]
        public IActionResult EyeSight()
        {
            EyeSightViewModel model = new EyeSightViewModel
            {
                SectionName = SectionName,
                TitleTagName = "Can you meet the legal eyesight standard for driving?"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EyeSight(EyeSightViewModel model)
        {
            model.SectionName = SectionName;
            model.TitleTagName = "Can you meet the legal eyesight standard for driving?";

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return model.IsLegalEyeSight.HasValue && model.IsLegalEyeSight.Value ? RedirectToAction("Index", "Participant") :
                RedirectToAction("End", "Eligibility");
        }

        [HttpGet]
        public IActionResult End()
        {
            BaseViewModel model = new BaseViewModel
            {
                SectionName = SectionName,
                TitleTagName = "Sorry, you are unable to continue"
            };

            return View(model);
        }
    }
}