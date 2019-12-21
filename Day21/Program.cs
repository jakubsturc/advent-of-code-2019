using JakubSturc.AdventOfCode2019.IntComp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day21
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var prog = IntProgram.ParseFrom("input.txt");
                var comp = new Computer(prog, input: LoadAndWrite());

                foreach (long c in comp.Run())
                {
                    if (c < 256)
                    {
                        Console.Write((char)c);
                    }
                    else
                    {
                        Console.Write(c);
                    }
                }

                Console.WriteLine("DONE! Press enter to retry");
                Console.ReadLine();
            }

            static IEnumerable<long> LoadAndWrite()
            {
                var logic = File.ReadAllText("springdroid.ascii").Select(c => (long)c).ToArray();
                foreach (long x in logic)
                {
                    Console.Write((char)x);
                    yield return x;
                }
            }
        }
    }
}
