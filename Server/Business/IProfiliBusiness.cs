// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;

namespace FabbPers.Business
{
    public interface IProfiliBusiness
    {
         public List<dynamic> Get();
         public int Post(Profilo p);
         public int Put(Profilo p);
         public int Delete(int id);
    }
}