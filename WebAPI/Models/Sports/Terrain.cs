using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using WebAPI.Models.Sports;
using Image = WebAPI.Models.Sports.Image;

namespace WebAPI.Models
{
public class Terrain
    {

        public Terrain()
        {
            Reservations = new HashSet<Reservation>();
            Images = new HashSet<Image>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdTerrain { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public bool Free { get; set; } = true;
        public double Price { get; set; }
        //Navigation
        public Guid IdClub { get; set; }
        public Club club { get; set; }

        //ReservationList
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Image> Images { get; set; }

             
    }
}
