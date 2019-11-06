using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace XC.Home.Controllers
{
    public class FileDownLoadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}