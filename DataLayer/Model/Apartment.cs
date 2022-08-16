using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Apartment
    {
        //Apartment
        public string Name { get; set; }
        public string ApartmentName { get; set; }
        public int Id { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int TotalRooms { get; set; }
        public decimal Price { get; set; }
        public Guid Guid{ get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int OwnerId { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int CityId { get; set; }
        public string Adress { get; set; }
        public string NameEng { get; set; }
        public int BeachDistance { get; set; }
        public string StatusName { get; set; }
        public int Ukupno { get; set; }
        public byte[] Base64Content { get; set; }
        public int ApartmentRating { get; set; }
        public string ImageString { get; set; }

    }
}
