using MVC_CircloidTemplate.App_Classes;
using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CircloidTemplate.Controllers
{
 
    public class HomeController : Controller
    {
        public HomeController()
        {
            ViewBag.MainPageSelected = "selected";
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.ActiveUserCount = HttpContext.Application["ActiveUserCount"];
            ViewBag.TotalUserCount = HttpContext.Application["TotalUserCount"];

            return View();
        }

        public ActionResult AssignCookie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AssignCookie(string CookieName, string CookieValue)
        {
            HttpCookie hc = new HttpCookie(CookieName);
            hc.Value = CookieValue;
            hc.Expires = DateTime.Now.AddDays(2);
            Response.Cookies.Add(hc);
            return View();
        }

        NorthwindEntities ctx = new NorthwindEntities();
        public ActionResult RetrieveCookie()
        {
            string cookieVal = Request.Cookies["Deneme"].Value.ToString();
            ViewBag.CookieValue = cookieVal;
            return View();
        }

        public ActionResult MyCart()
        {

            Cart crt;
            if (Session["CurrentCart"] != null)
            {
                crt = (Cart)Session["CurrentCart"];

            }
            else
            {
                crt = new Cart();
            }
            Session["CurrentCart"] = crt;
            return View();
        }


        [HttpPost]
        public string RemoveCart(int id)
        {

            string cartMessage = "";

            Cart crt = (Cart)Session["CurrentCart"];

            Product prd = ctx.Products.FirstOrDefault(x => x.ProductID == id);
            crt.PrdList.RemoveAll(x=> x.ProductID == id);
            Session["CurrentCart"] = crt;
            cartMessage = "Ürün sepetten silinmiştir";
            return cartMessage;
        }

        public ActionResult PartialCartListView()
        {
            if (Session["CurrentCart"] != null)
            {
                Cart uc = (Cart)Session["CurrentCart"];
                return PartialView(uc.PrdList);
            }
            return PartialView();
        }


    }
}