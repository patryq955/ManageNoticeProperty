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
        IRepository<Flat> _flatRepository;
        ILastVisit _lastVisit;
        public HomeController(IRepository<Flat> flatRepository, ILastVisit lastVisit)
        {
            _flatRepository = flatRepository;
            _lastVisit = lastVisit;
        }
        public ActionResult Index()
        {
            var lista = _lastVisit.GetLastViewProperty().ToList();
            List<Flat> lastVisitProperty= new List<Flat>();
            foreach (var id in lista)
            {
                var test = _flatRepository.GetID(int.Parse(id));
                lastVisitProperty.Add(_flatRepository.GetID(int.Parse(id)));
            }
            return View(lastVisitProperty);

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