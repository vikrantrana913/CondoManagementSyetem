using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace CondoEntity
{
    public enum Role
    {
        [Display(Name = "Admin")]
        Admin = 2,
       
    }
      public class UserDbModel
      {
        public Int64 UserId { get; set; }

        [Display(Name="Condo Name")]
        [RegularExpression("^[A-Za-z -]*$", ErrorMessage = "Sorry, only letters (a-z) are allowed.")]
        public string CondoName { get; set; }

        [Display(Name ="Condo Picture")]
        public string CondoPicture { get; set; }

        [Display(Name ="Role Type")]
        [Required(ErrorMessage ="Required to define role.")]
        public Role RoleType { get; set; }

        [MaxLength(50,ErrorMessage ="The length of username should be less then 50")]
        [Required(ErrorMessage ="Username is required.")]
        [Remote("IsUsernameAvailable", "SuperAdmin", HttpMethod = "Post", ErrorMessage = "Username is already exist.", AdditionalFields = "initialUsername")]
        public string Username { get; set; }

        [MaxLength(50,ErrorMessage ="The length of name should be less then 50")]
        [RegularExpression("^[A-Za-z -]*$", ErrorMessage = "Sorry, only letters (a-z) are allowed.")]
        [Required(ErrorMessage ="Name field is required.")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(50,ErrorMessage ="The length of email should be less then 50.")]
        [Required(ErrorMessage ="Email is required.")]
        [Remote("IsEmailAvailable", "SuperAdmin", HttpMethod = "Post", ErrorMessage = "Email is already exist.", AdditionalFields = "initialEmail")]
        public string Email { get; set; }


        [Required(ErrorMessage ="Password is required.")]
        [DataType(DataType.Password,ErrorMessage ="Invalid password.")]
        [RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Invalid password format")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Password is required.")]
        [Display(Name="Confirm Password")]
        [RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Invalid password format")]
        [System.Web.Mvc.Compare("Password",ErrorMessage ="Password does not match")]
        public string ConfirmPassword { get; set; }


        public string initialUsername
        {
            get
            {
                if (Username != null)
                {
                    return Username.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public string initialEmail
        {
            get
            {
                if (Email != null)
                {
                    return Email.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }


    }
 
}
