using System;
using System.IO;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var reactions = lines.Select(Reaction.Parse);
            var factory = new NanoFactory(reactions);
            var res1 = factory.GetOreRequirementFor(new ReactionItem(Chemical.FUEL, 1));
            
            Console.WriteLine($"Part I: {res1}");

            const long trillion = 1_000_000_000_000;
            long min = trillion / res1;
            long max = trillion;

            while (min < max - 1)
            {
                long med = (min + max) / 2;
                var ore = factory.GetOreRequirementFor(new ReactionItem(Chemical.FUEL, med));
                if (ore > trillion)
                {
                    max = med;
                }
                else
                {
                    min = med;
                }
            }

            Console.WriteLine($"Part II: {min}");
        }
    }
}
