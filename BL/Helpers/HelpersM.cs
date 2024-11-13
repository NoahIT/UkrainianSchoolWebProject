using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BL.Helpers
{
    public class HelpersM
    {
        public static TransactionScope CreateTransactionScope(int seconds = 60)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, seconds),
                TransactionScopeAsyncFlowOption.Enabled
            );
        }

        public static Guid? StringToGuidDef(string str)
        {
            Guid value;
            if (Guid.TryParse(str, out value))
                return value;
            return null;
        }

        public static int? StringToIntDef(string str, int? def)
        {
            int value;
            if (int.TryParse(str, out value))
                return value;
            return def;
        }

        public static List<int>? StringToListInt(string s)
        {
            List<int> subjectsIdList = new List<int>();

            if (!string.IsNullOrEmpty(s))
            {
                subjectsIdList = s
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id =>
                    {
                        if (int.TryParse(id, out int parsedId))
                        {
                            return parsedId;
                        }
                        return -1;
                    })
                    .Where(id => id != -1)
                    .ToList();
            }

            return subjectsIdList;
        }
    }
}
