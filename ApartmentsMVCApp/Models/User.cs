using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApartmentsMVCApp.Models
{
    public class User
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Unesite username")]
        public string username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Unesite password")]
        public string pass { get; set; }
    }
}