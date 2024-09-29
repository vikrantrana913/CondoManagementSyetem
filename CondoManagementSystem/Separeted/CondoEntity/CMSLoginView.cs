using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoEntity
{
    public class CMSLoginView
    {

        public Int64 UserId { get; set; }

        public Int64 CondoId { get; set; }

        [Required(ErrorMessage ="Enter your username.")]
        [MaxLength(50,ErrorMessage ="Length of username should be less then 50.")]
        public string Username { get; set; }

        [Required(ErrorMessage ="Enter your password.")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool Rememberme { get; set; }

        public Int64 UserCondoId { get; set; }

    }
}
