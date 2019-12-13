using System;

namespace JakubSturc.AdventOfCode2019.Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var sim = new Simulator(init: Color.Black);
            sim.Run();
            Console.WriteLine($"Part I: {sim.PaintedCount}");

            Console.ReadLine();

            var con = ConsoleView.Create();
            sim = new Simulator(init: Color.White, view: con);
            sim.Run();

            Console.ReadLine();
        }
    }
}
