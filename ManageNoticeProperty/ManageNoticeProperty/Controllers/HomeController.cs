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
        IExtendRepository<Flat> _flatRepository;
        ILastVisit _lastVisit;
        public HomeController(IExtendRepository<Flat> flatRepository, ILastVisit lastVisit)
        {
            _flatRepository = flatRepository;
            _lastVisit = lastVisit;
        }
        public ActionResult Index()
        {
            
            var lista = _lastVisit.GetLastViewProperty().Reverse().ToList();
            List<Flat> lastVisitProperty = new List<Flat>();
            foreach (var id in lista)
            {
                var flat = _flatRepository.GetID(int.Parse(id));
                if (flat != null)
                {
                    lastVisitProperty.Add(_flatRepository.GetID(int.Parse(id)));
                }
            }
            return View(lastVisitProperty);

        }
        public ActionResult Contact()
        {
            return View();
        }
    }
}