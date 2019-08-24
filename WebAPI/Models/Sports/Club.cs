using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Auth.Roles;

namespace WebAPI.Models
{
    public class Club
    {

        public Club()
        {
            Terrains = new HashSet<Terrain>();

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdClub { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public int Phone { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        
        public string OpeningTime { get; set; }
        public string ClosingTime { get; set; }

        [Required]
        public bool IsActive { get; set; }
        public string lng { get; set; }
        public string lat { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string StreetAddress { get; set; }
        public string Postal { get; set; }

        public ICollection<Terrain> Terrains { get; set; }

        //Navigation props
        //ClubAdmin
        public Guid ClubAdminId { get; set; }
        public ClubAdmin ClubAdmin { get; set; }
        //SuperAdmin
        public Guid SuperAdminId { get; set; }
        public SuperAdmin SuperAdmin { get; set; }

    }
}
