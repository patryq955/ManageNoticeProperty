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

namespace ManageNoticeProperty.Controllers
{
    public class PropertyController : Controller
    {
        private ApplicationUserManager _userManager;
        private IRepository<TypeFlat> _typeFlatRepository;
        public PropertyController()
        {
            _typeFlatRepository = new TypeFlatRepository();
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
            vM.TypeFlat = _typeFlatRepository.GetOverview().ToList();            
            return View(vM);
        }

        [HttpPost]
        public async Task<ActionResult> AddProperty(Flat flat)
        {
            if(Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                flat.UserId = user.Id;
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult GetProperty(int? id)
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