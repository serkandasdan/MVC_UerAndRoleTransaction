using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CircloidTemplate.Controllers
{
   
    public class SupplierController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();
        // GET: Supplier

        public SupplierController()
        {
            ViewBag.SupplierSelected = "selected";
        }
        public ActionResult Index()
        {
            List<Supplier> supList = ctx.Suppliers.ToList();
            return View(supList);
        }

        public ActionResult AddSupplier()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSupplier(Supplier s)
        {
            ctx.Suppliers.Add(s);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }


        //public ActionResult DeleteSupplier(int id)
        //{
        //    Supplier sup = ctx.Suppliers.FirstOrDefault(x => x.SupplierID == id);
        //    return View(sup);
        //}

        //[HttpPost]
        //public ActionResult DeleteSupplier(Supplier s)
        //{
        //    Supplier sup = ctx.Suppliers.FirstOrDefault(x => x.SupplierID == s.SupplierID);
        //    ctx.Suppliers.Remove(sup);
        //    ctx.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        /*Bu metodun içinde oluşan hata AJAX'ı etilemez. Ajax için success Ajax'ın doğru bir
      şekilde action'a ulaşmış olmasıyla ilgilidir. Bu metotta veritabanındaki ilişkilerden dolayı
      kayıt silinemez ve benzeri hatalar Ajax'ı ilgilendirmez. Bu üzden bu metot içinde
      oluşan hatalarla ilgili ajax tarafına bilgi göndermeliyiz.
    */
    [HttpPost]
    public string DeleteSupplier(int id)
        {
            try
            {
                Supplier s = ctx.Suppliers.Find(id);
                ctx.Suppliers.Remove(s);
                ctx.SaveChanges();

                return "OK";
            }
            catch (Exception)
            {

                return "ERROR";
            }
        }
    }
}