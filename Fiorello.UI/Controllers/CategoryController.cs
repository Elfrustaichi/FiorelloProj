﻿using Microsoft.AspNetCore.Mvc;

namespace Fiorello.UI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}