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
        [Required,MinLength(40),MaxLength(40)]
        public string Password { get; set; }
        [Required,]
        public bool Admin { get; set; }

        public Guid? AutoLoginKey { get; set; }

    }
}
