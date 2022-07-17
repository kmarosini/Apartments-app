using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RWA.Models
{
    public class Filter
    {
        
        public List<SelectListItem> Grad { get; set; }
        public int RoomNumber { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
    }
}