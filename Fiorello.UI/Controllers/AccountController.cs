﻿using Microsoft.AspNetCore.Mvc;

namespace Fiorello.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
