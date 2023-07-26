// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;
using FabbPers.Authentication;

namespace FabbPers.Business
{
    public interface IUtentiBusiness
    {
         public List<dynamic> Get();
         public int Post(Utente p);
         public int Put(Utente p);
         public int Delete(int id);
    }
}