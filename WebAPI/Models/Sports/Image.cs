using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Sports
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdImage { get; set; }
        public string ImageName { get; set; }

        //navigation
        public Guid IdTerrain { get; set; }
        public Terrain Terrain { get; set; }
    }
}
