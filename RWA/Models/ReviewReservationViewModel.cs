using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RWA.Models
{
    public class ReviewReservationViewModel
    {
        public ApartmentReservation Reservation { get; set; }
        public ApartmentReview Review { get; set; }
    }
}