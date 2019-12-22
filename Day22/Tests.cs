using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace JakubSturc.AdventOfCode2019.Day22
{
    public class Tests
    {
        [Fact]
        public void ToDealIntoNewStack()
        {
            var deck = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var sud = new Shuffle.DealIntoNewStack();
            sud.ApplyTo(ref deck);
            Assert.Equal(new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }, deck);

        }

        [Theory]
        [InlineData(3, new int[] { 3, 4, 5, 6, 7, 8, 9, 0, 1, 2 })]
        [InlineData(-4, new int[] { 6, 7, 8, 9, 0, 1, 2, 3, 4, 5 })]
        public void ToCutNCards(int n, int[] expected)
        {
            var deck = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var sud = new Shuffle.Cut(n);
            sud.ApplyTo(ref deck);
            Assert.Equal(expected, deck);
        }

        [Theory]
        [InlineData(3, new int[] { 0, 7, 4, 1, 8, 5, 2, 9, 6, 3 })]
        public void ToDealWithIncrementN(int n, int[] expected)
        {
            var deck = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var sud = new Shuffle.DealWithIncrement(n);
            sud.ApplyTo(ref deck);
            Assert.Equal(expected, deck);
        }

        [Theory]
        [InlineData(new string[] {
"deal with increment 7",
"deal into new stack",
"deal into new stack" } , new int[] { 0, 3, 6, 9, 2, 5, 8, 1, 4, 7 })]
        [InlineData(new string[] {
"deal with increment 7",
"deal with increment 9",
"cut -2" }, new int[] { 6, 3, 0, 7, 4, 1, 8, 5, 2, 9 })]
        [InlineData(new string[] {
"deal into new stack",
"cut -2",
"deal with increment 7",
"cut 8",
"cut -4",
"deal with increment 7",
"cut 3",
"deal with increment 9",
"deal with increment 3",
"cut -1" }, new int[] { 9, 2, 5, 8, 1, 4, 7, 0, 3, 6 })]
        public void Part1_Samples(string[] lines, int[] expected)
        {
            var deck = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var shuffles = lines.Select(Shuffle.ParseFrom);

            foreach (var shuffle in shuffles)
            {
                shuffle.ApplyTo(ref deck);
            }

            Assert.Equal(expected, deck);
        }

    }
}
