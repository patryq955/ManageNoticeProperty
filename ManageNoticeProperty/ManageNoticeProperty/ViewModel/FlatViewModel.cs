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
        private Flat _flat;
        public Flat Flat
        {
            get
            {
                return _flat;
            }
            set
            {
                _flat = value;
            }
        }
        public IEnumerable<TypeFlat> TypeFlat { get; set; }

        public FlatViewModel()
        {
            _typeFlatRepository = new TypeFlatRepository();
            _flat = new Flat();
            TypeFlat = _typeFlatRepository.GetOverview().ToList();
        }
    }
}