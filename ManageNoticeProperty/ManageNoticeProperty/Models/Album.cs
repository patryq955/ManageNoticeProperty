using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManageNoticeProperty.Models
{
    public class Album
    {
        public virtual Flat Flat { get; set; }

        public int AlbumId { get; set; }
        public int FlatId { get; set; }

        [Required(ErrorMessage = "Podaj ścieżke zdjęcia")]
        public byte[] Photo { get; set;}

    }
}