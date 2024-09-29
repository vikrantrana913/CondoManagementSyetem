using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SuperCondoEntities
{
    public class LoginSuperAdminDbModel
    {

        public Int64 UserId { get; set; }

        public Int64 CondoId { get; set; }

        [Required(ErrorMessage ="Enter your username.")]

        public string Username { get; set; }

        [Required(ErrorMessage ="Enter your password.")]
        public string Password { get; set; }

        public string Name { get; set; }
        
        public string Email { get; set; }

        public int RoleType { get; set; }


        [Display(Name = "Remember me")]
        public bool Rememberme { get; set; }



    }
}
