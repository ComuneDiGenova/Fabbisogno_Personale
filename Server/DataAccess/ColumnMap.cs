// SPDX-License-Identifier: EUPL-1.2
// SPDX-License-Identifier: BSD-3-Clause
namespace FabbPers.DataAccess
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    public class ColumnMap
    {
        private Dictionary<string,int> cm;
        public ColumnMap(SqlDataReader dr)
        {
            Dictionary<string,int> dic = new Dictionary<string, int>();
            for (int i=0;i<dr.FieldCount;i++) {
                dic[dr.GetName(i)]=i;
            }
            this.cm = dic;
        }

        public int this[string index] {
            get {
                return cm[index];
            }
        }

        public bool check(string key) {
            return cm.ContainsKey(key);
        }
    }
}