using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RWA.Models
{
    public class ApartmentReservation
    {
        [Required(ErrorMessage = "Please enter user Email!")]
        [DataType(DataType.EmailAddress)]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "Please enter user name!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter user phone!")]
        public string UserPhone { get; set; }
        [Required(ErrorMessage = "Please enter user address!")]
        public string UserAddress { get; set; }
        [Required(ErrorMessage = "Please enter details!")]
        public string Details { get; set; }
    }
}