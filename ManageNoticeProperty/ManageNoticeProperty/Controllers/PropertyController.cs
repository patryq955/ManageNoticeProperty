using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using ManageNoticeProperty.Models;
using ManageNoticeProperty.ViewModel;
using ManageNoticeProperty.Models.Repository;
using System.Collections.Generic;
using System.Linq;
using ManageNoticeProperty.Infrastructure;
using System;
using System.Globalization;

namespace ManageNoticeProperty.Controllers
{
    public class PropertyController : Controller
    {
        private ApplicationUserManager _userManager;
        private IRepository<TypeFlat> _typeFlatRepository;
        private IRepository<Order> _orderRepository;
        private IFlatRepository _flatRepository;
        private ILastVisit _lastVisit;
        public PropertyController(IRepository<TypeFlat> typeFlatRepository,
            IFlatRepository flatRepository, ILastVisit lastVisit, IRepository<Order> orderRepository)
        {
            _typeFlatRepository = typeFlatRepository;
            _flatRepository = flatRepository;
            _lastVisit = lastVisit;
            _orderRepository = orderRepository;
        }
        // GET: Property
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult AddProperty()
        {
            FlatViewModel vM = new FlatViewModel();
            vM.TypeFlat = _typeFlatRepository.GetOverview().ToArray();

            return View(vM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddProperty(FlatViewModel flatViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("AddProperty", flatViewModel);
            }

            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                flatViewModel.Flat.UserId = user.Id;
                flatViewModel.Flat.AddFlate = System.DateTime.Now;
                _flatRepository.Add(flatViewModel.Flat);
                _flatRepository.Save();

            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetProperty(int id)
        {
            Flat flat;
            GetPropertyOrderViewModel getPropertyOrder = new GetPropertyOrderViewModel();
            
            flat = _flatRepository.GetID(id);
            if (flat == null || (flat.IsHidden && flat.UserId != User.Identity.GetUserId()))
            {
                return View("NothingProperty");
            }
            getPropertyOrder.IsOwnProperty = flat.UserId == User.Identity.GetUserId() ? true : false;
            _lastVisit.AddLasstViewProperty(id);
            getPropertyOrder.Flat = flat;
            getPropertyOrder.Order = new Order();
            return View(getPropertyOrder);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult GetProperty(int id, GetPropertyOrderViewModel getPropertyOrder)
        {
            if(!ModelState.IsValid)
            {
                return View("GetProperty",new {id=id,getPropertyOrder=getPropertyOrder });
            }

            Order order = new Order();
            order.Description = getPropertyOrder.Order.Description;
            order.BuyUserID = User.Identity.GetUserId();
            order.FlatId = id;
            _orderRepository.Add(order);
            _orderRepository.Save();

            Flat flat = _flatRepository.GetID(id);
            flat.IsHidden = true;
            flat.SellDate = DateTime.Now;
            _flatRepository.Update(flat);
            _flatRepository.Save();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult NothingProperty()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult RaportAdmin(string startDate = null, string endDate = null)
        {
            AdminRaportViewModel adminRaportViewModel = new AdminRaportViewModel();

            Func<Flat, bool> func = x => x.IsHidden == true && x.SellDate >= setStartDateInRaportAdmin(startDate) && x.SellDate <= setEndDateInRaportAdmin(endDate);
            var listSellProperty = _flatRepository.GetOverviewAll(func).ToList();
            var totalPrice = listSellProperty.Sum(x => x.Price);

            adminRaportViewModel.Flats = listSellProperty;
            adminRaportViewModel.TotalPrice = totalPrice;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_RaportAdmin", adminRaportViewModel);
            }

            return View(adminRaportViewModel);
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


        #region private method
        private DateTime setStartDateInRaportAdmin(string startDate)
        {
            DateTime value;
            var isDate = DateTime.TryParseExact(startDate, "yyyy-MM-dd", new CultureInfo("pl-PL"), DateTimeStyles.None, out value);
            if (startDate == null || isDate == false)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            return value;
        }

        private DateTime setEndDateInRaportAdmin(string endDate)
        {
            DateTime value;
            var isDate  = DateTime.TryParseExact(endDate, "yyyy-MM-dd", new CultureInfo("pl-PL"), DateTimeStyles.None, out value);
            if (endDate == null || isDate == false)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            }
            return value;
        }



        #endregion
    }
}