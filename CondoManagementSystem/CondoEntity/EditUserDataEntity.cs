using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoEntity
{
    public class EditUserDataEntity
    {

        public Int64 UserId { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(50, ErrorMessage = "Length of name should be less then 50.")]
        [Required(ErrorMessage ="Field is required.")]
        [RegularExpression("^[A-Za-z -]*$", ErrorMessage = "Sorry, only letters (a-z) are allowed.")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress,ErrorMessage ="Invalid Email")]
        [MaxLength(50,ErrorMessage ="Length of email should be less then 50.")]
        [Required(ErrorMessage ="Field is required.")]
        public string Email { get; set; }   
    }
}
