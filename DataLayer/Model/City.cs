using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class City
    {
        public City()
        {

        }

        public City(int iDCity, string name)
        {
            Id = iDCity;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()=> $"{Name}";
    }
}
