using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class User : IdentityUser
    {


        [Column(TypeName ="nvarchar(150)")]
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; }

    }
}
