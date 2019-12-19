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
            
            //Print(prog);

            Part2(prog);

        }

        private static void Part2(long[] prog)
        {
            var beam = new List<(long start, long count)>();
            const int d = 100;
            for (long y = 0, start = 0; ;y++)
            {
                bool first = false; // true iff a first start on row was already found
                long count = 0;
                for (var x = start; y > 20 || x < 50; x++)
                {   
                    switch (first, IsPulled(prog, x, y))
                    {
                        case (true, true): continue;
                        case (true, false): count = x - start; break; 
                        case (false, true): start = x; first = true; continue; 
                        case (false, false): continue; 
                    }
                    break;
                }

                beam.Add((start, count));

                if (y >= d && count >= d)
                {
                    (long prevStart, long prevCount) = beam[^d];
                    if (start - prevStart + d <= prevCount)
                    {
                        Console.WriteLine($"Part II: {10000*start+(y - d + 1)}");
                        return;
                    }
                }
            }
        }

        private static void Print(long[] prog)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            for (long y = 0; y < 50; y++)
            {
                Console.Write("{0:00}", y);
                for (long x = 0; x < 50; x++)
                {
                    Console.BackgroundColor = (0 == (x / 5) % 2) ? ConsoleColor.DarkGreen: ConsoleColor.DarkBlue;
                    Console.Write(IsPulled(prog, x, y) ? "❤" : "❄");
                }
                Console.WriteLine();
            }
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

        static bool IsPulled(long[] prog, long x, long y)
        {
            var comp = new Computer(prog, input: new[] { x, y });
            return comp.Run().First() == 1L;
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
