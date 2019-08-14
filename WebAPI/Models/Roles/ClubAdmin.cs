using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Auth.Roles
{
    public class ClubAdmin : User
    {
        public ClubAdmin()
        {
            Clubs = new HashSet<Club>();
        }
        public ICollection<Club> Clubs { get; set; }
    }
}
