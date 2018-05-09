using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuMaMa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 判断结果
        /// </summary>
        /// <param name="total">总分</param>
        /// <param name="id">1，用户选择了正 2，用户选择了负</param>
        /// <param name="clickCount">点击次数</param>
        /// <param name="number">概率</param>
        /// <returns></returns>
        public JsonResult Result(int total, int id,int clickCount, double number = 36.7)
        {
            
            clickCount += 1;
            Random rd = new Random();
            int rdNumber = rd.Next(0, 99);
            if (rdNumber <= number && id == 1)
            {

                total += 3;
                return Json(new { success = true, msg = "同正加3分", resultStr = "正", total, clickCount });
            }
            else if (rdNumber > number && id == 2)
            {
                total += 1;
                return Json(new { success = true, msg = "同负加1分", resultStr = "负", total, clickCount });
            }
            else if (rdNumber < number && id == 2)
            {
                total -= 2;
                return Json(new { success = true, msg = "正负相反减2分", resultStr = "正", total, clickCount });
            }
            total -= 2;
            return Json(new { success = true, msg = "正负相反减2分", resultStr = "负", total, clickCount });
        }
    }
}