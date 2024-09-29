using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CondoEntity
{
    public class CondoDbModel
    {
        public Int64 CondoId { get; set; }

        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "The length of name should be less then 50")]
        [Required(ErrorMessage = "This field is required.")]
        public string CondoName { get; set; }




        [Display(Name = "Picture")]
        [DataType(DataType.ImageUrl,ErrorMessage ="Invalid image format.")]
        public string CondoPicture { get; set; }


         [MaxLength(50, ErrorMessage = "The length of email should be less then 50")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Invalid email.")]
        [Remote("IsCondoEmailAvailable", "SuperAdmin", HttpMethod = "Post", ErrorMessage = "Email is already exist.", AdditionalFields = "initialEmail")]
        public string CondoEmail { get; set;}



        [MaxLength(500, ErrorMessage = "The length of address should be less then 500")]
        [Required(ErrorMessage = "This field is required.")]
        public string Address { get; set; }

        [MaxLength(12, ErrorMessage = "The length of name should be less then 12")]
        [Required(ErrorMessage = "This field is required.")]
        public string Contact { get; set; }



        public string initialEmail
        {
            get
            {
                if (CondoEmail != null)
                {
                    return CondoEmail.ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
        }



    }
}
