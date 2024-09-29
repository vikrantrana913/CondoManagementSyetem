using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoBll
{
    public enum Role
    {
        [Display(Name = "Admin")]
        Admin = 2,
        [Display(Name = "Super Admin")]
        SuperAdmin = 1
    }

    public class UserCondoData
    {
        public Int64 UserCondoId { get; set; }  

        public Int64 UserId { get; set; }
        public Int64 CondoId { get; set; }

        [Required(ErrorMessage ="Field is required.")]
        public Role RoleType { get; set; }

        [MaxLength(50,ErrorMessage ="Length of name should be less then 50.")]
        [Required(ErrorMessage ="Field is required.")]
        [RegularExpression("^[a-zA-Z0-9'@&#.\\s]*$", ErrorMessage = "Sorry, only letters (a-z), numbers (0-9), and periods ('@&#.) are allowed.")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(50,ErrorMessage ="Length of email should be less then 50.")]
        [Required(ErrorMessage ="Field is required.")]
        public string Email { get; set; }


        [MaxLength(50,ErrorMessage ="Length of username should be less then 50.")]
        [Required(ErrorMessage = "Field is required.")]
        public string Username { get; set; }

        [Display(Name ="Condo Name")]
        [MaxLength(ErrorMessage ="Length of name should be less then 50.")]
        [Required(ErrorMessage ="Field is required.")]
        [RegularExpression("^[a-zA-Z0-9'@&#.\\s]*$", ErrorMessage = "Sorry, only letters (a-z), numbers (0-9), and periods ('@&#.) are allowed.")]
        public string CondoName { get; set; }

        [Display(Name ="Condo Email")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50, ErrorMessage = "Length of email should be less then 50.")]
        [Required(ErrorMessage = "Field is required.")]
        public string CondoEmail { get; set;}


        [Display(Name ="Condo Picture")]
        [Required(ErrorMessage ="Field is required.")]
        public string CondoPicture { get; set;}

        [MaxLength(500,ErrorMessage ="Length of the address should be less then 500.")]
        [Required(ErrorMessage = "Field is required.")]
        public string Address { get; set;}

        [MaxLength(12,ErrorMessage ="Length of contact should be less then 12.")]
        [Required(ErrorMessage = "Field is required.")]
        public string Contact { get; set;}

    }
}
