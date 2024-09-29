using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoEntity
{
    public class UpdateUserCondoDbModel
    {
        public Int64 UserId { get; set; }

     
        public string Name { get; set; }

        public Int64 CondoId { get; set; }

       
        public string CondoName { get; set; }   

    }
}
