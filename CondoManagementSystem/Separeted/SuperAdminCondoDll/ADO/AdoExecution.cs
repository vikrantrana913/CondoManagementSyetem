using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperAdminCondoDll.ADO
{
    public class AdoExecution : IDisposable
    {
        SqlConnection _con;

        public AdoExecution(string connectionString)
        {
            _con = new SqlConnection();
            _con.ConnectionString= connectionString;    
            if(_con.State==ConnectionState.Closed)
            {
                _con.Open();
            }
        }
        
        public int ExecuteNonQuery(CommandType commandType,string commandText,params IDataParameter[] parameters)
        {
        IDbCommand com=_con.CreateCommand();
        com.CommandText= commandText;
            com.CommandType = commandType;
            if(commandType==CommandType.StoredProcedure && parameters !=null)
            { 
                    foreach(IDataParameter parameter in parameters) 
                    {
                        com.Parameters.Add(parameter); 
                    } 
            }
        return com.ExecuteNonQuery();   
        }
        public IDataReader ExecuteReader(CommandType commandType,string commandText,params IDataParameter[] parameters) 
        {
        IDbCommand command=_con.CreateCommand();
            command.CommandText= commandText;   
            command.CommandType = commandType;  
            if(commandType==CommandType.StoredProcedure && parameters!=null)
            {
                foreach(IDataParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);  
                }
            }


        return command.ExecuteReader(); 
        }






        public void Dispose() 
        {
            if(_con != null ) 
            {
                _con.Close();

            }
        
        }

    }
}
