using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models
{
    public class Flat
    {

        private int _flatId;
        private TypeFlat _typeFlat;
        private string _userId;
        private string _quantityRoom;
        private decimal _area;
        private int _condignation;
        private bool _isBalcon;
        private string _description;
        private bool _isHidden;
        private decimal _price;
        private string _city;
        private string _street;
        private string _postCode;
        private DateTime _addFlate;
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Album> Album { get; set; }
        public virtual ICollection<Order> Order { get; set; }

        public int FlatId
        {
            get
            {
                return _flatId;
            }
            set
            {
                //throw new ArgumentException("Name cannot be null or empty string", "FlatId");
                _flatId = value;
            }
        }

        public string QuantityRoom
        {
            get { return QuantityRoom1; }
            set { QuantityRoom1 = value; }
        }

        public TypeFlat TypeFlat
        {
            get
            {
                return _typeFlat;
            }

            set
            {
                _typeFlat = value;
            }
        }

        public string UserId
        {
            get
            {
                return _userId;
            }

            set
            {
                _userId = value;
            }
        }

        public string QuantityRoom1
        {
            get
            {
                return _quantityRoom;
            }

            set
            {
                _quantityRoom = value;
            }
        }

        public decimal Area
        {
            get
            {
                return _area;
            }

            set
            {
                _area = value;
            }
        }

        public int Condignation
        {
            get
            {
                return _condignation;
            }

            set
            {
                _condignation = value;
            }
        }

        public bool IsBalcon
        {
            get
            {
                return _isBalcon;
            }

            set
            {
                _isBalcon = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        public bool IsHidden
        {
            get
            {
                return _isHidden;
            }

            set
            {
                _isHidden = value;
            }
        }

        public decimal Price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;
            }
        }

        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                _city = value;
            }
        }

        public string Street
        {
            get
            {
                return _street;
            }

            set
            {
                _street = value;
            }
        }

        public string PostCode
        {
            get
            {
                return _postCode;
            }

            set
            {
                _postCode = value;
            }
        }

        public DateTime AddFlate
        {
            get
            {
                return _addFlate;
            }

            set
            {
                _addFlate = value;
            }
        }

    }

    public enum TypeFlat
    {
        Datached,
        Villa,
        Farm,
        Skyscraper
    }
}