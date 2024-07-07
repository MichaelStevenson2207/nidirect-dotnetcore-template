using Microsoft.AspNetCore.Mvc;
using nidirect_app_frontend.ViewModels;

namespace nidirect_app_frontend.Controllers;

public class CheckAnswersController : Controller
{
    private const string SectionName = "Check answers";

    [HttpGet]
    public IActionResult Index()
    {
        BaseViewModel model = new BaseViewModel
        {
            SectionName = SectionName,
            TitleTagName = "Check answers"
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(BaseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        return RedirectToAction("Index", "Task");
    }
}