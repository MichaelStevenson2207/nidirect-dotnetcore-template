using Microsoft.AspNetCore.Mvc;
using nidirect_app_frontend.ViewModels;

namespace nidirect_app_frontend.Controllers;

public class ComponentController : Controller
{
    private const string SectionName = "Component";

    [HttpGet]
    public IActionResult Index()
    {
        BaseViewModel model = new BaseViewModel
        {
            SectionName = SectionName,
            TitleTagName = "Components"
        };

        return View(model);
    }
}