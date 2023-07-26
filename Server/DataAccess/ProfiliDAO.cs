// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace FabbPers.DataAccess
{
  public class ProfiliDAO
  {
    public List<dynamic> Read()
    {

      string sql = "SELECT ID,CATEGORIA, NOME FROM PERS_PROFILO ORDER BY CATEGORIA, NOME";
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        SqlDataReader reader = cmd.ExecuteReader();
        return DbManager.DynamicOutput(reader);
      }
      finally
      {
        if (cmd?.Connection?.State != ConnectionState.Closed)
          cmd.Connection.Close();
      }
    }

    public int Create(Profilo pf)
    {
      string sql = "INSERT INTO PERS_PROFILO (CATEGORIA, NOME) VALUES(@CATEGORIA, @NOME)";
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        SqlParameter p = new SqlParameter("@nome", SqlDbType.VarChar);
        p.Value = pf.Nome;
        cmd.Parameters.Add(p);
        SqlParameter p1 = new SqlParameter("@CATEGORIA", SqlDbType.VarChar);
        p1.Value = pf.Categoria;
        cmd.Parameters.Add(p1);

        int numResult = cmd.ExecuteNonQuery();
        return numResult;
      }
      finally
      {
        if (cmd?.Connection?.State != ConnectionState.Closed)
          cmd.Connection.Close();
      }
    }

    public int Update(Profilo pf)
    {
      string sql = "UPDATE PERS_PROFILO SET NOME = @NOME, CATEGORIA = @CATEGORIA WHERE ID = @ID";
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        SqlParameter p = new SqlParameter("@nome", SqlDbType.VarChar);
        p.Value = pf.Nome;
        cmd.Parameters.Add(p);
        SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
        p1.Value = pf.Id;
        cmd.Parameters.Add(p1);
        SqlParameter p2 = new SqlParameter("@CATEGORIA", SqlDbType.VarChar);
        p2.Value = pf.Categoria;
        cmd.Parameters.Add(p2);

        int numResult = cmd.ExecuteNonQuery();
        return numResult;
      }
      finally
      {
        if (cmd?.Connection?.State != ConnectionState.Closed)
          cmd.Connection.Close();
      }
    }

    public int Delete(int id)
    {
      string sql = "DELETE FROM PERS_PROFILO WHERE ID = @ID";
      SqlCommand cmd = DbManager.CreateCommand(sql);


      try
      {
        SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
        p1.Value = id;
        cmd.Parameters.Add(p1);

        int numResult = cmd.ExecuteNonQuery();
        return numResult;
      }
      finally
      {
        if (cmd?.Connection?.State != ConnectionState.Closed)
          cmd.Connection.Close();
      }
    }
  }
}