using System;
using System.IO;

namespace JakubSturc.AdventOfCode2019.Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = File.ReadAllText("input.txt");
            var signal = Signal.From(str);
            var output = signal.Advance(100).Digits;

            Console.Write("Part I: ");

            for (int i = 0; i < 8; i++)
            {
                Console.Write(output[i]);
            }

            Console.WriteLine();

            int offset = int.Parse(str.Substring(0, 7));
            signal = Signal.From(str).Repeat10k();
            output = signal.Advance(100, offset).Digits;

            Console.Write("Part II: ");

            for (int i = 0; i < 8; i++)
            {
                Console.Write(output[offset + i]);
            }

            Console.WriteLine();


        }
    }
}
