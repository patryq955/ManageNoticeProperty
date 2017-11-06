using ManageNoticeProperty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.ViewModel
{
    public class AdminRaportViewModel
    {
        public List<Flat> Flats { get; set; }
        public decimal TotalPrice { get; set; }
        
    }
}