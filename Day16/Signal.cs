using System;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day16
{
    public class Signal
    {
        public int Length { get => Digits.Length; }

        public int[] Digits { get; private set; }

        private Signal(int[] digits)
        {
            Digits = digits;
        }

        public static Signal From(string str)
        {
            return new Signal(str.Select(Parse).ToArray());

            static int Parse(char c) => c - '0';
        }

        public Signal Advance(int cnt, int offset = 0)
        {
            var len = Length;
            var cur = Digits;
            var next = new int[len];

            for (int i = 0; i < cnt; i++)
            {
                // we compute the first half normally 
                for (int j = offset; j < len / 2; j++)
                {
                    long res = 0;

                    for (int k = offset; k < len; k++)
                    {
                        res += (cur[k] * Pattern.Get(j + 1, k));
                    }

                    res = res >= 0 ? res : -res;
                    res %= 10;
                    next[j] = (int)res;
                }


                // we compute the the second half from the back  
                for (int acc = 0, j = len - 1; j >= Math.Max(len/2, offset); j--)
                {
                    acc += cur[j];
                    acc %= 10;
                    next[j] = acc;
                }

                cur = next;
            }

            Digits = next;

            return this;
        }

        public Signal Repeat10k()
        {
            var _10k = 10_000;
            var len = Digits.Length;
            var result = new int[len * _10k];
            
            for (int i = 0; i < _10k; i++)
            {
                Digits.CopyTo(result, i * len);
            }

            return new Signal(result);
        }
    }
}
