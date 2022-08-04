using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Middleware.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            int k = 5;
            double a = k / 0;
            return View();
        }
    }
}
