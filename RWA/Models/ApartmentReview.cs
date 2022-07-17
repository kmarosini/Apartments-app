using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RWA.Models
{
    public class ApartmentReview
    {
        [Required(ErrorMessage = "Niste unjeli detalje!")]
        public string Details { get; set; }

        [Required(ErrorMessage = "Niste unjeli zvjezde!")]
        public int Stars { get; set; }

        public override string ToString() => $"{Details}";
    }
}