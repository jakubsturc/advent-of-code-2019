using System;
using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2019.Day3
{
    public class Tests
    {
        [Fact]
        public void Parse_And_Enumerate()
        {
            var path = Path.Parse("R75,D30,R83");
            var steps = path.ToList();

            Assert.Equal(75+30+83, steps.Count);
            Assert.Equal((1,0), steps[0]);
            Assert.Equal((75+83,-30), steps.Last());
        }

        [Fact]
        public void Distance_Example1()
        {
            var path1 = Path.Parse("R8,U5,L5,D3");
            var path2 = Path.Parse("U7,R6,D4,L4");

            var points = Walk.Cross(path1, path2);

            Assert.Equal(2, points.Count());
            Assert.Contains((3,3), points);
            Assert.Contains((6,5), points);
        }

        [Fact]
        public void Distance_Example2()
        {
            var path1 = Path.Parse("R75,D30,R83,U83,L12,D49,R71,U7,L72");
            var path2 = Path.Parse("U62,R66,U55,R34,D71,R55,D58,R83");

            var minx = Walk.MinX(path1, path2);

            Assert.Equal(159, minx);
        }

        [Fact]
        public void Distance_Example3()
        {
            var path1 = Path.Parse("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51");
            var path2 = Path.Parse("U98,R91,D20,R16,D67,R40,U7,R15,U6,R7");

            var minx = Walk.MinX(path1, path2);

            Assert.Equal(135, minx);
        }
    }
}
