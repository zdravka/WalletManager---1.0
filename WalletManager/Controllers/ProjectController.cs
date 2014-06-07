using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WalletManager.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Uni
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}