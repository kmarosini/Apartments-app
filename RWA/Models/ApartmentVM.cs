using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RWA.Models
{
    public class ApartmentVM
    {
        public List<Apartment> ListaApartmana { get; set; }
        public Filter Filter { get; set; }
    }
}

