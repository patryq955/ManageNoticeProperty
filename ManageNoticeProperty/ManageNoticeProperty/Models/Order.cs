using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models
{
    public class Order
    {
        public virtual ApplicationUser BuyUser { get; set; }
        public virtual Flat Flat { get; set; }

        public int OrderId { get; set; }

        public int FlatId { get; set; }

        public string BuyUserID { get; set; }

        [Required(ErrorMessage = "Opis do zamówiena jest wymagany")]
        public string Description { get; set; }

        public bool isDelete { get; set; }

        public DateTime? SellDate { get; set; }
    }
}