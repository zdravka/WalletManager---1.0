using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WalletManager.DataAccess;
using WalletManager.DataAccess.Interface;
using WalletManager.DataAccess.Source;
using WalletManager.Models;

namespace WalletManager.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult index(LoginModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                //if (DAL.UserIsValid(model.Username, model.Password))
                //{
                //    FormsAuthentication.SetAuthCookie(model.Username, false);
                //    return RedirectToAction("index", "home");
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Invalid Username or Password");
                //}
                if (MembershipService.ValidateUser(model.Username, model.Password))
                {
                    SetupFormsAuthTicket(model.Username, false);
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "The user name or password provided is incorrect.");

            }
            return View(model);
        }

        private User SetupFormsAuthTicket(string userName, bool persistanceFlag)
        {
            User user;
            using (UsersContext usersContext = new UsersContext())
            {
                user = usersContext.GetUser(userName);
            }
            var userId = user.id;
            var userData = userId.ToString(CultureInfo.InvariantCulture);
            var authTicket = new FormsAuthenticationTicket(1, //version
                                userName, // user name
                                DateTime.Now,             //creation
                                DateTime.Now.AddMinutes(30), //Expiration
                                persistanceFlag, //Persistent
                                userData);

            var encTicket = FormsAuthentication.Encrypt(authTicket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
            return user;
        }
    }
}