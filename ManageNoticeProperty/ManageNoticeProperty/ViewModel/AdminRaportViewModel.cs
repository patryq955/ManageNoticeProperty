using ManageNoticeProperty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.ViewModel
{
    public class AdminRaportViewModel
    {
        public List<Order> Order { get; set; }
        public decimal TotalPrice { get; set; }
        
    }
}