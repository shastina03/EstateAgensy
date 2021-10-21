using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstateAgency
{
    public static class SearchEngine
    {
        public static int EditDistance(string s, string t)
        {
            if (s == null || t == null)
                return 0;
            int m = s.Length, n = t.Length;
            int[,] ed = new int[m, n];

            for (int i = 0; i < m; ++i)
            {
                ed[i, 0] = i + 1;
            }

            for (int j = 0; j < n; ++j)
            {
                ed[0, j] = j + 1;
            }

            for (int j = 1; j < n; ++j)
            {
                for (int i = 1; i < m; ++i)
                {
                    if (s[i] == t[j])
                    {
                        // Операция не требуется
                        ed[i, j] = ed[i - 1, j - 1];
                    }
                    else
                    {
                        // Минимум между удалением, вставкой и заменой
                        ed[i, j] = Math.Min(ed[i - 1, j] + 1,
                            Math.Min(ed[i, j - 1] + 1, ed[i - 1, j - 1] + 1));
                    }
                }
            }

            return ed[m - 1, n - 1];
        }
    }
}
