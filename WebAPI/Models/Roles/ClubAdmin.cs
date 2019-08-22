using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Roles;

namespace WebAPI.Models.Auth.Roles
{
    public class ClubAdmin : User
    {
        public ClubAdmin()
        {
            Clubs = new HashSet<Club>();
            TruckDrivers = new HashSet<TruckDriver>();
        }
        public ICollection<Club> Clubs { get; set; }
        public ICollection<TruckDriver> TruckDrivers { get; set; }
    }
}
