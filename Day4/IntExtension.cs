using System;

namespace JakubSturc.AdventOfCode2019.Day4
{
    public static class IntExtension
    {
        public static int[] Split(this int num, int cnt)
        {
            return SplitInto(num, new int[cnt]);
        }

        public static int[] SplitInto(this int num, int[] res)
        {
            for (int i = 1; i <= res.Length; i++)
            {
                num = Math.DivRem(num, 10, out res[^i]);
            }

            return res;
        }
    }
}
