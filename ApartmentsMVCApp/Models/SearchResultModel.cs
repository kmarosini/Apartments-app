using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApartmentsMVCApp.Models
{
    public class SearchResultModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public int? TotalRooms { get; set; }
        public int? MaxAdults { get; set; }
        public int? MaxChildren { get; set; }
        public decimal Price { get; set; }
    }
}