﻿using Microsoft.AspNetCore.Mvc;

namespace CustomConfig.Controllers
{
    public class VotesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
