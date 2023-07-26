// SPDX-License-Identifier: EUPL-1.2
using System.Collections.Generic;

namespace FabbPers.Business
{
    public interface IMotiviRichiestaBusiness
    {
         public List<dynamic> Get(string query);
         public int Post(ListOfValue  lov);
         public int Put(ListOfValue  lov);
         public int Delete(int id);
    }
}