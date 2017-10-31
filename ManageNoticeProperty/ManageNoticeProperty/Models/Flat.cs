using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models
{
    public class Flat
    {

        private int _flatId;
        private TypeFlat _typeFlat;
        private string _userId;
        private int _quantityRoom;
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

        public virtual ICollection<TypeFlat> TypeFlat { get; set; }

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

        [Range(1,20,ErrorMessage ="Ilość pokoi może wynosić 1-20")]
        [Display(Name ="Ilość pokoi")]
        public int QuantityRoom
        {
            get { return _quantityRoom; }
            set { _quantityRoom = value; }
        }

        public int TypeFlatID { get; set; }


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

        [Display(Name = "Powierzchnia")]
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

        [Display(Name ="Ilość kondygnacji")]
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

        [Display(Name = "Czy mieszkanie posiada balkon")]
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

        [Required(ErrorMessage ="Podaj krótki opis mieszkania")]
        [Display(Name = "Opis")]
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

        [Display(Name = "Cena")]
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

        [Display(Name = "Miasto")]
        [Required(ErrorMessage ="Podaj miasto")]
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

        [Display(Name = "Ulica")]
        [Required(ErrorMessage ="Podaj nazwę ulicy")]
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

        [Display(Name = "Kod pocztowy")]
        [RegularExpression("^[0-9][0-9]-[0-9][0-9][0-9]$")]
        [Required(ErrorMessage ="Podaj kod pocztowy")]
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
}