using System;
using System.IO;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day22
{
    class Program
    {
        static void Main(string[] args)
        {
            var shuffles = File.ReadAllLines("input.txt").Select(Shuffle.ParseFrom);

            var len = 10007;
            var deck = Enumerable.Range(0, len).ToArray();

            foreach (var shuffle in shuffles)
            {
                shuffle.ApplyTo(ref deck);
            }

            Console.WriteLine($"Part I: {Array.IndexOf(deck,2019)}");
        }
    }
}
