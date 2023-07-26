// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;
using FabbPers.DataAccess;

namespace FabbPers.Business
{
    public class MansioniBusiness: IMansioniBusiness
    {
        public List<dynamic> Get(string query) {
            MansioniDAO cd = new MansioniDAO();
            return cd.Read(query);
        }

         public int Post(ListOfValue lov) {
             MansioniDAO cd = new MansioniDAO();
             string nome = lov.Nome;
             return cd.Create(nome);
         }

           public int Put(ListOfValue  lov) {
             //TODO: validazione
             MansioniDAO cd = new MansioniDAO();
             
             return cd.Update(lov);
         }

            public int Delete(int id) {
             MansioniDAO cd = new MansioniDAO();
             return cd.Delete(id);
         }
    }
}