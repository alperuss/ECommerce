using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Data.Entities
{
    public class User : Abstracts.Entity
    {
        [Required, MaxLength(50), MinLength(2)]
        public String Name { get; set; }

        [Required, MaxLength(50), MinLength(2)]
        public String Surname { get; set; }

        [Required, MaxLength(350), MinLength(6)]
        public String Email { get; set; }

        [Required]
        public bool EmailVerified { get; set; }

        [Required, MaxLength(40), MinLength(40)]
        public String Password { get; set; }

        [Required]
        public bool Admin { get; set; }

        public Guid? AutoLoginKey { get; set; }

        public int TitleId { get; set; }
        public virtual Title Title { get; set; }
    }
}
