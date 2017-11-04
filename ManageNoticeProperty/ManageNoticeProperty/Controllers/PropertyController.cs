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

namespace ManageNoticeProperty.Controllers
{
    public class PropertyController : Controller
    {
        private ApplicationUserManager _userManager;
        private IRepository<TypeFlat> _typeFlatRepository;
        private IRepository<Flat> _flatRepository;
        private ILastVisit _lastVisit;
        public PropertyController(IRepository<TypeFlat> typeFlatRepository, 
            IRepository<Flat> flatRepository, ILastVisit lastVisit)
        {
            _typeFlatRepository = typeFlatRepository;
            _flatRepository = flatRepository;
            _lastVisit = lastVisit;
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
            flat = _flatRepository.GetID(id);
            if (flat == null)
            {
                return View("NothingProperty");
            }
            _lastVisit.AddLasstViewProperty(id);
            return View(flat);
        }

        public ActionResult NothingProperty()
        {
            return View();
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
    }
}