using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using ManageNoticeProperty.Models;
using ManageNoticeProperty.ViewModel;
using ManageNoticeProperty.Models.Repository;
using System.Linq;
using ManageNoticeProperty.Infrastructure;
using System;
using System.Globalization;
using System.Collections.Generic;
using PagedList;

namespace ManageNoticeProperty.Controllers
{
    public class PropertyController : Controller
    {
        private ApplicationUserManager _userManager;
        private IRepository<TypeFlat> _typeFlatRepository;
        private IExtendRepository<Order> _orderRepository;
        private IRepository<Album> _albumRepository;
        private IExtendRepository<Flat> _flatRepository;
        private ILastVisit _lastVisit;
        private IPhotoConvert _photoConvert;
        private GetPropertyOrderViewModel _getPropertyOrder;
        public PropertyController(IRepository<TypeFlat> typeFlatRepository,
            IExtendRepository<Flat> flatRepository, ILastVisit lastVisit, IExtendRepository<Order> orderRepository,
            IRepository<Album> albumRepository, IPhotoConvert photoConvert)
        {
            _typeFlatRepository = typeFlatRepository;
            _flatRepository = flatRepository;
            _lastVisit = lastVisit;
            _orderRepository = orderRepository;
            _albumRepository = albumRepository;
            _photoConvert = photoConvert;
        }
        // GET: Property
        public ActionResult Index(int? page)
        {
            AllPropertyViewModel vM = new AllPropertyViewModel();
            vM.TypeFlat = _typeFlatRepository.GetOverview();

            Func<Flat, bool> predicate = x => x.IsHidden == false;
            var lista = _flatRepository.GetOverview(predicate).ToList();
            int pageSize = 5;
            var pageNumber = page ?? 1;
            var test = lista.ToPagedList(pageNumber, pageSize);
            vM.Flat = test;

            return View(vM);
        }

        [HttpPost]
        public ActionResult Index(AllPropertyViewModel vM)
        {

            return View(vM);
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddProperty()
        {
            FlatViewModel vM = new FlatViewModel();
            vM.TypeFlat = _typeFlatRepository.GetOverview().ToArray();
            vM.NameAction = "AddProperty";
            vM.isAddAction = true;
            return View(vM);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProperty(FlatViewModel flatViewModel)
        {
            if (flatViewModel.PostedFile == null)
            {
                ModelState.AddModelError("FlatViewModel.PostedFile", "Zdjęcie jest wymagane");
            }
            if (!ModelState.IsValid)
            {
                return View("AddProperty", flatViewModel);
            }

            if (flatViewModel.PostedFile == null)
            {

                return View("AddProperty", flatViewModel);
            }

            if (Request.IsAuthenticated)
            {
                flatViewModel.Flat.Album = new List<Album>();
                var album = new Album();
                album.Photo = _photoConvert.PhotoToByte(flatViewModel.PostedFile);
                flatViewModel.Flat.Album.Add(album);
                flatViewModel.Flat.UserId = User.Identity.GetUserId();
                flatViewModel.Flat.AddFlate = System.DateTime.Now;
                _flatRepository.Add(flatViewModel.Flat);
                _flatRepository.Save();

            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult EditProperty(int id)
        {
            FlatViewModel vM = new FlatViewModel();
            vM.Flat = _flatRepository.GetID(id);
            if (vM.Flat.UserId != User.Identity.GetUserId())
            {
                return RedirectToAction("ManageOwnProperty", "Property");
            }
            vM.TypeFlat = _typeFlatRepository.GetOverview();
            vM.isAddAction = false;
            vM.NameAction = "EditProperty";
            return View(vM);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProperty(FlatViewModel flatViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("EditProperty", flatViewModel);
            }

            var flat = _flatRepository.GetID(flatViewModel.Flat.FlatId);
            flat.Area = flatViewModel.Flat.Area;
            flat.City = flatViewModel.Flat.City;
            flat.Condignation = flatViewModel.Flat.Condignation;
            flat.Description = flatViewModel.Flat.Description;
            flat.IsBalcon = flatViewModel.Flat.IsBalcon;
            flat.PostCode = flatViewModel.Flat.PostCode;
            flat.Price = flatViewModel.Flat.Price;
            flat.QuantityRoom = flatViewModel.Flat.QuantityRoom;
            flat.Street = flatViewModel.Flat.Street;
            flat.TypeFlatID = flatViewModel.Flat.TypeFlatID;
            _flatRepository.Update(flat);
            _flatRepository.Save();

            return RedirectToAction("ManageOwnProperty");

        }

        public ActionResult ManageOwnProperty()
        {
            Func<Flat, bool> predicate = x => x.UserId == User.Identity.GetUserId();
            var ownProperty = _flatRepository.GetOverviewAll(predicate).ToList();
            return View(ownProperty);
        }

        public ActionResult GetProperty(int id)
        {
            Flat flat;
            _getPropertyOrder = new GetPropertyOrderViewModel();

            flat = _flatRepository.GetIdAll(id);
            if (isGetProperty(flat))
            {
                return View("NothingProperty");
            }
            _lastVisit.AddLasstViewProperty(id);
            setGetPropertyViewModel(flat);

            return View(_getPropertyOrder);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult GetProperty(int id, GetPropertyOrderViewModel getPropertyOrder)
        {
            if (!ModelState.IsValid)
            {
                return View("GetProperty", new { id = id, getPropertyOrder = getPropertyOrder });
            }

            Order order = new Order();
            order.Description = getPropertyOrder.Order.Description;
            order.BuyUserID = User.Identity.GetUserId();
            order.FlatId = id;
            order.isDeleteBuyer = false;
            order.Price = getPropertyOrder.Flat.Price;
            order.SellDate = DateTime.Now;
            _orderRepository.Add(order);
            _orderRepository.Save();

            Flat flat = _flatRepository.GetID(id);
            // flat.IsHidden = true;
            //flat.SellDate = DateTime.Now;
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

            Func<Order, bool> func = x => x.SellDate >= setStartDateInRaportAdmin(startDate) && x.SellDate <= setEndDateInRaportAdmin(endDate);
            var listSellProperty = _orderRepository.GetOverviewAll(func).ToList();
            var totalPrice = listSellProperty.Sum(x => x.Price);

            adminRaportViewModel.Order = listSellProperty;
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
            var isDate = DateTime.TryParseExact(endDate, "yyyy-MM-dd", new CultureInfo("pl-PL"), DateTimeStyles.None, out value);
            if (endDate == null || isDate == false)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            }
            return value;
        }

        private void setGetPropertyViewModel(Flat flat)
        {
            _getPropertyOrder.IsOwnProperty = flat.UserId == User.Identity.GetUserId() ? true : false;

            _getPropertyOrder.isBuyAfter = flat.Order.Where(
                    x => x.BuyUserID == User.Identity.GetUserId()
                    ).Count() > 0 ? true : false;

            _getPropertyOrder.Flat = flat;
            _getPropertyOrder.Order = new Order();
        }

        private bool isGetProperty(Flat flat)
        {
            return flat == null
               || ((flat.IsHidden && flat.UserId != User.Identity.GetUserId()) //You are seller and Property is Hidden
               && (flat.IsHidden && flat.Order.Count(x => x.BuyUserID == User.Identity.GetUserId()) == 0) // You are buyer on list Order
                );
        }



        #endregion
    }
}