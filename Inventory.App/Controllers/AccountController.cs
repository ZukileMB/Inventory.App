using Inventory.App.Helpers;
using Inventory.App.Models;
using Inventory.App.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Inventory.App.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginView, string ReturnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //code here
                    var DBcontext = new DatabaseContext();
                    var user = DBcontext.User.Where(x => x.Username.Equals(loginView.Email, System.StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (user != null)
                    {
                        bool isValid = Security.PasswordEncryption.CompareHashValue(loginView.Password, loginView.Email, user.HASH, user.SALT);
                        if (isValid)
                        {
                            FormsAuthentication.SetAuthCookie(loginView.Email, false);
                            SessionContext.UserId = user.UserId;
                            return RedirectToLocal(ReturnUrl);
                        }
                    }
                }
            }
            catch
            {
                ModelState.AddModelError("", "Error occured while tring to login.");
                return View();

            }
            ModelState.AddModelError("", "Invalid Username or Password.");
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //code here
                    var salt = Security.PasswordEncryption.Get_SALT();
                    var hash = Security.PasswordEncryption.Get_HASH_SHA512(registerViewModel.Password, registerViewModel.Email, salt);
                    var DBcontext = new DatabaseContext();
                    var user = new User
                    {
                        FirstName = registerViewModel.FirstName,
                        LastName = registerViewModel.LastName,
                        Username = registerViewModel.Email,
                        HASH = hash,
                        SALT = salt

                    };
                    DBcontext.User.Add(user);
                    int userid = DBcontext.SaveChanges();
                    FormsAuthentication.SetAuthCookie(registerViewModel.Email, false);
                    SessionContext.UserId = userid;
                    return RedirectToAction("index", "Home");
                }
            }
            catch
            {
                ModelState.AddModelError("", "Error occured while trying to save user details.");
            }

            return View();
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}