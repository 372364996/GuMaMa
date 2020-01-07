using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
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
        public JsonResult Result(int total, int id, int clickCount, double number = 36.7)
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

        public ActionResult GeneratePic()
        {
           


            for (int i = 0; i < 10; i++)
            {
                var imgPoster = Bitmap.FromFile(HttpContext.Server.MapPath("~/Content/images/yuebao.jpg"));
                Graphics g = Graphics.FromImage(imgPoster);
                var name = "用户名:进击的";
                var date = $"{DateTime.Now.Year}年{DateTime.Now.Month}月收支统计";
                var balance = $"账户余额：{i}元";
                var income = $"本月收入：{i}元";
                var incomeMoney = 123;
                var tip = incomeMoney > 0 ? "再接再厉！    再创佳绩！" : "您可以尝试使用打折功能哦！";
                Font nameFont = new Font("微软雅黑", 80, FontStyle.Bold);
                Font datetFont = new Font("微软雅黑", 80, FontStyle.Bold);
                Font balanceFont = new Font("微软雅黑", 85, FontStyle.Bold);
                Font incomeFont = new Font("微软雅黑", 85, FontStyle.Bold);
                Font tipFont = new Font("微软雅黑", 85, FontStyle.Bold);

                SizeF nameSize = g.MeasureString(name, nameFont);
                SizeF dateSize = g.MeasureString(date, datetFont);
                SizeF incomeSize = g.MeasureString(balance.ToString(), incomeFont);
                SizeF balanceSize = g.MeasureString(balance.ToString(), balanceFont);
                SizeF tipSize = g.MeasureString(tip.ToString(), tipFont);
                SolidBrush nameBrush = new SolidBrush(Color.FromArgb(182, 29, 28));
                SolidBrush incomeBrush = new SolidBrush(Color.FromArgb(133, 196, 71));
                SolidBrush balanceBrush = new SolidBrush(Color.FromArgb(239, 105, 4));
                g.DrawString(name, nameFont, nameBrush, new PointF((imgPoster.Width - nameSize.Width) / 2, 210));
                g.DrawString(date, datetFont, nameBrush, new PointF((imgPoster.Width - dateSize.Width) / 2, 990));
                g.DrawString(income, incomeFont, incomeBrush, new PointF((imgPoster.Width - incomeSize.Width) / 2, 1500));
                g.DrawString(balance, balanceFont, balanceBrush, new PointF((imgPoster.Width - balanceSize.Width) / 2, 1800));
                g.DrawString(tip, tipFont, Brushes.White, new PointF((imgPoster.Width - tipSize.Width) / 2, 2090));
                if (!Directory.Exists(Server.MapPath("~/yuebao")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/yuebao"));
                }
                imgPoster.Save(Server.MapPath($"~/yuebao/{i}.jpg"));
                g = Graphics.FromImage(Bitmap.FromFile(HttpContext.Server.MapPath("~/Content/images/yuebao.jpg")));


                g.Dispose();
            }
            return Content("OK");
        }
    }
}