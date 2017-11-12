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

        /// <summary>
        /// Check is own Property
        /// </summary>
        public bool IsOwnProperty { get; set; }

        /// <summary>
        /// checks if the user bought property before
        /// </summary>
        public bool isBuyAfter { get; set; }
    }
}