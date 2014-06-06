using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using WalletManager.DataAccess;
using WalletManager.DataAccess.Interface;
using WalletManager.DataAccess.Source;
using WalletManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
namespace WalletManager.Controllers
{
    [Authorize]
    public class MovementOfFundsController : Controller
    {
        IMovementOfFundsRepository _mfRepository;
        IFundsTypeRepository _ftRepository;
        IMovementTypes _mtRepository;

        private ApplicationUserManager _userManager;
       
        public MovementOfFundsController()
        {
            this._mfRepository = new MovementOfFundsRepository(new Context());
            this._ftRepository = new FundsTypeRepository(new Context());
            this._mtRepository = new MovementTypes(new Context());
        }
        public MovementOfFundsController(ApplicationUserManager userManager)
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
        // GET: MovementOfFunds
        public ActionResult Index()
        {
            var movement = from mf in _mfRepository.GetMovementOfFunds() where mf.userId == User.Identity.GetUserId()
                      select mf;
            foreach (var item in movement)
            {
                if (item.sectionId == 1)
                {
                    item.EstimatePrice -= 2 * item.EstimatePrice;
                    item.RealPrice -= 2 * item.RealPrice;
                }
            }
            return View("Index", movement);
        }

        public ActionResult Create()
        {
            MovementOfFundsModel fundModel = new MovementOfFundsModel();
            PopulateDropDown();
            return PartialView(fundModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MovementOfFundsModel fundModel)
        {
           
            //fundModel.
            if (User.Identity.Name != null)
            { 
                fundModel.userId = User.Identity.GetUserId();
            }
            //var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
           // fundModel.userId = User.Identity.GetUserId();
            PopulateDropDown(fundModel.sectionId);
            _mfRepository.InsertMovementOfFunds(fundModel);
            _mfRepository.Save();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            MovementOfFundsModel movementModel = new MovementOfFundsModel();
            movementModel = _mfRepository.GetMovementOfFundsByID(id);
            PopulateDropDown(movementModel.sectionId);
            return PartialView(movementModel);
        }

        [HttpPost]
        public ActionResult Edit(MovementOfFundsModel movementModel)
        {
            PopulateDropDown(movementModel.sectionId);
            if (User.Identity.Name != null)
            {
                movementModel.userId = User.Identity.GetUserId();
            }
            _mfRepository.UpdateMovementOfFunds(movementModel);
            _mfRepository.Save();

            return RedirectToAction("Index");
        }



        public ActionResult PieChart()
        {
            var EstimatePrices = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 1 && d.userId == User.Identity.GetUserId() select d.EstimatePrice);
            var EstimateNames = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 1 && d.userId == User.Identity.GetUserId() select d.name);
            //var RealPrices = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 1 select d.RealPrice);


            var pie = new Chart(width: 520, height: 400, theme: ChartTheme.Vanilla)
                   .AddTitle("Estimate Expenses")
                   .AddSeries(
                   chartType: "pie",
                   legend: "prices",
                   xField: "Estimate Prices",
                   xValue: EstimateNames.ToArray(),
                   yFields: "Names",
                   yValues: EstimatePrices.ToArray())
               .Write();
           
               return null;

        }

        public ActionResult PieChartExpense()
        {
            var RealPrices = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 1 && d.userId == User.Identity.GetUserId() select d.RealPrice);
            var Names = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 1 && d.userId == User.Identity.GetUserId() select d.name);


            var pie = new Chart(width: 520, height: 400, theme: ChartTheme.Vanilla)
                   .AddTitle("Real Expenses")
                   .AddSeries(
                   chartType: "pie",
                   legend: "prices",
                   xField: "Estimate Prices",
                   xValue: Names.ToArray(),
                   yFields: "Names",
                   yValues: RealPrices.ToArray())
               .Write();

            return null;
        }

        public ActionResult PieChartIncome()
        {
            var RealNames = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 2 && d.userId == User.Identity.GetUserId() select d.name);
            var EstimatePrices = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 2 && d.userId == User.Identity.GetUserId() select d.EstimatePrice);

            var pies = new Chart(width: 520, height: 400, theme: ChartTheme.Vanilla)
                   .AddTitle("Estimate Incomes")
                   .AddSeries(
                   chartType: "pie",
                   legend: "prices",
                   xField: "Estimate Prices",
                   xValue: RealNames.ToArray(),
                   yFields: "Names",
                   yValues: EstimatePrices.ToArray())
               .Write();

            return null;
        }

        public ActionResult PieChartIncomeReal()
        {
            var RealNames = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 2 && d.userId == User.Identity.GetUserId() select d.name);
            var RealPrices = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 2 && d.userId == User.Identity.GetUserId() select d.RealPrice);
            
            var pie = new Chart(width: 520, height: 400, theme: ChartTheme.Vanilla)
                   .AddTitle("Real Incomes")
                   .AddSeries(
                   chartType: "pie",
                   legend: "prices",
                   xField: "Real Prices",
                   xValue: RealNames.ToArray(),
                   yFields: "Names",
                   yValues: RealPrices.ToArray())
               .Write();
          
            return null;
        }

        public void PopulateDropDown(object typeSelected = null)
        {
            ViewBag.sectionId = new SelectList(_ftRepository.GetFundsTypes(), "Id", "name");
        }
    }
}