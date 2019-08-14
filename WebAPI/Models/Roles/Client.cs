using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Auth.Roles
{
    public class Client : User
    {

        public Client()
        {
            Reservations = new HashSet<Reservation>();
        }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
