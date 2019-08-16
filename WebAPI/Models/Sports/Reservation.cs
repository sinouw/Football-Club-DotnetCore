using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Auth.Roles;

namespace WebAPI.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdReservation { get; set; }
        public string IdClient { get; set; }
        public Client Client { get; set; }
        public Guid IdTerrain { get; set; }
        public Terrain Terrain { get; set; }
        public string resDay { get; set; }
        public string StartRes { get; set; }
        public string EndRes { get; set; }
        public string status { get; set; }
    }
}
