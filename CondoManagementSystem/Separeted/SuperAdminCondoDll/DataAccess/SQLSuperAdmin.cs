using CondoEntity;
using SuperAdminCondoDll.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperAdminCondoDll.DataAccess
{
    public partial class SQLSuperAdmin:SQLBase,ISuperAdmin
    {
        public SQLSuperAdmin():base()
        {

        }

    }
}
