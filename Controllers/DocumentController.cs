using Microsoft.AspNetCore.Mvc;
using nidirect_app_frontend.ViewModels.Document;

namespace nidirect_app_frontend.Controllers;

public sealed class DocumentController : Controller
{
    private const string SectionName = "Documents";

    [HttpGet]
    public IActionResult Index()
    {
        var model = new DocumentViewModel
        {
            SectionName = SectionName,
            TitleTagName = "Upload documents"
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(DocumentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        return RedirectToAction("Index", "CheckAnswers");
    }
}