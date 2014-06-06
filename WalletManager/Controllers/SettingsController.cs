using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WalletManager.DataAccess;
using WalletManager.DataAccess.Interface;
using WalletManager.DataAccess.Source;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using System.Threading.Tasks;
using WalletManager.Models;
namespace WalletManager.Controllers
{
    public class SettingsController : Controller
    {
        
        IMovementTypes _mtRepository;
        IFundsTypeRepository _ftRepository;
        private ApplicationUserManager _userManager;
        public SettingsController() 
        {
            this._mtRepository = new MovementTypes(new Context());
            this._ftRepository = new FundsTypeRepository(new Context());
        }
        public SettingsController(ApplicationUserManager userManager)
        {
             UserManager = userManager;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Settings
        [Authorize]
        public ActionResult Index()
        {
            var movement = from mf in _mtRepository.GetMovementTypes()
                           select mf;

            return View("Index", movement);
        }
        [HttpGet]
        public ActionResult Create()
        {
            MovementTypesModel typeModel = new MovementTypesModel();
            PopulateDropDown();
            return PartialView(typeModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MovementTypesModel typeModel)
        {

            //fundModel.
            if (User.Identity.Name != null)
            {
                typeModel.userId = User.Identity.GetUserId();
            }
            //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            // fundModel.userId = User.Identity.GetUserId();
            PopulateDropDown(typeModel.sectionId);
            _mtRepository.InsertMovementTypes(typeModel);
            _mtRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            MovementTypesModel movementModel = new MovementTypesModel();
            movementModel = _mtRepository.GetMovementOfFundsByID(id);
            PopulateDropDown(movementModel.sectionId);
            return PartialView(movementModel);
        }

        [HttpPost]
        public ActionResult Edit(MovementTypesModel movementModel)
        {
            PopulateDropDown(movementModel.sectionId);
            if (User.Identity.Name != null)
            {
                movementModel.userId = User.Identity.GetUserId();
            }
            _mtRepository.UpdateMovementTypes(movementModel);
            _mtRepository.Save();

            return RedirectToAction("Index");
        }

        public ActionResult Sections()
        {
            var allsections = from ft in _ftRepository.GetFundsTypes()
                              select ft;

            return PartialView(allsections);
        }

        public void PopulateDropDown(object typeSelected = null)
        {
            ViewBag.sectionId = new SelectList(_ftRepository.GetFundsTypes(), "Id", "name");
        }
    }
}