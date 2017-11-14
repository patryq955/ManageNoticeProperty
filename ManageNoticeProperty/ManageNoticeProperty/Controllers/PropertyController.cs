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
        private readonly int _itemOnPage = 2;
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
        public ActionResult Index(int? page, AllPropertyViewModel vM, string testString)
        {
            vM.TypeFlat = _typeFlatRepository.GetOverview();
                       
            ViewBag.Test = vM;
            vM.Flat = _flatRepository.GetOverview(SearchProperty(vM)).ToPagedList(pageNumber: page ?? 1, pageSize: _itemOnPage);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ListAllProperty", vM);
            }
            return View(vM);
        }

        public ActionResult IndexNextPage(int? page, AllPropertyViewModel vM, string testString)
        {

            return PartialView("_ListAllProperty", vM);

        }

        //[HttpPost]
        //public ActionResult Index(AllPropertyViewModel vM,int? page)
        //{
        //    vM.TypeFlat = _typeFlatRepository.GetOverview();
        //    Func<Flat, bool> predicate = x => x.IsHidden == false && x.City == vM.City;

        //    vM.Flat = _flatRepository.GetOverview(predicate).ToPagedList(pageNumber: page ?? 1, pageSize: 2);

        //    if (Request.IsAjaxRequest())
        //    {
        //        return PartialView("_ListAllProperty", vM);
        //    }

        //    return View(vM);
        //  }

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
            if (IsGetProperty(flat))
            {
                return View("NothingProperty");
            }
            _lastVisit.AddLasstViewProperty(id);
            SetGetPropertyViewModel(flat);

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

            Func<Order, bool> func = x => x.SellDate >= SetStartDateInRaportAdmin(startDate) && x.SellDate <= SetEndDateInRaportAdmin(endDate);
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
        private DateTime SetStartDateInRaportAdmin(string startDate)
        {
            DateTime value;
            var isDate = DateTime.TryParseExact(startDate, "yyyy-MM-dd", new CultureInfo("pl-PL"), DateTimeStyles.None, out value);
            if (startDate == null || isDate == false)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            return value;
        }

        private DateTime SetEndDateInRaportAdmin(string endDate)
        {
            DateTime value;
            var isDate = DateTime.TryParseExact(endDate, "yyyy-MM-dd", new CultureInfo("pl-PL"), DateTimeStyles.None, out value);
            if (endDate == null || isDate == false)
            {
                return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
            }
            return value;
        }

        private void SetGetPropertyViewModel(Flat flat)
        {
            _getPropertyOrder.IsOwnProperty = flat.UserId == User.Identity.GetUserId() ? true : false;

            _getPropertyOrder.isBuyAfter = flat.Order.Where(
                    x => x.BuyUserID == User.Identity.GetUserId()
                    ).Count() > 0 ? true : false;

            _getPropertyOrder.Flat = flat;
            _getPropertyOrder.Order = new Order();
        }

        private bool IsGetProperty(Flat flat)
        {
            return flat == null
               || ((flat.IsHidden && flat.UserId != User.Identity.GetUserId()) //You are seller and Property is Hidden
               && (flat.IsHidden && flat.Order.Count(x => x.BuyUserID == User.Identity.GetUserId()) == 0) // You are buyer on list Order
                );
        }

        private Func<Flat,bool> SearchProperty(AllPropertyViewModel vM)
        {
            vM.PriceFrom = ValidateType.isCheck<double>(vM.PriceFrom) ? vM.PriceFrom : null;
            vM.PriceTo = ValidateType.isCheck<double>(vM.PriceTo) ? vM.PriceTo : null;
            vM.QuantityRoomFrom = ValidateType.isCheck<int>(vM.QuantityRoomFrom) ? vM.QuantityRoomFrom : null;
            vM.QuantityRoomTo = ValidateType.isCheck<int>(vM.QuantityRoomTo) ? vM.QuantityRoomTo : null;
            vM.AreaFrom = ValidateType.isCheck<double>(vM.AreaFrom) ? vM.AreaFrom.Replace(".",",") : null;
            vM.AreatTo = ValidateType.isCheck<double>(vM.AreatTo) ? vM.AreatTo : null;
            vM.CondignationFrom = ValidateType.isCheck<int>(vM.CondignationFrom) ? vM.CondignationFrom : null;
            vM.CondignationTo = ValidateType.isCheck<int>(vM.CondignationTo) ? vM.CondignationTo : null;

            return x => x.IsHidden == false && (vM.City == null || x.City.ToLower().Contains(vM.City.ToLower()))
                && (vM.PriceFrom == null || x.Price >= decimal.Parse(vM.PriceFrom))
                && (vM.PriceTo == null || x.Price <= decimal.Parse(vM.PriceTo))
                && (vM.IsBalcon == x.IsBalcon)
                && (vM.QuantityRoomFrom == null || x.QuantityRoom >= int.Parse(vM.QuantityRoomFrom))
                && (vM.QuantityRoomTo == null || x.QuantityRoom <= int.Parse(vM.QuantityRoomTo))
                && (vM.AreaFrom == null || x.Area >= decimal.Parse(vM.AreaFrom))
                && (vM.AreatTo == null || x.Area <= decimal.Parse(vM.AreatTo))
                && (vM.CondignationFrom == null || x.Condignation >= int.Parse(vM.CondignationFrom))
                && (vM.CondignationTo == null || x.Condignation <= int.Parse(vM.CondignationTo))
                ;
        }

        #endregion
    }
}