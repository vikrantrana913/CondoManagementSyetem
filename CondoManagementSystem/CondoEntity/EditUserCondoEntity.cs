using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoEntity
{
    public class EditUserCondoEntity
    {

        public Int64 CondoId { get; set; }

        [Display(Name ="Condo Name")]
        [Required(ErrorMessage ="Field is required.")]
        [MaxLength(50,ErrorMessage ="Length of name should be less then 50.")]
        public String CondoName { get; set; }

        [Display(Name ="Condo Picture")]
        public String CondoPicture { get; set; }

        [Display(Name = "Condo Email")]
        [Required(ErrorMessage ="Field is required.")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Invalid email.")]
        [MaxLength(50,ErrorMessage ="Length of email should be less then 50.")]
        public String CondoEmail { get; set;}

        [MaxLength(500,ErrorMessage ="Length of address should be less then 500.")]
        [Required(ErrorMessage ="Field is required.")]
        public String Address { get; set;}

        [MaxLength(12,ErrorMessage ="Length of contact should be less then 12.")]
        [Required(ErrorMessage ="Field is required.")]
        public String Contact { get; set;}


    }
}
