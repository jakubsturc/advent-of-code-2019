using System;

namespace JakubSturc.AdventOfCode2019.Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var match = 0;
            var arr6 = new int[6];

            for (int i = 183564; i <= 657474; i++)
            {
                i.SplitInto(arr6);
                if (Validate6.NeverDecrease(arr6) && Validate6.AtLeastTwoAdjacentDigitsAreSame(arr6))
                {
                    match++;
                }
            }

            Console.WriteLine($"Part1 different passwords: {match}");

            // Part 2

            match = 0;

            for (int i = 183564; i <= 657474; i++)
            {
                i.SplitInto(arr6);
                if (Validate6.NeverDecrease(arr6) && Validate6.TwoAdjacentDigitsAreSame(arr6))
                {
                    match++;
                }
            }

            Console.WriteLine($"Part2 different passwords: {match}");

        }
    }
}
