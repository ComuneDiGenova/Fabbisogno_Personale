// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace FabbPers.DataAccess
{
  public class MotiviRichiestaDAO
  {
    public List<dynamic> Read(string query)
    {

      string sql = "SELECT ID,NOME FROM PERS_MOTIVO_RICHIESTA ORDER BY NOME";
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        if (query != null)
        {
          sql += " where ID=@id";
          SqlParameter g = new SqlParameter("@id", SqlDbType.Int);
          g.Value = int.Parse(query);
          cmd.Parameters.Add(g);
        }

        SqlDataReader reader = cmd.ExecuteReader();
        return DbManager.DynamicOutput(reader);
      }
      finally
      {
        if (cmd?.Connection?.State != ConnectionState.Closed)
          cmd.Connection.Close();
      }
    }

    public int Create(string nome)
    {
      string sql = "INSERT INTO PERS_MOTIVO_RICHIESTA (NOME) VALUES(@NOME)";
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        SqlParameter p = new SqlParameter("@nome", SqlDbType.VarChar);
        p.Value = nome;
        cmd.Parameters.Add(p);

        int numResult = cmd.ExecuteNonQuery();
        return numResult;
      }
      finally
      {
        if (cmd?.Connection?.State != ConnectionState.Closed)
          cmd.Connection.Close();
      }
    }

    public int Update(ListOfValue lov)
    {
      string sql = "UPDATE PERS_MOTIVO_RICHIESTA SET NOME = @NOME WHERE ID = @ID";
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        SqlParameter p = new SqlParameter("@nome", SqlDbType.VarChar);
        p.Value = lov.Nome;
        cmd.Parameters.Add(p);
        SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
        p1.Value = lov.Id;
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

    public int Delete(int id)
    {
      string sql = "DELETE FROM PERS_MOTIVO_RICHIESTA WHERE ID = @ID";
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