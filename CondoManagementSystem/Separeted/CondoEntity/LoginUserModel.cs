using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CondoEntity
{
    public class LoginUserModel
    {
        public Int64 UserId { get; set; }

        public Int64 UserCondoId { get; set; } 
        
        public Int64 CondoId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

      
        [Required(ErrorMessage = "Email is required.")]
        [Display(Name ="Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Field is required.")]
        public Int64 RoleType { get; set; }



    }
}
