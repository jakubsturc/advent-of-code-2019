using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2019.Day12
{
    public class Tests
    {
        [Fact]
        public void Part1_Velocity_Sample()
        {
            var europa = new Moon() { Position = new P3(1, 2, 3), Velocity = new P3(-2, 0, 3) };
            europa.ApplyVelocity();
            Assert.Equal(new P3(-1, 2, 6), europa.Position);
        }

        [Fact]
        public void Part1_Example1()
        {
            var a = new Moon() { Position = new P3(-1, 0, 2) };
            var b = new Moon() { Position = new P3(2, -10, -7) };
            var c = new Moon() { Position = new P3(4, -8, 8) };
            var d = new Moon() { Position = new P3(3, 5, -1) };

            var system = new MoonSystem(a, b, c, d);
            system.Turn(1);

            // Positions after 1 step:
            Assert.Equal(new P3(2,-1, 1), system[0].Position);
            Assert.Equal(new P3(3,-7,-4), system[1].Position);
            Assert.Equal(new P3(1,-7, 5), system[2].Position);
            Assert.Equal(new P3(2, 2, 0), system[3].Position);
            // Velocities after 1 step:
            Assert.Equal(new P3( 3,-1,-1), system[0].Velocity);
            Assert.Equal(new P3( 1, 3, 3), system[1].Velocity);
            Assert.Equal(new P3(-3, 1,-3), system[2].Velocity);
            Assert.Equal(new P3(-1,-3, 1), system[3].Velocity);
        }

        [Fact]
        public void Part1_Example2()
        {
            var b = new Moon() { Position = new P3(-8,-10,0) };
            var c = new Moon() { Position = new P3(5,5,10) };
            var a = new Moon() { Position = new P3(2,-7,3) };
            var d = new Moon() { Position = new P3(9,-8,-3) };

            var system = new MoonSystem(new [] { a, b, c, d });
            system.Turn(100);

            Assert.Equal(1940, system.Energy);
        }

        [Fact]
        public void Part2_Example1()
        {
            var a = new Moon() { Position = new P3(-1, 0, 2) };
            var b = new Moon() { Position = new P3(2, -10, -7) };
            var c = new Moon() { Position = new P3(4, -8, 8) };
            var d = new Moon() { Position = new P3(3, 5, -1) };

            var system = new MoonSystem(a, b, c, d);
            var cycle = system.CalculateCycle();
            Assert.Equal((ulong)2772, cycle);
        }
    }
}
