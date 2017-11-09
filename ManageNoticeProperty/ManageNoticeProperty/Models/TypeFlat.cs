using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models
{
    public class TypeFlat
    {
        public int TypeFlatID { get; set; }

        [Required(ErrorMessage = "Podaj nazwe typu mieszkania")]
        [StringLength(50, ErrorMessage = "Nazwa typu mieszkania jest zbyt długa")]
        public string Name { get; set; }



    }
}