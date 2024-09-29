using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CondoEntity
{
    public class ChangePasswordDbModel
    {

        public Int64 UserId { get; set; }

       
        [Display(Name = "Old Password")]

          
        public string Password { get; set; }

        [MaxLength(10, ErrorMessage = "Invalid password.")]
        [Display(Name ="New Password")]
        [Required(ErrorMessage = "Field is required.")]
        [DataType(DataType.Password)]
        [RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Invalid password format")]
        public string NewPassword { get; set; }

        [Display(Name ="Confirm Password")]
        [Required(ErrorMessage = "Field is required.")]
        [Compare("NewPassword", ErrorMessage ="Password does not match.")]
        public string ConfirmPassword { get; set;}

        [MaxLength(50,ErrorMessage ="Length of username should be less then 50.")]
        public string Username { get; set;}


       

    }
}
