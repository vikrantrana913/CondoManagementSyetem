using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoDll.ADO
{
    public class AdoExecution:IDisposable
    {
        SqlConnection con;
        public AdoExecution(string connectionString)
        {
            con =new SqlConnection();
            con.ConnectionString = connectionString;
            if (con.State == ConnectionState.Closed)
            {
               con.Open();
            }
        }

        public int ExecuteNonQuery(System.Data.CommandType commandType, string commandText, params IDataParameter[] parameters)
        {
            // Create and configure a new command.
            IDbCommand com = con.CreateCommand();
            com.CommandType = commandType;
            com.CommandText = commandText;

            if (commandType == CommandType.StoredProcedure && parameters != null)
            {
                foreach (IDataParameter parameter in parameters)
                {
                    com.Parameters.Add(parameter);
                }
            }

            return com.ExecuteNonQuery();
        }

        public IDataReader ExecuteReader(CommandType commandType,string commandText,params IDataParameter[] parameters)
        {
            // Create and configure a new command.
            IDbCommand com = con.CreateCommand();
            com.CommandType = commandType;
            com.CommandText = commandText;
            if (commandType == CommandType.StoredProcedure && parameters != null)
            {
                foreach (IDataParameter parameter in parameters)
                {
                    com.Parameters.Add(parameter);
                }
            }

            return com.ExecuteReader();

        }

        public void Dispose()
        {
            if (con != null)
            {
                con.Close();
            }
        }

    }
}
