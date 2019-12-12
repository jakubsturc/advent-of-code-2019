using System;

namespace JakubSturc.AdventOfCode2019.Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var con = ConsoleView.Create(80, 40);
            var sim = new Simulator();
            sim.Run();
            Console.WriteLine($"Part I: {sim.PaintedCount}");
        }
    }
}
