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

        public int TypeFlatID { get; set; }

        public string City { get; set; }

        public int QuantityRoomTo { get; set; }

        public int QuantityRoomFrom { get; set; }

        public decimal AreatTo { get; set; }

        public decimal AreaFrom { get; set; }

        public decimal CondignationTo { get; set; }

        public decimal CondignationFrom { get; set; }

        public decimal PriceTo { get; set; }

        public decimal PriceFrom { get; set; }

        public bool IsBalcon { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int RecordCount { get; set; }
    }
}