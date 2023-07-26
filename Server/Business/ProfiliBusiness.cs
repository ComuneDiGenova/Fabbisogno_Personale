// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;
using FabbPers.DataAccess;

namespace FabbPers.Business
{
    public class ProfiliBusiness: IProfiliBusiness
    {
        public List<dynamic> Get() {
            ProfiliDAO cd = new ProfiliDAO();
            return cd.Read();
        }

         public int Post(Profilo lov) {
             ProfiliDAO cd = new ProfiliDAO();
             return cd.Create(lov);
         }

           public int Put(Profilo lov) {
             //TODO: validazione
             ProfiliDAO cd = new ProfiliDAO();
             
             return cd.Update(lov);
         }

            public int Delete(int id) {
             ProfiliDAO cd = new ProfiliDAO();
             return cd.Delete(id);
         }
    }
}