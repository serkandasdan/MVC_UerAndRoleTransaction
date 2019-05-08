using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CircloidTemplate.Controllers
{
   
    public class UpdateCategoryController : Controller
    {
        NorthwindEntities ctx = new NorthwindEntities();
        public UpdateCategoryController()
        {
            ViewBag.CategorySelected = "selected";
        }
        // GET: UpdateCategory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateCategory(int id)
        {
            Category prd = ctx.Categories.FirstOrDefault(x => x.CategoryID == id);
            return View(prd);
        }


        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes(image.ContentLength);
            byte[] bytes = new byte[imageBytes.Length + 78];
            Array.Copy(imageBytes, 0, bytes, 78, imageBytes.Length);
            return bytes;
        }

        [HttpPost]
        public ActionResult UpdateCategory([Bind(Include ="CategoryID,CategoryName,Description")]
        Category ktg, HttpPostedFileBase Picture)
        {
            if (ModelState.IsValid)
            {
                Category k = ctx.Categories.Find(ktg.CategoryID);

                k.CategoryName = ktg.CategoryName;
                k.Description = ktg.Description;

                if (Picture != null)
                {
                    k.Picture = ConvertToBytes(Picture);
                }

                ctx.SaveChanges();
            }
            

            return RedirectToAction("Index", "Category");
        }
    }
}