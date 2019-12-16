using System;
using System.Collections.Generic;

namespace JakubSturc.AdventOfCode2019.Day14
{
    public class Reaction
    {
        private const string _ruleSeparator = " => ";
        public ReactionItem Output { get; }
        public IEnumerable<ReactionItem> Input { get; }

        public Reaction(ReactionItem output, IEnumerable<ReactionItem> input)
        {
            Output = output;
            Input = input;
        }

        public override string ToString()
        {
            return $"{string.Join(", ", Input)}{_ruleSeparator}{Output}";
        }

        public static Reaction Parse(string str)
        {
            // quick and dirty approach to parsing
            var span = str.AsSpan();
            var idx = span.IndexOf(_ruleSeparator);
            var input = ParseInput(span.Slice(0, idx));
            var output = ParseOutput(span.Slice(idx + _ruleSeparator.Length));
            return new Reaction(output, input);

            static List<ReactionItem> ParseInput(ReadOnlySpan<char> str)
            {
                var result = new List<ReactionItem>();
                while (true)
                {
                    var idx = str.IndexOf(',');
                    if (idx == -1)
                    {
                        result.Add(ReactionItem.Parse(str));
                        return result;
                    }
                    else
                    {
                        result.Add(ReactionItem.Parse(str.Slice(0, idx)));
                        str = str.Slice(idx + 2); // +1 for space after ','
                    }
                }
            }

            static ReactionItem ParseOutput(ReadOnlySpan<char> str)
            {
                return ReactionItem.Parse(str);
            }
        }
    }
}
