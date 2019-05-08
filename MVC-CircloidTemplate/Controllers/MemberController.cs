using MVC_CircloidTemplate.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVC_CircloidTemplate.Controllers
{

    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult MemberLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult MemberLogin(UserClass uc, string RememberMe)
        {
            ViewBag.Name = uc.UserName;
            bool validationResult = Membership.ValidateUser(uc.UserName, uc.Password);
            if (validationResult == true)
            {
                /*
                 * Girilen kullanıcı ve şifre bilgileri doğru ise, kullanıcının web sitemize giriş yapabilmesi gerekir. Bunun için öncelikle Web.Config dosyasında authorizaition ayarlarının yapılması gerekir. Ayarlar yapıldıktan sonra FormsAuthentication.RedirectFromLoginPage() methodu çağırılacaktır.
                */
                if (RememberMe == "on")
                {
                    FormsAuthentication.RedirectFromLoginPage(uc.UserName, true);
                }
                else
                {
                    FormsAuthentication.RedirectFromLoginPage(uc.UserName, false);
                }
                //return RedirectToAction("Index", "Home");

            }
            else
            {
                ViewBag.Message = "Kullanıcı adı ya da parola hatalı!";
            }

            return View();
        
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            //Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("MemberLogin", "Member");
        }

        public ActionResult ForgotMyPassword()
        {
            return View();
        }

        public ActionResult CreateNewAccount()
        {
            return View();
        }

        ////[HttpPost]
        ////public ActionResult CreateNewAccount(UserClass uc)
        ////{
        ////    if (uc.Password == uc.ConfirmPassword)
        ////    {
        ////        Membership.CreateUser(uc.UserName, uc.Password, uc.Email, uc.PasswordQuestion, uc.PasswordAnswer, true, out MembershipCreateStatus status);

        ////        string createMessage = "";
        ////        switch (status)
        ////        {
        ////            case MembershipCreateStatus.Success:
        ////                break;
        ////            case MembershipCreateStatus.InvalidUserName:
        ////                createMessage = "Geçersiz kullanıcı adı";
        ////                break;
        ////            case MembershipCreateStatus.InvalidPassword:
        ////                createMessage = "Geçersiz şifre";
        ////                break;
        ////            case MembershipCreateStatus.InvalidQuestion:
        ////                createMessage = "Geçersiz gizli soru";
        ////                break;
        ////            case MembershipCreateStatus.InvalidAnswer:
        ////                createMessage = "Geçersiz gizli cevap";
        ////                break;
        ////            case MembershipCreateStatus.InvalidEmail:
        ////                createMessage = "Geçersiz email adresi";
        ////                break;
        ////            case MembershipCreateStatus.DuplicateUserName:
        ////                createMessage = "Kullanılmış kullanıcı adı";
        ////                break;
        ////            case MembershipCreateStatus.DuplicateEmail:
        ////                createMessage = "Kullanılmış email adresi";
        ////                break;
        ////            case MembershipCreateStatus.UserRejected:
        ////                createMessage = "Kullanıcı engellendi";
        ////                break;
        ////            case MembershipCreateStatus.InvalidProviderUserKey:
        ////                createMessage = "Geçersiz kullanıcı anahtar";
        ////                break;
        ////            case MembershipCreateStatus.DuplicateProviderUserKey:
        ////                createMessage = "Tekrarlanmış kullanıcı anahtarı";
        ////                break;
        ////            case MembershipCreateStatus.ProviderError:
        ////                createMessage = "Sağlayıcı hatası";
        ////                break;

        ////            default:
        ////                break;
        ////        }


        ////        ViewBag.CreateMessage = createMessage;

        ////        if (createMessage == "")
        ////        {
        ////            return RedirectToAction("MemberLogin");
        ////        }
        ////        else
        ////        {
        ////            return View();
        ////        }
        ////    }
        ////    else
        ////    {
        ////        ViewBag.CreateMessage = "Şifre eşleşmesi sağlanamadı";
        ////    }
        ////    return View();
        ////}



        [HttpPost]
        public ActionResult CreateNewAccount(UserClass uc, string ConfirmPassword)
        {
            if (uc.Password == ConfirmPassword)
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
                    return RedirectToAction("MemberLogin");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ViewBag.CreateMessage = "Şifre eşleşmesi sağlanamadı";
            }
            return View();
        }



    }
}