using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JakubSturc.AdventOfCode2019.Day18
{
    public class AsciiMapTests
    {
        [Theory]
        [InlineData(Sample1)]
        [InlineData(Sample2)]
        public void CreateFrom_Text_Samples(string text)
        {
            var map = AsciiMap.CreateFrom(text);

            Assert.Equal(text.TrimEnd(), map.ToString().TrimEnd());
        }

        private const string Sample1 = @"#########
#b.A.@.a#
#########";

        private const string Sample2 = @"########################
#f.D.E.e.C.b.A.@.a.B.c.#
######################.#
#d.....................#
########################";
    }
}
