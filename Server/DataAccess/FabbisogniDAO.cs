// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using FabbPers.Authentication;

namespace FabbPers.DataAccess
{
  public class FabbisogniDAO
  {
    private static string baseQuery = @"SELECT F.ID,F.DIREZIONE,ANNO,
            F.CATEGORIA,
            PROFILO_ID, pp.NOME as PROFILO,
            MANSIONE_ID, pm.NOME as MANSIONE,
            pmr.NOME as MOTIVO_RICHIESTA,
            PROFILO_FORMATIVO,
            MOTIVO_RICHIESTA_ID,
            UNITA,STATO_ID,
            DATA_INS,UTENTE_INS,pu.NOME + ' '+ pu.COGNOME as FULL_UTENTE_INS,
            DATA_CONF,UTENTE_CONF,pu2.NOME + ' '+ pu2.COGNOME as FULL_UTENTE_CONF,
            DATA_ANN,UTENTE_ANN,pu3.NOME + ' '+ pu3.COGNOME as FULL_UTENTE_ANN,
            DATA_ANALIZZATO,UTENTE_ANALIZZATO, pu4.NOME + ' '+ pu4.COGNOME as FULL_UTENTE_ANALIZZATO,
            ANNOTAZIONI
            FROM 
            PERS_MANSIONE pm,
            PERS_PROFILO pp,
            PERS_MOTIVO_RICHIESTA pmr,
            PERS_FABBISOGNO F
            LEFT OUTER JOIN PERS_UTENTE pu on F.UTENTE_INS = pu.MATRICOLA 
            LEFT OUTER JOIN PERS_UTENTE pu2 on F.UTENTE_CONF = pu2.MATRICOLA 
            LEFT OUTER JOIN PERS_UTENTE pu3 on F.UTENTE_ANN = pu3.MATRICOLA 
            LEFT OUTER JOIN PERS_UTENTE pu4 on F.UTENTE_ANALIZZATO = pu4.MATRICOLA
            where F.MANSIONE_ID = pm.ID 
            and pp.ID  = F.PROFILO_ID and pmr.ID = F.MOTIVO_RICHIESTA_ID";

    public List<dynamic> Read(string direzione)
    {

      string sql = baseQuery;
      SqlCommand cmd = new SqlCommand();
      string sqlOrder = " ORDER BY DATA_INS DESC";

      try
      {
        if (direzione != null)
        {
          SqlParameter g = new SqlParameter("@DIREZIONE", SqlDbType.VarChar);
          sql += " and F.DIREZIONE=@DIREZIONE" + sqlOrder;
          g.Value = direzione;
          cmd = DbManager.CreateCommand(sql);
          cmd.Parameters.Add(g);
        }
        else
        {
          sql += sqlOrder;
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


    public List<dynamic> ReadById(int id)
    {

      string sql = baseQuery;
      SqlCommand cmd = new SqlCommand();

      try
      {
        SqlParameter g = new SqlParameter("@ID", SqlDbType.Int);
        sql += " and F.ID=@ID";
        g.Value = id;
        cmd = DbManager.CreateCommand(sql);
        cmd.Parameters.Add(g);


        SqlDataReader reader = cmd.ExecuteReader();
        return DbManager.DynamicOutput(reader);
      }
      finally
      {
        if (cmd?.Connection?.State != ConnectionState.Closed)
          cmd.Connection.Close();
      }
    }

    public int Create(Fabbisogno fabb)
    {
      string sql = @"INSERT INTO PERS_FABBISOGNO 
            (DIREZIONE,ANNO,CATEGORIA,PROFILO_ID,MANSIONE_ID,PROFILO_FORMATIVO,MOTIVO_RICHIESTA_ID,UNITA,STATO_ID,DATA_INS,UTENTE_INS,ANNOTAZIONI) 
                VALUES(@DIREZIONE,
                       @ANNO,
                       @CATEGORIA,
                       @PROFILO_ID,
                       @MANSIONE_ID,
                       @PROFILO_FORMATIVO,
                       @MOTIVO_RICHIESTA,
                       @UNITA,
                       @STATO_ID,
                       @DATA_INS,
                       @UTENTE_INS,
                       @ANNOTAZIONI)";
      //@DATA_CONF,
      //@UTENTE_CONF,
      //@DATA_ANN,
      //@UTENTE_ANN)";
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        SqlParameter p1 = new SqlParameter("@DIREZIONE", SqlDbType.VarChar);
        p1.Value = fabb.DIREZIONE;
        cmd.Parameters.Add(p1);
        SqlParameter p2 = new SqlParameter("@ANNO", SqlDbType.Int);
        p2.Value = fabb.ANNO;
        cmd.Parameters.Add(p2);
        SqlParameter p3 = new SqlParameter("@CATEGORIA", SqlDbType.VarChar);
        p3.Value = fabb.CATEGORIA;
        cmd.Parameters.Add(p3);
        SqlParameter p4 = new SqlParameter("@PROFILO_ID", SqlDbType.Int);
        p4.Value = fabb.PROFILO_ID;
        cmd.Parameters.Add(p4);
        SqlParameter p5 = new SqlParameter("@MANSIONE_ID", SqlDbType.Int);
        p5.Value = fabb.MANSIONE_ID;
        cmd.Parameters.Add(p5);
        SqlParameter p6 = new SqlParameter("@PROFILO_FORMATIVO", SqlDbType.VarChar);
        p6.Value = fabb.PROFILO_FORMATIVO;
        cmd.Parameters.Add(p6);
        SqlParameter p7 = new SqlParameter("@MOTIVO_RICHIESTA", SqlDbType.Int);
        p7.Value = fabb.MOTIVO_RICHIESTA_ID;
        cmd.Parameters.Add(p7);
        SqlParameter p8 = new SqlParameter("@UNITA", SqlDbType.Int);
        p8.Value = fabb.UNITA;
        cmd.Parameters.Add(p8);
        SqlParameter p9 = new SqlParameter("@STATO_ID", SqlDbType.Int);
        p9.Value = fabb.STATO_ID;
        cmd.Parameters.Add(p9);
        SqlParameter p10 = new SqlParameter("@DATA_INS", SqlDbType.DateTime);
        p10.Value = System.DateTime.UtcNow;
        cmd.Parameters.Add(p10);
        SqlParameter p11 = new SqlParameter("@UTENTE_INS", SqlDbType.VarChar);
        p11.Value = fabb.UTENTE_INS;
        cmd.Parameters.Add(p11);
        SqlParameter p12 = new SqlParameter("@ANNOTAZIONI", SqlDbType.VarChar);
        p12.Value = fabb.ANNOTAZIONI;
        cmd.Parameters.Add(p12);
        /*   SqlParameter p12 = new SqlParameter("@DATA_CONF", SqlDbType.DateTime);
            p12.Value = null;//(fabb.DATA_CONF;
            cmd.Parameters.Add(p12);
            SqlParameter p13 = new SqlParameter("@UTENTE_CONF", SqlDbType.VarChar);
            p13.Value = fabb.UTENTE_CONF;
            cmd.Parameters.Add(p13);
            SqlParameter p14 = new SqlParameter("@DATA_ANN", SqlDbType.DateTime);
            p14.Value = null;//fabb.DATA_ANN;
            cmd.Parameters.Add(p14);
            SqlParameter p15 = new SqlParameter("@UTENTE_ANN", SqlDbType.VarChar);
            p15.Value = fabb.UTENTE_CONF;
            cmd.Parameters.Add(p15);*/


        int numResult = cmd.ExecuteNonQuery();
        return numResult;
      }
      finally
      {
        if (cmd?.Connection?.State != ConnectionState.Closed)
          cmd.Connection.Close();
      }
    }

    public int UpdateStato(StateChange sc, UserModel currentUser)
    {
      string log_sql = "";
      if (sc.NuovoStato == (int)STATO.CONFERMATO)
      {
        log_sql = ", DATA_CONF=@DATA, UTENTE_CONF=@UTENTE ";
      }
      else if (sc.NuovoStato == (int)STATO.ANNULLATO)
      {
        log_sql = ", DATA_ANN=@DATA, UTENTE_ANN=@UTENTE ";
      }
      else if (sc.NuovoStato == (int)STATO.ANALIZZATO)
      {
        log_sql = ", DATA_ANALIZZATO=@DATA, UTENTE_ANALIZZATO=@UTENTE ";
      }
      else return 0; // should not happen
      string sql = @"UPDATE PERS_FABBISOGNO SET 
                  STATO_ID=@STATO_ID ";
      sql += log_sql;
      sql += " WHERE ID = @ID";
      SqlParameter p0 = new SqlParameter("@ID", SqlDbType.VarChar);
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        p0.Value = sc.Id;
        cmd.Parameters.Add(p0);
        SqlParameter p1 = new SqlParameter("@STATO_ID", SqlDbType.Int);
        p1.Value = sc.NuovoStato;
        cmd.Parameters.Add(p1);
        SqlParameter p10 = new SqlParameter("@DATA", SqlDbType.DateTime);
        p10.Value = System.DateTime.UtcNow;
        cmd.Parameters.Add(p10);
        SqlParameter p11 = new SqlParameter("@UTENTE", SqlDbType.VarChar);
        p11.Value = currentUser.Matricola;
        cmd.Parameters.Add(p11);
        return cmd.ExecuteNonQuery();
      }
      finally
      {
        if (cmd?.Connection?.State != ConnectionState.Closed)
          cmd.Connection.Close();
      }
    }

    public int Update(Fabbisogno fabb)
    {
      string sql = @"UPDATE PERS_FABBISOGNO SET 
                DIREZIONE=@DIREZIONE,
                ANNO=  @ANNO,
                CATEGORIA= @CATEGORIA,
                PROFILO_ID=@PROFILO_ID,
                MANSIONE_ID=@MANSIONE_ID,
                PROFILO_FORMATIVO=@PROFILO_FORMATIVO,
                MOTIVO_RICHIESTA_ID=@MOTIVO_RICHIESTA,
                UNITA=@UNITA,
                ANNOTAZIONI=@ANNOTAZIONI
                WHERE ID = @ID";
      /*STATO_ID=@STATO_ID,
      DATA_INS=@DATA_INS,
      UTENTE_INS=@UTENTE_INS,
      DATA_CONF=@DATA_CONF,
      UTENTE_CONF=@UTENTE_CONF,
      DATA_ANN=@DATA_ANN,
      UTENTE_ANN=@UTENTE_ANN 
      WHERE ID = @ID";*/
      SqlCommand cmd = DbManager.CreateCommand(sql);

      try
      {
        SqlParameter p1 = new SqlParameter("@DIREZIONE", SqlDbType.VarChar);
        p1.Value = fabb.DIREZIONE;
        cmd.Parameters.Add(p1);
        SqlParameter p2 = new SqlParameter("@ANNO", SqlDbType.Int);
        p2.Value = fabb.ANNO;
        cmd.Parameters.Add(p2);
        SqlParameter p3 = new SqlParameter("@CATEGORIA", SqlDbType.VarChar);
        p3.Value = fabb.CATEGORIA;
        cmd.Parameters.Add(p3);
        SqlParameter p4 = new SqlParameter("@PROFILO_ID", SqlDbType.Int);
        p4.Value = fabb.PROFILO_ID;
        cmd.Parameters.Add(p4);
        SqlParameter p5 = new SqlParameter("@MANSIONE_ID", SqlDbType.Int);
        p5.Value = fabb.MANSIONE_ID;
        cmd.Parameters.Add(p5);
        SqlParameter p6 = new SqlParameter("@PROFILO_FORMATIVO", SqlDbType.VarChar);
        p6.Value = fabb.PROFILO_FORMATIVO;
        cmd.Parameters.Add(p6);
        SqlParameter p7 = new SqlParameter("@MOTIVO_RICHIESTA", SqlDbType.Int);
        p7.Value = fabb.MOTIVO_RICHIESTA_ID;
        cmd.Parameters.Add(p7);
        SqlParameter p8 = new SqlParameter("@UNITA", SqlDbType.Int);
        p8.Value = fabb.UNITA;
        cmd.Parameters.Add(p8);
        SqlParameter p9 = new SqlParameter("@ANNOTAZIONI", SqlDbType.VarChar);
        p9.Value = fabb.ANNOTAZIONI;
        cmd.Parameters.Add(p9);
        /*   SqlParameter p9 = new SqlParameter("@STATO_ID", SqlDbType.Int);
           p9.Value = fabb.STATO_ID;
           cmd.Parameters.Add(p9);
           SqlParameter p10 = new SqlParameter("@DATA_INS", SqlDbType.DateTime);
           p10.Value = fabb.DATA_INS;
           cmd.Parameters.Add(p10);
           SqlParameter p11 = new SqlParameter("@UTENTE_INS", SqlDbType.VarChar);
           p11.Value = fabb.UTENTE_INS;
           cmd.Parameters.Add(p11);
           SqlParameter p12 = new SqlParameter("@DATA_CONF", SqlDbType.DateTime);
           p12.Value = fabb.DATA_CONF;
           cmd.Parameters.Add(p12);
           SqlParameter p13 = new SqlParameter("@UTENTE_CONF", SqlDbType.VarChar);
           p13.Value = fabb.UTENTE_CONF;
           cmd.Parameters.Add(p13);
           SqlParameter p14 = new SqlParameter("@DATA_ANN", SqlDbType.DateTime);
           p14.Value = fabb.DATA_ANN;
           cmd.Parameters.Add(p14);
           SqlParameter p15 = new SqlParameter("@UTENTE_ANN", SqlDbType.VarChar);
           p15.Value = fabb.UTENTE_CONF;
           cmd.Parameters.Add(p15);*/
        SqlParameter p0 = new SqlParameter("@ID", SqlDbType.VarChar);
        p0.Value = fabb.ID;
        cmd.Parameters.Add(p0);

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
      string sql = "DELETE FROM PERS_FABBISOGNO WHERE ID = @ID";
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