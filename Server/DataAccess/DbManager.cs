// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;
using System.Dynamic;
//using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

namespace FabbPers.DataAccess
{
    public class DbManager
    {
        public static SqlCommand CreateCommand(string sql) {
            SqlConnection conn = new SqlConnection(Global.connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            cmd.CommandType= System.Data.CommandType.Text;
            
            return cmd;
        }

        public static List<dynamic> DynamicOutput(SqlDataReader dr ) {
             ColumnMap cm = new ColumnMap(dr);
            List<dynamic> outList = new List<dynamic>();
            while (dr.Read()) {
                dynamic expando = new ExpandoObject();
                var row= expando as IDictionary<string,object>;
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    if (dr.IsDBNull(i)) {
                            row.Add(dr.GetName(i),null);
                    } else if (dr.GetFieldType(i)== typeof(int)) {
                        row.Add(dr.GetName(i),dr.GetInt32(i));
                    } else if (dr.GetFieldType(i)== typeof(System.DateTime)) {
                        row.Add(dr.GetName(i),dr.GetDateTime(i));
                    }else {
                        row.Add(dr.GetName(i),dr.GetString(i));
                    }
                }
                outList.Add(row);
            }
            return outList;
        }

        public static bool CheckSorting(string sorting, List<string> allow) {
            allow.Add("ASC");
            allow.Add("DESC");
            string[] chunks = sorting.Split(",");
            foreach (var chunk in chunks)
            {
              string[] terms = chunk.Split(" ");
              foreach (var term in terms)
              {
                if (!allow.Contains(term.ToUpper())) {
                  return false;
                }    
              }
              
            } 
            return true;
        }
    }
}