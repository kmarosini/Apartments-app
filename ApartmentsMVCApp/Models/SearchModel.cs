using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApartmentsMVCApp.Models
{
    public class SearchModel
    {
        public int FilterRooms { get; set; }
        public int FilterAdults { get; set; }
        public int FilterChildren { get; set; }
        public int FilterCity { get; set; }
        public int Order { get; set; }

        public List<City> ListaGradova{ get; set; }
        public List<SearchResultModel> ListaRezultata { get; set; }



    }
}