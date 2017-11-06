using ManageNoticeProperty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.ViewModel
{
    public class GetPropertyOrderViewModel
    {
        public Flat Flat { get; set; }
        public Order Order { get; set; }
        public bool IsOwnProperty { get; set; }
    }
}