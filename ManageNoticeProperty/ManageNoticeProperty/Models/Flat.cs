﻿using ManageNoticeProperty.Models.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models
{
    public class Flat
    {

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Album> Album { get; set; }
        public virtual ICollection<Order> Order { get; set; }

        public virtual ICollection<TypeFlat> TypeFlat { get; set; }

        public int FlatId { get; set; }

        [Display(Name = "Typ mieszkania")]
        public int TypeFlatID { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "Podaj miasto")]
        public string City { get; set; }

        [Display(Name = "Powierzchnia")]
        [Range(1,10000,ErrorMessage ="Powierzchnia jest za mała")]
        public decimal Area { get; set; }

        [Range(1, 20, ErrorMessage = "Ilość pokoi może wynosić {1}-{2}")]
        [Display(Name = "Ilość pokoi")]
        public int QuantityRoom { get; set; }

        [Display(Name = "Ilość kondygnacji")]
        [Range(1, 200,ErrorMessage = "Ilość kondygnacji musi zawierać sie w przedziale {1}-{2}")]
        public int Condignation { get; set; }

        [Display(Name = "Czy mieszkanie posiada balkon")]
        public bool IsBalcon { get; set; }

        [Required(ErrorMessage = "Podaj krótki opis mieszkania")]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        public bool IsHidden { get; set; }

        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Display(Name = "Ulica")]
        [Required(ErrorMessage = "Podaj nazwę ulicy")]
        public string Street { get; set; }

        [Display(Name = "Kod pocztowy")]
        [RegularExpression("^[0-9][0-9]-[0-9][0-9][0-9]$",ErrorMessage ="Nieprawidłowy kod pocztowy")]
        [Required(ErrorMessage = "Podaj kod pocztowy")]
        public string PostCode { get; set; }

        public DateTime AddFlate { get; set; }

    }
}