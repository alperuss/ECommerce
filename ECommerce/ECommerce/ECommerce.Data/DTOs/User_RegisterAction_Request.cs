using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Data.DTOs
{
    public class User_RegisterAction_Request
    {
        [Required, MinLength(2), MaxLength(50)]
        public string Name { get; set; }
        [Required, MinLength(2), MaxLength(50)]
        public string Surname { get; set; }

        [Required, MinLength(6), MaxLength(350)]
        public string Email { get; set; }

        [Required, MinLength(8), MaxLength(64)]
        public string Password { get; set; }
    }
}
