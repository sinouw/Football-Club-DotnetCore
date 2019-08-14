using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Auth.Roles
{
    public class SuperAdmin : User
    {
        public SuperAdmin()
        {
            Clubs = new HashSet<Club>();
        }
        public ICollection<Club> Clubs { get; set; }
    }
}
