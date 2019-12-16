using System;

namespace JakubSturc.AdventOfCode2019.Day14
{
    public struct ReactionItem
    { 
        public Chemical Chemical { get; }
        public long Quantity { get; }

        public ReactionItem(Chemical chemical, long quantity)
        {
            Chemical = chemical;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Quantity} {Chemical.Name}";
        }

        public static ReactionItem operator *(long x, ReactionItem ri)
            => new ReactionItem(ri.Chemical, x * ri.Quantity);

        public static ReactionItem operator +(ReactionItem a, ReactionItem b)
        {
            if (a.Chemical != b.Chemical) throw new ArgumentException();

            return new ReactionItem(a.Chemical, a.Quantity + b.Quantity);
        }

        public static ReactionItem Parse(ReadOnlySpan<char> str)
        {
            var idx = str.IndexOf(' ');
            var quantity = long.Parse(str.Slice(0, idx));
            var name = new string(str.Slice(idx + 1));
            var chemical = Chemical.Create(name);
            return new ReactionItem(chemical, quantity);
        }
    }

}
