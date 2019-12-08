using System;
using System.IO;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt").First();
            int w = 25; // 25 pixels wide
            int t = 6; // 6 pixels tall
            int ls = w * t; // layer size
            int lc = input.Length / ls; // layer count;

            var min = (c0: int.MaxValue, c1: 0, c2: 0);
            for (int i = 0; i < input.Length; i += ls)
            {
                var current = PixelCount(input.Substring(i, ls));
                if (current.c0 < min.c0)
                {
                    min = current;
                }
            }

            Console.WriteLine($"Part I: {min}");

            var result = Enumerable.Repeat(2, ls).ToArray();
            for (int i = 0; i < input.Length; i++)
            {
                if (result[i % ls] == 2)
                {
                    result[i % ls] = input[i] - '0';
                }
            }

            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == 2)
                {
                    result[i] = 0;
                }

                if (i % w == 0)
                {
                    Console.WriteLine();
                }

                Console.Write(result[i] == 0 ? ' ' : '█');

                
            }

            

            (int c0, int c1, int c2) PixelCount(string str)
            {
                return 
                (
                    c0: str.Count(c => c == '0'),
                    c1: str.Count(c => c == '1'),
                    c2: str.Count(c => c == '2')
                );
            }
        }
    }
}
