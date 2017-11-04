using ManageNoticeProperty.Infrastructure;
using ManageNoticeProperty.Models;
using ManageNoticeProperty.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace ManageNoticeProperty.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Album> _repo;
        public HomeController(IRepository<Album> repo)
        {
            _repo = repo;
        }
        public ActionResult Index()
        {
            LastVisit test = new LastVisit();

            return View();
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}