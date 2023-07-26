// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;
using FabbPers.DataAccess;
using FabbPers.Authentication;

namespace FabbPers.Business
{
    public class UtentiBusiness: IUtentiBusiness
    {
        public List<dynamic> Get() {
            UtentiDAO cd = new UtentiDAO();
            return cd.Read(null);
        }

         public int Post(Utente user) {
             UtentiDAO cd = new UtentiDAO();
             List<dynamic> users = cd.Read(user.Matricola);
             if (users.Count>0) {
                 return -1;
             }
             return cd.Create(user);
         }

           public int Put(Utente lov) {
             UtentiDAO cd = new UtentiDAO();
             return cd.Update(lov);
         }

            public int Delete(int id) {
             UtentiDAO cd = new UtentiDAO();
             return cd.Delete(id);
         }
    }
}