using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace JakubSturc.AdventOfCode2019.Day10
{
    using static Asteroid;

    public class Tests
    {
        [Fact]
        public void Parsing_Sample()
        {
            var asteroids = ParseMap(Sample1).ToList();

            Assert.Equal(10, asteroids.Count);
            Assert.Contains(new Asteroid(2, 1), asteroids);
            Assert.Contains(new Asteroid(5, 5), asteroids);
        }

        [Fact]
        public void Angle_Test()
        {
            var d = Angle(new Asteroid(1, 1), new Asteroid(1, 0));
            Assert.Equal(0, d);
        }

        [Theory]
        [InlineData(5,5, 7)]
        [InlineData(4,5, 8)]
        [InlineData(5,3, 5)]
        public void Sample1_Visibility(int x, int y, int expected)
        {
            var map = ParseMap(Sample1).ToList();
            var asteroid = new Asteroid(x, y);
            var cnt = CountVisible(from: asteroid, map);
            Assert.Equal(expected, cnt);
        }

        [Fact] 
        public void Sample1_Result()
        {
            var map = ParseMap(Sample1).ToList();
            Assert.Equal(8, IdealLocationIndex(map));
        }

        public const string Sample1 = @".#..#
.....
#####
....#
...##";

    }
}
