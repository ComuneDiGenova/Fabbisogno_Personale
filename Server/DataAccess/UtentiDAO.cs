// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;
using System.Dynamic;
using System.Data.SqlClient;
using System.Data;

namespace FabbPers.DataAccess
{
  public class UtentiDAO
  {

    public List<dynamic> Read(string username)
    {

      string sql = "SELECT ID,MATRICOLA, NOME,COGNOME,DIREZIONE,RUOLO,ATTIVO FROM PERS_UTENTE ";
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        if (username != null && username.Length > 0)
        {
          SqlParameter g = new SqlParameter("@MATRICOLA", SqlDbType.VarChar);
          sql += " WHERE MATRICOLA=@MATRICOLA and ATTIVO=1";
          g.Value = username;
          cmd = DbManager.CreateCommand(sql);
          cmd.Parameters.Add(g);
        }
        else
        {
          sql += "ORDER BY COGNOME, NOME";
          cmd = DbManager.CreateCommand(sql);
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

    public List<dynamic> Check(string username, string password)
    {

      string sql = "SELECT ID,MATRICOLA, NOME,COGNOME,DIREZIONE,RUOLO,ATTIVO FROM PERS_UTENTE ";
      sql += " WHERE MATRICOLA=@MATRICOLA and ATTIVO=1 AND PASSWORD=HASHBYTES('SHA1',@PASSWORD)";
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        SqlParameter p1 = new SqlParameter("@MATRICOLA", SqlDbType.VarChar);
        p1.Value = username;
        cmd.Parameters.Add(p1);

        SqlParameter p2 = new SqlParameter("@PASSWORD", SqlDbType.VarChar);
        p2.Value = password;
        cmd.Parameters.Add(p2);

        SqlDataReader reader = cmd.ExecuteReader();
        return DbManager.DynamicOutput(reader);
      }
      finally
      {
        if (cmd?.Connection?.State != ConnectionState.Closed)
          cmd.Connection.Close();
      }
    }

    public int Create(Utente pf)
    {
      string sql = @"INSERT INTO PERS_UTENTE (MATRICOLA, NOME,COGNOME,DIREZIONE,RUOLO, PASSWORD) 
                VALUES(@MATRICOLA, @NOME,@COGNOME,@DIREZIONE,@RUOLO, HASHBYTES('SHA1',@PASSWORD))";
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        SqlParameter p = new SqlParameter("@MATRICOLA", SqlDbType.VarChar);
        p.Value = pf.Matricola;
        cmd.Parameters.Add(p);
        SqlParameter p1 = new SqlParameter("@NOME", SqlDbType.VarChar);
        p1.Value = pf.Nome;
        cmd.Parameters.Add(p1);
        SqlParameter p2 = new SqlParameter("@COGNOME", SqlDbType.VarChar);
        p2.Value = pf.Cognome;
        cmd.Parameters.Add(p2);
        SqlParameter p3 = new SqlParameter("@DIREZIONE", SqlDbType.VarChar);
        p3.Value = pf.Direzione;
        cmd.Parameters.Add(p3);
        SqlParameter p4 = new SqlParameter("@RUOLO", SqlDbType.Int);
        p4.Value = pf.Ruolo;
        cmd.Parameters.Add(p4);
        SqlParameter p5 = new SqlParameter("@PASSWORD", SqlDbType.VarChar);
        p5.Value = pf.Password;
        cmd.Parameters.Add(p5);

        int numResult = cmd.ExecuteNonQuery();
        return numResult;
      }
      finally
      {
        if (cmd?.Connection?.State != ConnectionState.Closed)
          cmd.Connection.Close();
      }
    }

    public int Update(Utente pf)
    {
      string sql = @"UPDATE PERS_UTENTE SET MATRICOLA=@MATRICOLA, NOME=@NOME,COGNOME=@COGNOME,DIREZIONE=@DIREZIONE,RUOLO=@RUOLO, ATTIVO=@ATTIVO";
      if (pf.Password != null)
      {
        sql += ", PASSWORD=HASHBYTES('SHA1',@PASSWORD)";
      }
      sql += " WHERE ID = @ID";
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        SqlParameter p0 = new SqlParameter("@id", SqlDbType.Int);
        p0.Value = pf.Id;
        cmd.Parameters.Add(p0);
        SqlParameter p = new SqlParameter("@MATRICOLA", SqlDbType.VarChar);
        p.Value = pf.Matricola;
        cmd.Parameters.Add(p);
        SqlParameter p1 = new SqlParameter("@NOME", SqlDbType.VarChar);
        p1.Value = pf.Nome;
        cmd.Parameters.Add(p1);
        SqlParameter p2 = new SqlParameter("@COGNOME", SqlDbType.VarChar);
        p2.Value = pf.Cognome;
        cmd.Parameters.Add(p2);
        SqlParameter p3 = new SqlParameter("@DIREZIONE", SqlDbType.VarChar);
        p3.Value = pf.Direzione;
        cmd.Parameters.Add(p3);
        SqlParameter p4 = new SqlParameter("@RUOLO", SqlDbType.Int);
        p4.Value = pf.Ruolo;
        cmd.Parameters.Add(p4);
        SqlParameter p5 = new SqlParameter("@ATTIVO", SqlDbType.Int);
        p5.Value = pf.Attivo;
        cmd.Parameters.Add(p5);
        if (pf.Password != null)
        {
          SqlParameter p6 = new SqlParameter("@PASSWORD", SqlDbType.VarChar);
          p6.Value = pf.Password;
          cmd.Parameters.Add(p6);
        }

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
      // preferisco non implementare il delete, ma lo lascio per eventuali sviluppi futuri
      return 0;
      /*
      string sql = "DELETE FROM PERS_UTENTE WHERE ID = @ID";
      SqlCommand cmd = DbManager.CreateCommand(sql);

      SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
      p1.Value = id;
      cmd.Parameters.Add(p1);

      int numResult = cmd.ExecuteNonQuery();
      return numResult;*/

    }

  }
}