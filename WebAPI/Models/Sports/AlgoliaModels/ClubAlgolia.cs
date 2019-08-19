using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Sports
{
    public class ClubAlgolia
    {
        public string ObjectID { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
        public int Phone { get; set; }

        public string Email { get; set; }

        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }
    }
}
