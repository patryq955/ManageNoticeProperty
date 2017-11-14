using ManageNoticeProperty.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.ViewModel
{
    public class AllPropertyViewModel
    {
        public IPagedList<Flat> Flat { get; set; }

        public IEnumerable<TypeFlat> TypeFlat { get; set; }


        public string City { get; set;}

        public string QuantityRoomTo { get; set; }

        public string QuantityRoomFrom { get; set; }

        public string AreatTo { get; set; }

        public string AreaFrom { get; set; }

        public string CondignationTo { get; set; }

        public string CondignationFrom { get; set; }

        public string PriceTo { get; set; }

        public string PriceFrom { get; set; }

        public bool IsBalcon { get; set; }

        public int TypeFlatID { get; set; }
    }
}