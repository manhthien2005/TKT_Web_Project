﻿using Microsoft.AspNetCore.Mvc;

namespace Tiki_Web_Project.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
