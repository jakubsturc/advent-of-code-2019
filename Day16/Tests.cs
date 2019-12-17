using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2019.Day16
{
    public partial class Tests
    {
        [Theory]
        [InlineData(1, new int[] { 1, 0, -1, 0, 1, 0, -1, 0, 1, 0 })]
        [InlineData(2, new int[] { 0, 1, 1, 0, 0, -1, -1, 0, 0, 1 })]
        [InlineData(3, new int[] { 0, 0, 1, 1, 1, 0, 0, 0, -1, -1 })]
        public void PatternTest(int size, int[] expected)
        {
            Assert.Equal(expected, Pattern.Get(size, from: 0, count: 10));
        }

        [Fact]
        public void Part1_Sample1()
        {
            var signal = Signal.From("12345678");
            var phase1 = signal.Advance(1);
            Assert.Equal("48226158", AsString(phase1.Digits));
            var phase2 = phase1.Advance(1);
            Assert.Equal("34040438", AsString(phase2.Digits));
            var phase3 = phase2.Advance(1);
            Assert.Equal("03415518", AsString(phase3.Digits));
        }

        [Theory]
        [InlineData("80871224585914546619083218645595", "24176176")]
        [InlineData("19617804207202209144916044189917", "73745418")]
        [InlineData("69317163492948606335995924319873", "52432133")]
        public void Part1_Sample2(string input, string expected)
        {
            var signal = Signal.From(input);
            var output = signal.Advance(100).Digits;
            Assert.Equal(expected, AsString(output.Take(8)));

            
        }

        [Fact]
        public void Repeat10k()
        {
            var signal = Signal.From("123").Repeat10k();

            Assert.Equal(30_000, signal.Digits.Length);
            Assert.Equal("123", AsString(signal.Digits.Take(3)));
            Assert.Equal("123", AsString(signal.Digits.TakeLast(3)));
            Assert.Equal(60_000, signal.Digits.Cast<int>().Sum());
        }

        [Theory]
        [InlineData("03036732577212944063491565474664", "84462026")]
        [InlineData("02935109699940807407585447034323", "78725270")]
        [InlineData("03081770884921959731165446850517", "53553731")]
        public void Part2_Samples(string input, string expected)
        {
            int offset = int.Parse(input.Substring(0, 7));
            var signal = Signal.From(input).Repeat10k();
            var output = signal.Advance(100, offset).Digits;
            Assert.Equal(expected, AsString(output.Skip(offset).Take(8)));
        }

        private static string AsString(IEnumerable<int> digis)
        {
            return new string(digis.Select(ToChar).ToArray());
            static char ToChar(int i) => (char)(i + '0');
        }


    }
}
