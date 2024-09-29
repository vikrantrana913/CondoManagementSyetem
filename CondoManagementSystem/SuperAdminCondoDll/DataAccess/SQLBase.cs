using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperAdminCondoDll.DataAccess
{
    public abstract class SQLBase
    {
        public SQLBase() 
        {
        
        }
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        }


    }
}
