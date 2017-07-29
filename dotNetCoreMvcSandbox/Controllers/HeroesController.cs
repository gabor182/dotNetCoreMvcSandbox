using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dotNetCoreMvcSandbox.Controllers
{
    public class HeroesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}