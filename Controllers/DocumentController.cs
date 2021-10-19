﻿using Microsoft.AspNetCore.Mvc;
using nidirect_app_frontend.ViewModels.Document;

namespace nidirect_app_frontend.Controllers
{
    public class DocumentController : Controller
    {
        private const string SectionName = "Documents";
        [HttpGet]
        public IActionResult Index()
        {
            DocumentViewModel model = new DocumentViewModel
            {
                SectionName = SectionName,
                TitleTagName = "Upload documents"
            };
            return View(model);
        }
    }
}