using Microsoft.AspNetCore.Mvc;
using nidirect_app_frontend.ViewModels;

namespace nidirect_app_frontend.Controllers;

public sealed class CitizenWorkFlowController : Controller
{
    private const string SectionName = "Workflows";

    [HttpGet]
    public IActionResult Index()
    {
        var model = new BaseViewModel
        {
            SectionName = SectionName,
            TitleTagName = "Workflows"
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Start()
    {
        var model = new BaseViewModel
        {
            SectionName = SectionName,
            TitleTagName = "Apply for your first provisional driving licence"
        };

        return View(model);
    }
}