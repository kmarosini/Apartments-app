using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class ApartmentPicture
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public int ApartmentId { get; set; }
        public string Path { get; set; }
        public byte[] Base64Content { get; set; }
        public string Name { get; set; }
        public bool isRepresentative { get; set; }

        public override string ToString() => $"{Name}";
    }
}
