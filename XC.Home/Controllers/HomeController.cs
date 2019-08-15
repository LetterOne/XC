using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XC.Home.Common;
using XC.Home.Models;

namespace XC.Home.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewData["AboutCompany"] =Const.AboutCompany;
            ViewData["AboutProduct"] = Const.AboutProduct;
            ViewData["AboutCustomer"] = Const.AboutCustomer;
            return View();
        }

        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>
        public IActionResult About()
        {
            ViewData["AboutCompany"] = Const.AboutCompany;
            ViewData["AboutProduct"] = Const.AboutProduct;
            ViewData["AboutCustomer"] = Const.AboutCustomer;
            return View();
        }

        /// <summary>
        /// 产品中心
        /// </summary>
        /// <returns></returns>
        public IActionResult ProductCenter()
        {
            return View();
        }

        /// <summary>
        /// 施工方式
        /// </summary>
        /// <returns></returns>
        public IActionResult CosMethod()
        {
            return View();
        }

        /// <summary>
        /// 行业新闻
        /// </summary>
        /// <returns></returns>
        public IActionResult News()
        {
            return View();
        }

        /// <summary>
        /// 经纬达新闻
        /// </summary>
        /// <returns></returns>
        public IActionResult CompanyNews()
        {
            return View();
        }
        /// <summary>
        /// 联系我们
        /// </summary>
        /// <returns></returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult MessageBox()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
