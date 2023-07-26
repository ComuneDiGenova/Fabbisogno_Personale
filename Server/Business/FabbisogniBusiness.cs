// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;
using FabbPers.DataAccess;
using FabbPers.Authentication;

namespace FabbPers.Business
{
    public class FabbisogniBusiness: IFabbisogniBusiness
    {
        public UserModel currentUser {get;set;}
        public List<dynamic> Get() {
            // se l'utente corrente non è admin filtro sulla direzione dell'utente
            string direzione = currentUser.Ruolo==(int)RUOLO.USER?currentUser.Direzione:null; 
            
            FabbisogniDAO cd = new FabbisogniDAO();
            return cd.Read(direzione);
        }

         public int Post(Fabbisogno fabb) {
             FabbisogniDAO cd = new FabbisogniDAO();
             return cd.Create(fabb);
         }

           public int Put(Fabbisogno fabb) {
             //TODO: validazione
             FabbisogniDAO cd = new FabbisogniDAO();
             
             return cd.Update(fabb);
         }

            public int Delete(int id) {
             FabbisogniDAO cd = new FabbisogniDAO();
             return cd.Delete(id);
         }

        public int CambiaStato(StateChange sc)
        {
            // State model attuale. Cambi stato concessi:
            // In corso -> Confermato -> tutti
            // Confermato -> Annullato -> 
            // Confermato -> Analizzato
            FabbisogniDAO cd = new FabbisogniDAO();
            // individua il record
            var x = cd.ReadById(sc.Id);
            if (x.Count ==0)
             return -1;
            // è approvabile solo se l'utente è lo stesso --> TODO dev'essere la direzione!

            else {
                dynamic fb = x[0];
                
                int old_status_id = fb.STATO_ID;
                // nessuna transizione verso "In corso"
                if (sc.NuovoStato==(int)STATO.IN_CORSO) 
                    return -1;
                // Confermato solo da In corso
                if (sc.NuovoStato == (int)STATO.CONFERMATO)
                {
                    if (old_status_id == (int)STATO.IN_CORSO)
                    {
                        return cd.UpdateStato(sc, currentUser);
                    }
                    else return -1;
                } 
                // Annullato solo da Confermato
                if (sc.NuovoStato == (int)STATO.ANNULLATO)
                {
                    if (old_status_id == (int)STATO.CONFERMATO)
                    {
                        return cd.UpdateStato(sc, currentUser);
                    
                    }
                    else return -1;
                } 
                // Analizzato solo da Confermato 
                // e da direzione
                if (sc.NuovoStato == (int)STATO.ANALIZZATO)
                {
                    if (old_status_id == (int)STATO.CONFERMATO && currentUser.Ruolo==(int)RUOLO.ADMIN)
                    {
                        return cd.UpdateStato(sc, currentUser);
                    
                    }
                    else return -1;
                } 
                
                // e se mi son perso qualcosa che non va
                return -1;
            }
        }

        public int ConfermaMultipla(MultiStato ids) {
            if (ids==null)
                return -1;
            int counter = 0;
            FabbisogniDAO cd = new FabbisogniDAO();
            foreach (int id in ids.Ids) {
                 var x = cd.ReadById(id);
                 if (x.Count==0)
                    continue;
                dynamic record = x[0];
                if (record.STATO_ID != (int)STATO.IN_CORSO)
                    continue;
                StateChange sc = new StateChange () {
                    Id= id,
                    NuovoStato=(int)STATO.CONFERMATO
                };
                counter += cd.UpdateStato(sc,currentUser);
            }
            return counter;
        }
    }
}