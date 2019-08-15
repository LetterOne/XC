using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XC.Web.Models;

namespace XC.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = @"上海狂鲨达建材有限公司是一家专业从事生产美缝剂，真瓷胶，晶砖瓷的企业。本公司秉持绿色环保为发展理念的一家企业，本着健康至上、惠及千家万户的使命，坚持采用进口原材料。先进的工艺钵造本款离端关建剂，郑重承诺不含有X任基酣、烷基酣、甲醛、甲苯等有害物质。公司的产品经过香港STC，SGS防水防霉检测，各种化学物质对人体无害，可以放心使用。公司的经纬达系列产品已经在市场上销售多年，产品销往多个省市，在市场上有一定的市场占有率。";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
