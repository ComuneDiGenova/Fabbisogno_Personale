// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;
using FabbPers.DataAccess;

namespace FabbPers.Business
{
    public class MotiviRichiestaBusiness: IMotiviRichiestaBusiness
    {
        public List<dynamic> Get(string query) {
            MotiviRichiestaDAO cd = new MotiviRichiestaDAO();
            return cd.Read(query);
        }

         public int Post(ListOfValue lov) {
             MotiviRichiestaDAO cd = new MotiviRichiestaDAO();
             string nome = lov.Nome;
             return cd.Create(nome);
         }

           public int Put(ListOfValue  lov) {
             //TODO: validazione
             MotiviRichiestaDAO cd = new MotiviRichiestaDAO();
             
             return cd.Update(lov);
         }

            public int Delete(int id) {
             MotiviRichiestaDAO cd = new MotiviRichiestaDAO();
             return cd.Delete(id);
         }
    }
}