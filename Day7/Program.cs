using System;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var max = Permutation.All.Select(sequence => new Amplifier(
                    program: Puzzle.Input.ToArray(), // A copy
                    sequence: sequence,
                    input: new[] { 0 }))
                .Select(amp => amp.Run().First()) 
                .Max();

            Console.WriteLine($"Part I - highest signal: {max}");

            max = Permutation.All
                .Select(arr => arr.Select(i => i + 5).ToArray())
                .Select(sequence => new Amplifier(
                    program: Puzzle.Input.ToArray(), // A copy
                    sequence: sequence,
                    input: new[] { 0 }))
                .Select(amp => amp.Run().Last())
                .Max();

            Console.WriteLine($"Part II - highest signal: {max}");
        }
    }
}
