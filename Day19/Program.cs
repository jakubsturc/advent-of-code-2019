using JakubSturc.AdventOfCode2019.IntComp;
using System;
using System.Linq;
using System.Collections.Generic;

namespace JakubSturc.AdventOfCode2019.Day19
{
    class Program
    {
        static void Main(string[] args)
        {
            var prog = IntProgram.ParseFrom("input.txt");
            Part1(prog);

        }

        private static void Part1(long[] prog)
        {
            var result = Enumerable.Empty<long>();
            foreach ((long x, long y) in GetCoordinates(50))
            {
                var comp = new Computer(prog, input: new[] { x, y });
                result = result.Concat(comp.Run());
            }

            Console.WriteLine($"Part I: {result.Sum()}");
        }

        static IEnumerable<(long,long)> GetCoordinates(int dim)
        {
            for (int i = 0; i < dim; i++)
                for (int j = 0; j < dim; j++)
                {
                    yield return (j, i);
                }
        }
    }
}
