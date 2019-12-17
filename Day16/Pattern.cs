using System.Collections.Generic;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day16
{
    public static class Pattern
    {
        private static readonly int[] _inner = new int[] { 0, 1, 0, -1 };

        public static int Get(int step, int pos)
        {
            var idx = ((pos + 1) / step) % 4;
            return _inner[idx];
        }

        public static IEnumerable<int> Get(int step, int from, int count)
        { 
            for (int i = 0; i < count; i++)
            {
                yield return Get(step, from + i);
            }
        }
    }
}
