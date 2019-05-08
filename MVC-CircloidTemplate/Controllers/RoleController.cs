using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_CircloidTemplate.Controllers
{
   
    public class RoleController : Controller
    {

        public RoleController()
        {
            ViewBag.RoleSelected = "selected";
        }
        // GET: Role
        public ActionResult Index()
        {
            List<string> rolesList = Roles.GetAllRoles().ToList();
            return View(rolesList);
        }
  

        public ActionResult AddRole(string message = null)
        {
            ViewBag.Message = message;
            return View();
        }

        //List<string> roleList;
        //[HttpPost]
        //public ActionResult AddRole(string roleName)
        //{
        //    try
        //    {
        //        roleList = Roles.GetAllRoles().ToList();
        //        if (roleList.Contains(roleName))
        //        {
        //            ViewBag.Var = roleName + " listede var";
        //            return View();
        //        }
        //        else if (roleName == null || roleName.Equals(""))
        //        {
        //            ViewBag.Var = "Boş veya null değer girmeyiniz.";
        //            return View();
        //        }
        //        else
        //        {
        //            Roles.CreateRole(roleName);
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //        return View();

        //    }

        //}

        [HttpPost]
        [ActionName("AddRole")]
        public ActionResult AddRolePost(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return RedirectToAction("AddRole", new { message = "Rol boş olamaz" });

            if (Roles.RoleExists(roleName))

                return RedirectToAction("AddRole", new { message = "Rol zaten kayıtlı" });

            Roles.CreateRole(roleName);
            return RedirectToAction("AddRole", new { message = "Başarılı" });
            
        }


        [HttpPost]
        public ActionResult DeleteRole(string roleName)
        {
            Roles.DeleteRole(roleName);
            return RedirectToAction("Index");
        }
    }
}