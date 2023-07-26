// SPDX-License-Identifier: EUPL-1.2
using System;
using System.Collections.Generic;
using FabbPers.Authentication;

namespace FabbPers
{
    /*  public class Cimitero
      {
          public int CimId { get; set; }

          public string Nome { get; set; }

          //public static List<Cimitero> Map()

      }*/

    public enum STATO {
        IN_CORSO,CONFERMATO,ANNULLATO, ANALIZZATO
    }

    public enum RUOLO {
        NOT_FOUND=0,
        ADMIN= 1,
        USER=2

    }

    public class ListOfValue 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class Profilo {
        
        public int Id { get; set; }
        public string Categoria {get;set;}
        public string Nome { get; set; }
    }

        public class StateChange {
         public int Id { get; set; }
        public int NuovoStato { get; set; }
    }

    public class Utente : UserModel {
        public bool Attivo_b {get;set;}

        public int Id {get;set;}

        public string Password {get;set;}
    }

    public class MultiStato {
        public List<int> Ids {get;set;}
        
    }
    public class Fabbisogno
    {
        public int ID { get; set; }
        public string DIREZIONE { get; set; }
        public int ANNO { get; set; }
        public string CATEGORIA { get; set; }
        public int PROFILO_ID { get; set; }
        public int MANSIONE_ID { get; set; }
        public string PROFILO_FORMATIVO { get; set; }
        public int MOTIVO_RICHIESTA_ID { get; set; }
        public int UNITA { get; set; }
        public int STATO_ID { get; set; }
        public DateTime DATA_INS { get; set; }
        public string UTENTE_INS { get; set; }
        public DateTime? DATA_CONF { get; set; }
        public string UTENTE_CONF { get; set; }
        public DateTime? DATA_ANN { get; set; }
        public string UTENTE_ANN { get; set; }
        
        public DateTime? DATA_ANALIZZATO { get; set; }
        public string UTENTE_ANALIZZATO { get; set; }
        public string ANNOTAZIONI { get; set; }
    }
}
