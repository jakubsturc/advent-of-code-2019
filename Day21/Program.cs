using JakubSturc.AdventOfCode2019.IntComp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JakubSturc.AdventOfCode2019.Day21
{
    class Program
    {
        static void Main(string[] args)
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


            static IEnumerable<long> LoadAndWrite()
            {
                Console.OutputEncoding = Encoding.UTF8;

                var lines = File.ReadAllLines("part1.ascii");
                foreach (var line in lines)
                {
                    Console.WriteLine(line);

                    var copy = line;
                    if (line.Contains('#'))
                    {
                        copy = line.Substring(0, line.IndexOf('#'));
                    }

                    copy = copy.Trim();

                    if (copy.Length > 0)
                    {
                        foreach (char c in copy)
                        {
                            yield return (long)c;
                        }

                        yield return 10L;
                    }
                }
            }
        }
    }
}
