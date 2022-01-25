using System;
using System.Collections.Generic;
using System.Text;

namespace CarpoolApp.Models
{
    class ActivityCode
    {
        private const int HASHCODE = 9973;

        public static string CreateGroupCode(int ID)
        {
            return "C" + $"{HASHCODE / ID}" + "P" + $" {HASHCODE % ID}";
        }

        public static int CodeToGroupID(string code)
        {
            if (string.IsNullOrEmpty(code))
                return -1;

            int cPos = code.IndexOf('C');
            int pPos = code.IndexOf('P');

            if (cPos != 0 || pPos <= 2)
                return 0;

            string param1 = code.Substring(1, pPos - 1);
            string param2 = code.Substring(pPos + 1);

            int p1 = 0, p2 = 0;
            if (!int.TryParse(param1, out p1) ||
                !int.TryParse(param2, out p2))
                return 0;

            return (HASHCODE - p2) / p1;
        }
    }
}
