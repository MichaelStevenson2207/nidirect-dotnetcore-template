using Microsoft.AspNetCore.Mvc;
using nidirect_app_frontend.ViewModels;

namespace nidirect_app_frontend.Controllers;

public class TaskController : Controller
{
    private const string SectionName = "Tasks";

    [HttpGet]
    public IActionResult Index()
    {
        BaseViewModel model = new BaseViewModel
        {
            SectionName = SectionName,
            TitleTagName = "Task list"
        };

        return View(model);
    }
}