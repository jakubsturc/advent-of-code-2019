using System;
using System.Diagnostics.CodeAnalysis;

namespace JakubSturc.AdventOfCode2019.Day22
{
    public abstract class Shuffle
    {
        private Shuffle() { }

        public static Shuffle ParseFrom(string line)
        {

            if (line.StartsWith("deal into new stack"))
            {
                return DealIntoNewStack.Instance;
            }

            if (line.StartsWith("deal with increment"))
            {
                int n = int.Parse(line.AsSpan().Slice(20));
                return new DealWithIncrement(n);
            }

            if (line.StartsWith("cut"))
            {
                int n = int.Parse(line.AsSpan().Slice(4));
                return new Cut(n);
            }

            throw new NotSupportedException();
        }

        public abstract void ApplyTo(ref int[] deck);

        public static void Init(int len, [AllowNull]ref int[] cache)
        {
            if (cache == null || cache.Length != len)
            {
                cache = new int[len];
            }
        }

        public static void Swap(ref int[] a, ref int[] b)
        {
            var tmp = a;
            a = b;
            b = tmp;
        }

        public class DealIntoNewStack : Shuffle
        {
            internal static DealIntoNewStack Instance { get; } = new DealIntoNewStack();

            public override void ApplyTo(ref int[] deck)
            {
                Array.Reverse<int>(deck);
            }
        }

        public class DealWithIncrement : Shuffle
        {
            private int[]? _cache; // to prevent allocations
            public int N { get; }

            public DealWithIncrement(int n)
            {
                N = n;
            }

            public override void ApplyTo(ref int[] deck)
            {
                var len = deck.Length;

                Shuffle.Init(len, ref _cache);

                for (var i = 0; i < len; i++)
                {
                    _cache[(i * N) % len] = deck[i];
                }

                Shuffle.Swap(ref deck, ref _cache);
            }
        }

        public class Cut : Shuffle
        {
            private int[]? _cache; // to prevent allocations

            public int N { get; }

            public Cut(int n)
            {
                N = n;
            }

            public override void ApplyTo(ref int[] deck)
            {
                var len = deck.Length;
                var n = N >= 0 ? N : len + N;

                Shuffle.Init(len, ref _cache);

                Array.Copy(deck, 0, _cache, len - n, n);
                Array.Copy(deck, n, _cache, 0, len - n);

                Shuffle.Swap(ref deck, ref _cache);

            }
        }
    }
}
