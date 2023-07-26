// SPDX-License-Identifier: EUPL-1.2

using System.Collections.Generic;
using System.Security.Principal;
using FabbPers.Authentication;

namespace FabbPers.Business
{
    public interface IFabbisogniBusiness
    {
         public UserModel currentUser {get;set;}
         public List<dynamic> Get();
         public int Post(Fabbisogno lov);
         public int Put(Fabbisogno lov);
         public int Delete(int id);
         public int CambiaStato(StateChange sc);

         public int ConfermaMultipla(MultiStato ids);
    }
}