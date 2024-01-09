﻿using Microsoft.AspNetCore.Mvc;

namespace MyWatchShop.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int statusCode)
        {
            return View("NotFound");
        }
    }
}
