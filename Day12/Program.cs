using System;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var b = new Moon() { Position = new P3( 17, -7,-11) };
            var c = new Moon() { Position = new P3(  1,  4, -1) };
            var a = new Moon() { Position = new P3(  6, -2, -6) };
            var d = new Moon() { Position = new P3( 19, 11,  9) };
            var system = new MoonSystem(new[] { a, b, c, d });
            system.Turn(1000);

            Console.WriteLine($"Part 1:{system.Energy}");
        }
    }
}
