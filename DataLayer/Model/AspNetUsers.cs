using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class AspNetUsers
    {
        //[Id], [Guid], [CreatedAt], [DeletedAt], 
        //[Email], [EmailConfirmed], [PasswordHash], 
        //[SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [LockoutEndDateUtc], 
        //[LockoutEnabled], [AccessFailedCount], [UserName], [Address]
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccesFailedCount { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }

        public override string ToString() => $"{UserName}";
    }
}
