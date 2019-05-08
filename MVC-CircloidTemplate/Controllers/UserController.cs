using MVC_CircloidTemplate.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_CircloidTemplate.Controllers
{
  
    public class UserController : Controller
    {


        public UserController()
        {
            ViewBag.UserSelected = "selected";
        }
        // GET: User
        public ActionResult Index()
        {
            // Veritabanındaki tüm kullanıcıları çekip users isimli Collection'da toplayacak.
            MembershipUserCollection users = Membership.GetAllUsers();
            return View(users);
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserClass uc)
        {
            Membership.CreateUser(uc.UserName, uc.Password, uc.Email, uc.PasswordQuestion, uc.PasswordAnswer, true, out MembershipCreateStatus status);
            string createMessage = "";
            switch (status)
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    createMessage = "Geçersiz kullanıcı adı";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    createMessage = "Geçersiz şifre";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    createMessage = "Geçersiz gizli soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    createMessage = "Geçersiz gizli cevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    createMessage = "Geçersiz email adresi";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    createMessage = "Kullanılmış kullanıcı adı";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    createMessage = "Kullanılmış email adresi";
                    break;
                case MembershipCreateStatus.UserRejected:
                    createMessage = "Kullanıcı engellendi";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    createMessage = "Geçersiz kullanıcı anahtar";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    createMessage = "Tekrarlanmış kullanıcı anahtarı";
                    break;
                case MembershipCreateStatus.ProviderError:
                    createMessage = "Sağlayıcı hatası";
                    break;
                default:
                    break;
            }

            ViewBag.CreateMessage = createMessage;

            if (createMessage == "")
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult AssignRole(string username, string message = null)
        {
            /*
             * Parametre olarak id yazmak zorundayız, sebebi projenin App_start klasörünün altında Route_Config.cs dosyasında "{controller}/{action}/{id}" bu parametre adının da id olması gerekiyor.
             * Kullanıcı RolAta'ya tıkladığında kullanıcı adını parametre olarak buraya alıyoruz. Buradan da kullanıcının adını View'e gönderiyoruz. Amacımız parametre bilgisini View'e taşımak. View tarafında ekle butonuna basında tekrar kullanıcı adını ve rol adını View'dan alıp post tarafına taşımak.
             */
            if (string.IsNullOrWhiteSpace(username))
                return RedirectToAction("Index");

            MembershipUser user = Membership.GetUser(username);

            if (user == null)
                return HttpNotFound();

            string[] userRoles = Roles.GetRolesForUser(username);
            string[] allRoles = Roles.GetAllRoles();

            List<string> availabelRoles = new List<string>();
            foreach (string role in allRoles)
            {
                if (!userRoles.Contains(role))
                    availabelRoles.Add(role);
            }

            ViewBag.AvailableRoles = availabelRoles;
            ViewBag.UserRoles = userRoles;
            ViewBag.Username = username;
            ViewBag.Message = message;

            return View();
        }
        [HttpPost]
        public ActionResult AssignRole(string username, List<string> addedRoles)
        {
            if(addedRoles == null)
                return RedirectToAction("AssignRole", new { username = username, message = "Lütfen rol ekleyiniz" });
            if (addedRoles.Count<1)
            {
                return RedirectToAction("AssignRole", new { username = username, message = "Hata" });
            }

            Roles.AddUserToRoles(username, addedRoles.ToArray());

            return RedirectToAction("AssignRole", new { username = username, message = "Başarılı" });
        }

        [HttpPost]
        public string DeleteRole(string username, string removedRoles)
        {
            string[] removedRolesArray = removedRoles.Split(',');

            if (removedRolesArray.Length < 1 || string.IsNullOrWhiteSpace(removedRolesArray[0]))
            {
                return "Hata";             
            }
            Roles.RemoveUserFromRoles(username, removedRolesArray);
            return "Başarılı";
        }


        public string UserRoles(string username)
        {
            List<string> roles = Roles.GetRolesForUser(username).ToList();
            string role = "";
            foreach (string r in roles)
            {
                role += r + "-";
            }

            if (roles.Count>0)
            {
                role = role.Remove(role.Length - 1, 1);
                return role;
            }     
            else
            {
                return "Rol bulunmuyor";
            }
            
        }

        public ActionResult UserRole(string username)
        {
            List<string> roles = Roles.GetRolesForUser(username).ToList();
            ViewBag.Role = roles;
            return View(roles);
        }
        [HttpPost]
        public ActionResult DeleteUser(string UserName)
        {
            Membership.DeleteUser(UserName);
            return RedirectToAction("Index");
        }
    }
}