using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CircloidTemplate.Controllers
{

    public class USupplierController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();

        public USupplierController()
        {
            ViewBag.SupplierSelected = "selected";
        }
        // GET: UpdateCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateSupplier(int id)
        {
            Supplier sup = ctx.Suppliers.FirstOrDefault(x => x.SupplierID == id);
            return View(sup);
        }

        [HttpPost]
        public ActionResult UpdateSupplier(Supplier s)
        {
            Supplier sup = ctx.Suppliers.FirstOrDefault(x => x.SupplierID == s.SupplierID);


            ctx.Suppliers.Remove(sup);
            ctx.Suppliers.Add(s);

            ctx.SaveChanges();

            return RedirectToAction("Index", "Supplier");
        }


        //Diğer bir güncelleme yöntemi
        //public ActionResult UpdateSupplier([Bind(Include ="SupplierID,CompanyName,ContactName," +
        //    "ContactTitle,Adress,City,Region,PostalCode,Country,Phone")] Supplier supplier)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ctx.Entry(supplier).State = System.Data.Entity.EntityState.Modified;
        //        ctx.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return RedirectToAction("UpdateSupplier", new { id = supplier.SupplierID });
        //}
    }
}