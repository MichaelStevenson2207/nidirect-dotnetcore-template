using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using nidirect_app_frontend.Models;
using nidirect_app_frontend.ViewModels;

namespace nidirect_app_frontend.Controllers;

public class HomeController : Controller
{
    private const string SectionName = "Home";

    [HttpGet]
    public IActionResult Index()
    {
        BaseViewModel model = new BaseViewModel
        {
            SectionName = SectionName,
            TitleTagName = "Index"
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Accessibility()
    {
        BaseViewModel model = new BaseViewModel
        {
            SectionName = SectionName,
            TitleTagName = "Accessibility"
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Cookies()
    {
        BaseViewModel model = new BaseViewModel
        {
            SectionName = SectionName,
            TitleTagName = "Cookies"
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Privacy()
    {
        BaseViewModel model = new BaseViewModel
        {
            SectionName = SectionName,
            TitleTagName = "Privacy"
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, TitleTagName = "Something went wrong", SectionName = "Error" });
    }
}