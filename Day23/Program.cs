using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JakubSturc.AdventOfCode2019.IntComp;

namespace JakubSturc.AdventOfCode2019.Day23
{
    public partial class Program
    {
        public const int Size = 50;

        static void Main(string[] args)
        {
            Part1();
        }
        
        static void Debug()
        {
            var program = IntProgram.ParseFrom("input.txt");
            var computer = new Computer(program, DummyInput(0));

            foreach ((var ticks, var output) in computer.Walk())
            {
                if (output.HasValue)
                {
                    Console.WriteLine($"{ticks};{output.Value}");
                }
            }

            IEnumerable<long> DummyInput(int idx)
            {
                yield return idx;
                while (true)
                {
                    yield return -1;
                }
            }
        }
        
        static void Part1()
        {
            // Console.WriteLine("Ticks;From;To;X;Y");
            var program = IntProgram.ParseFrom("input.txt");
            var sw = Stopwatch.StartNew();
            var computers = new Computer[Size];
            var inputs = new Queue<long>[Size];

            for (int i = 0; i < Size; i++)
            {
                inputs[i] = new Queue<long>();
                inputs[i].Enqueue(i);
                computers[i] = new Computer(program, NetworkInput(i));
            }

            var outputs = Interleave(computers.Select(c => c.Walk()));

            var result = Route(outputs);

            Console.WriteLine($"Part I: {result} in {sw.ElapsedMilliseconds}ms");

            IEnumerable<long> NetworkInput(int idx)
            {
                var input = inputs[idx];

                while (true)
                {
                    if (input.Count == 0)
                    {
                        yield return -1;
                    }
                    else
                    {
                        long val = input.Dequeue();
                        yield return val;
                    }
                }
            }

            long Route(IEnumerable<(int comp, long output, long ticks)> outputs)
            {
                var packets = new Queue<long>[Size];

                for (int i = 0; i < Size; i++)
                {
                    packets[i] = new Queue<long>();
                }

                foreach ((int comp, long output, long ticks) in outputs)
                {
                    var packet = packets[comp];
                    packet.Enqueue(output);

                    if (packet.Count == 3)
                    {
                        int adr = (int)packet.Dequeue();
                        long x = packet.Dequeue();
                        long y = packet.Dequeue();

                        //Console.WriteLine($"{ticks};{comp};{adr};{x};{y}");

                        if (adr == 255) return y;

                        var input = inputs[adr];
                        input.Enqueue(x);
                        input.Enqueue(y);
                    }
                }

                return -1; // all computers ended without sending anything to 255
            }
        }

        public static IEnumerable<(int comp, long output, long ticks)> Interleave(IEnumerable<IEnumerable<(long ticks, long? output)>> outputs)
        {
            var enumerators = outputs.Select(o => o.GetEnumerator()).ToArray();
            var len = enumerators.Length;
            var finished = new bool[len];
            bool allFinished;

            do
            {
                allFinished = true;

                for (int i = 0; i < len; i++)
                {
                    if (finished[i]) continue;

                    if (enumerators[i].MoveNext())
                    {
                        allFinished = false;
                        (var ticks, var output) = enumerators[i].Current;
                        if (output.HasValue)
                        {
                            yield return (comp: i, output: output.Value, ticks);
                        }
                    }
                    else
                    {
                        finished[i] = true;
                    }

                }
            } while (!allFinished);
        }
    }
}
