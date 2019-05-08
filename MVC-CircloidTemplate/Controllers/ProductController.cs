using MVC_CircloidTemplate.App_Classes;
using MVC_CircloidTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_CircloidTemplate.Controllers
{

    public class ProductController : Controller
    {
        public ProductController()
        {
            ViewBag.ProductSelected = "selected";
        }
        NorthwindEntities ctx = new NorthwindEntities();
        
        // GET: Product
        public ActionResult Index()
        {
            List<Product> prdList = ctx.Products.ToList();
            return View(prdList);
        }

        public ActionResult AddProduct()
        {
            ViewBag.catList = ctx.Categories.ToList();
            ViewBag.supList = ctx.Suppliers.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            ctx.Products.Add(p);
            ctx.SaveChanges();

            return RedirectToAction("Index");
        }

        //Sil işlemini üç farklı yolla yapacağız:

        //İlk çözümümüz sil butonuna basılınca
        //yeni bir view açılacak, yani kullanıcı yeni bir sayfaya yönlendirilecek ve
        // evet derse silinecek

        // İkinci yol: sil butonuna basılınca yukarıda alert kutusu çıkacak ve kayıt silinsin
        // mi diye sorulacak evet denilirse kayıt silinecek. Bu yöntemin dezavantajı,
        //alert kutusu birkaç kez görüntülendikten sonra browser otomatik olarak alert
        // kutusunun altına checkbox'ı ekliyor ve bu mesajı tekrar gösterme seçeneği
        // sunuyor. Eğer kullanıcı checkbox'ı işaretlerse, tekrar alert kutusu 
        // gözükmeyeceği için silme işlemi gerçekleştirilemiyor.(AJAX kodu yazılacak)

        // Üçüncü yol: Sil butonuna basılınca,küçük bir pencere açılacak yani başka bir
        //sayfaya yönlendirilmeyecek, evet seçilirse silme işlemi gerçekleştirilecek.
        //(AJAX kodu yazılacak) 
        public ActionResult DeleteProduct(int id)
        {
            Product prd = ctx.Products.FirstOrDefault(x => x.ProductID == id);
            return View(prd);
        }

        [HttpPost]
        public ActionResult DeleteProduct(Product p)
        {
            Product prod = ctx.Products.FirstOrDefault(x => x.ProductID == p.ProductID);
            ctx.Products.Remove(prod);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult UpdateProduct()
        {
            int productID = Convert.ToInt32(Request.QueryString["prdID"]);
            string productName = Request.QueryString["prdName"].ToString();
            string productFrom = Request.QueryString["prdFrom"].ToString();

            Product prd = ctx.Products.FirstOrDefault(x => x.ProductID == productID);

            ViewBag.catList = ctx.Categories.ToList();
            ViewBag.supList = ctx.Suppliers.ToList();
            ViewBag.productFrom = productFrom;

            return View(prd);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product prd)
        {
            Product prod = ctx.Products.Find(prd.ProductID);
            prod.ProductName = prd.ProductName;
            prod.UnitPrice = prd.UnitPrice;
            prod.UnitsInStock = prd.UnitsInStock;
            prod.CategoryID = prd.CategoryID;
            prod.SupplierID = prd.SupplierID;

            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public string AddCart(int id)
        {
            //sepet varsa, gelen ürünü var olan sepete ekle, sepet yoksa önce aktif session(oturum) için sepet oluştur ve oluşan yeni sepete gelen ürünü ekle
            Cart crt;
            string cartMessage = "";
            if (Session["CurrentCart"] == null)
            {
                crt = new Cart();

            }
            else
            {
                crt = (Cart)Session["CurrentCart"];
                
            }


            foreach (Product p in crt.PrdList)
            {
                if (p.ProductID == id)
                {
                    cartMessage = "Eklemek istediğiniz ürün sepette mevcut";
                    return cartMessage;
                }
            }

            Product prd = ctx.Products.FirstOrDefault(x => x.ProductID == id);
            crt.PrdList.Add(prd);
            Session["CurrentCart"] = crt;
            cartMessage = "Ürün sepete eklenmiştir";
            return cartMessage;
        }

        public ActionResult PartialProductCountNav()
        {
            Cart c = (Cart)Session["CurrentCart"];
            int n = c.PrdList.Count();
            return PartialView(c.PrdList);
            //return PartialView(Session["CurrentCart"] as Cart);
        }
    }
}