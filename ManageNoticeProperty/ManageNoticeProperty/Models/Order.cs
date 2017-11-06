using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models
{
    public class Order
    {
        private int _orderId;
        private int _flatId;
        private string _buyUserId;
        public virtual ApplicationUser BuyUser { get; set; }
        public virtual Flat Flat { get; set; }

        public int OrderId
        {
            get
            {
                return _orderId;
            }

            set
            {
                _orderId = value;
            }
        }

        public int FlatId
        {
            get
            {
                return _flatId;
            }

            set
            {
                _flatId = value;
            }
        }

        public string BuyUserID
        {
            get
            {
                return _buyUserId;
            }

            set
            {
                _buyUserId = value;
            }
        }

        [Required(ErrorMessage = "Opis do zamówiena jest wymagany")]
        public string Description { get; set; }
    }
}