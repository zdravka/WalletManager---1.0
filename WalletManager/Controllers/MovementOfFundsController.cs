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
using RazorPDF;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MvcRazorToPdf;
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
            //ViewData["SearchString"] = SearchString;
            var movement = from mf in _mfRepository.GetMovementOfFunds()
                           where mf.userId == User.Identity.GetUserId()
                           select mf;

            foreach (var item in movement)
            {
                if (item.sectionId == 1)
                {
                    item.EstimatePrice -= 2 * item.EstimatePrice;
                    item.RealPrice -= 2 * item.RealPrice;
                }
            }
            //if (string.IsNullOrWhiteSpace(SearchString))
            //{
            //    movement = pos.ToPagedList(currentPageIndex, defaultPageSize);
            //}
            //else
            //{
            //    movement = pos.Where(p => p.CurrentDate.ToShortDateString().Contains(date.ToShortDateString())).ToPagedList(currentPageIndex, defaultPageSize);

            //}

           
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
            //MovementTypesModel movementType = new MovementTypesModel();
           // movementType = _mtRepository.GetMovementOfFundsByID((int)fundModel.movementTypeId);
           // fundModel.sectionId = fundModel.movementType.sectionId;

            PopulateDropDown(fundModel.sectionId, fundModel.movementTypeId);
            _mfRepository.InsertMovementOfFunds(fundModel);
            _mfRepository.Save();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Report()
        {
            var movement = from mf in _mfRepository.GetMovementOfFunds()
                           where mf.userId == User.Identity.GetUserId()
                           select mf;


            MovementOfFundsModel model = new MovementOfFundsModel();
            model = _mfRepository.GetMovementOfFundsByID(1);
            var move = new MovementOfFundsModel 
            {
                EstimateDate = model.EstimateDate,
                EstimatePrice = model.EstimatePrice,
                RealDate = model.RealDate,
                RealPrice = model.RealPrice,
                sectionId = model.sectionId,
                movementTypeId = model.movementTypeId
            };
            //var pdfresult = new PdfResult(movement,"Report");

            //var model = new MovementOfFundsModel
            //{
            //    EstimateDate = 1,
            //    Items = new List<BasketItem>(){
            //        new BasketItem {
            //         Id = 1,
            //         Description = "Item 1",
            //         Price = 1.99m},
            //        new BasketItem {
            //            Id = 2,
            //            Description = "Item 2",
            //            Price = 2.99m }
            //    }
            //};

            return new PdfActionResult(move);
        }

        public ActionResult PDF(int id)
        {
            //var user = new User();
            //user.Username = "private";
            //user.Password = "most private";
            //var pie = new Report
            var movement = from mf in _mfRepository.GetMovementOfFunds()
                           where mf.userId == User.Identity.GetUserId()
                           select mf;
            //var pdfResult = new PdfResult(null);
            //foreach (var item in movement)
            //{
            //    pdfResult = new PdfResult(item, "PDF");
            //}
            
            //pdfResult.ViewBag.Title = "Title from pdf";
            //return pdfResult;

            // Setup sample model
            var list = movement;
            //for (int i = 1; i < 10; i++)
                //list.Add(new Person() { UserName = "Person " + i, LuckyNumber = i });

            // Output to Pdf?
            //if (Request.QueryString["format"] == "pdf")
            //    return new PdfResult(list, "PDF");

            //return View(list);
           // MemoryStream m = new MemoryStream();
           // Document document = new Document();
           // PdfWriter.GetInstance(document, m);
           // document.Open();
           // document.Add(new Paragraph("Hello WORLD"));
           // document.Add(new Paragraph(DateTime.Now.ToString()));
           //// document.Close();
           // byte[] byteInfo = m.ToArray();
           // MemoryStream output = new MemoryStream();
           // output.Write(byteInfo, 0, byteInfo.Length);
           // output.Position = 0;
           // HttpContext.Response.AddHeader("content-disposition", "attachment; filename=form.pdf");
           // //m.Write(byteInfo, 0, byteInfo.Length);
           // //m.Position = 0;
           // //m.Flush();
            
           // return new FileStreamResult(output, "application/pdf");
            MovementOfFundsModel model = new MovementOfFundsModel();
            model = _mfRepository.GetMovementOfFundsByID(id);
            MovementTypesModel modelTypes = new MovementTypesModel();
            modelTypes = _mtRepository.GetMovementOfFundsByID(model.movementTypeId);

            var move = new MovementOfFundsModel
            {
                EstimateDate = model.EstimateDate,
                EstimatePrice = model.EstimatePrice,
                RealDate = model.RealDate,
                RealPrice = model.RealPrice,
                fundType = modelTypes.movementType,
                movementType = modelTypes
            };
            //var model = new MovementOfFundsModel
            //{
                
            //    RealPrice = 1,
            //    sectionId = 1
            //    //movementType = {
            //    //    //new MovementTypesModel {
            //    //     sectionId = 1,
            //    //     Description = "Item 1",
            //       //  directory ="test"}
            //        //new MovementTypesModel {
            //        //     sectionId = 1,
            //        // Description = "Item 1",
            //        // directory ="test"}
            //    //}
            //};
            return new PdfActionResult(move);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            MovementOfFundsModel movementModel = new MovementOfFundsModel();
            movementModel = _mfRepository.GetMovementOfFundsByID(id);
            PopulateDropDown(movementModel.sectionId, movementModel.movementTypeId);
            return PartialView(movementModel);
        }

        [HttpPost]
        public ActionResult Edit(MovementOfFundsModel movementModel)
        {
            PopulateDropDown(movementModel.sectionId, movementModel.movementTypeId);
            if (User.Identity.Name != null)
            {
                movementModel.userId = User.Identity.GetUserId();
            }
            _mfRepository.UpdateMovementOfFunds(movementModel);
            _mfRepository.Save();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _mfRepository.DeleteMovementOfFunds(id);
            _mfRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult PieChart()
        {
            var EstimatePrices = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 1 && d.userId == User.Identity.GetUserId() select d.EstimatePrice);
            var EstimateNames = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 1 && d.userId == User.Identity.GetUserId() select d.movementType.directory);
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
            var Names = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 1 && d.userId == User.Identity.GetUserId() select d.movementType.directory);


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
            var RealNames = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 2 && d.userId == User.Identity.GetUserId() select d.movementType.directory);
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
            var RealNames = (from d in _mfRepository.GetMovementOfFunds() where d.sectionId == 2 && d.userId == User.Identity.GetUserId() select d.movementType.directory);
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

        public void PopulateDropDown(object typeSelected = null, object movementSelected = null)
        {
            ViewBag.sectionId = new SelectList(_ftRepository.GetFundsTypes(), "Id", "name");
            ViewBag.movementTypeId = new SelectList(_mtRepository.GetMovementTypes(), "id", "directory");
        }
    }
}