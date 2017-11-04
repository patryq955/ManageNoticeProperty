using ManageNoticeProperty.Models;
using ManageNoticeProperty.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.ViewModel
{
    public class FlatViewModel
    {
        private TypeFlatRepository _typeFlatRepository;
        public Flat Flat { get; set; }
        public IEnumerable<TypeFlat> TypeFlat { get; set; }

        public FlatViewModel()
        {
            _typeFlatRepository = new TypeFlatRepository();
            TypeFlat = _typeFlatRepository.GetOverview().ToList();
        }
    }
}