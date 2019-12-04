using System;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day3
{
    public class Program
    {
        static void Main(string[] args)
        {
            var minx = Walk.MinX(Input.Paths[0], Input.Paths[1]);

            Console.WriteLine($"Min distance: {minx}");
        }
    }
}
