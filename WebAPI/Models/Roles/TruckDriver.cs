using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Auth.Roles;

namespace WebAPI.Models.Roles
{
    public class TruckDriver : User
    {
        public string Lng { get; set; }
        public string Lat { get; set; }

        //Navigation
        //ClubAdmin
        public Guid ClubAdminId { get; set; }
        public ClubAdmin ClubAdmin { get; set; }
    }
}
