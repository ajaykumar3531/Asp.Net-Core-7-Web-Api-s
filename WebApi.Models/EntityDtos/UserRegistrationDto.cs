using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models.Dtos
{
    public class UserRegistrationDto
    {
        public string Id { get; set; } = null!;
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*\W).+$", ErrorMessage = "Password must be strong")]
        public string Password {  get; set; }

        [Required]
        public string Rolename {  get; set; }
        [Required]
        public string PhoneNumber { get; set; }

    }
}
