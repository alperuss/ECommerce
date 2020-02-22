using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Data.Entities
{
    public class User:Abstracts.Entity
    {
        [Required, MinLength(2), MaxLength(50)]
        public string Name { get; set; }
        [Required, MinLength(2), MaxLength(50)]
        public string Surname { get; set; }
        [Required, MinLength(2), MaxLength(50)]
        public string Email { get; set; }
      

        public string Password { get; set; }

    }
}
