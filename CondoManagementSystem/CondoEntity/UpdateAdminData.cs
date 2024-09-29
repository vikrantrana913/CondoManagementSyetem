using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoEntity
{
    public class UpdateAdminData
    {
        public Int64 UserId { get; set; }

        [Required(ErrorMessage ="This field is required.")]
        [MaxLength(50,ErrorMessage ="Length of name should be less then.")]
        [RegularExpression("^[A-Za-z -]*$", ErrorMessage = "Sorry, only letters (a-z) are allowed.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(50, ErrorMessage = "Length of username should be less then.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(50, ErrorMessage = "Length of name should be less then.")]
        public string Email { get; set; }   

        public string Password { get; set; }

        [Required(ErrorMessage ="This field is required.")]
        public string ConfirmPassword { get; set; } 


    }
}
