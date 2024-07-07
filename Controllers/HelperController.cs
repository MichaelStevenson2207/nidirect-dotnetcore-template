using Microsoft.AspNetCore.Mvc;
using nidirect_app_frontend.ViewModels;

namespace nidirect_app_frontend.Controllers;

public class HelperController : Controller
{
    public RedirectToActionResult TimeoutResult()
    {
        return RedirectToAction("Timeout", "Helper");
    }

    [HttpGet]
    public IActionResult Timeout()
    {
        BaseViewModel model = new BaseViewModel
        {
            SectionName = "Time out",
            TitleTagName = "Timed out"
        };

        return View(model);
    }
}