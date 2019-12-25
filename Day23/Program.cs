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

            var result = Route(outputs, part1: true);

            Console.WriteLine($"Part I: {result} in {sw.ElapsedMilliseconds}ms");

            sw.Restart();

            computers = new Computer[Size];
            inputs = new Queue<long>[Size];

            for (int i = 0; i < Size; i++)
            {
                inputs[i] = new Queue<long>();
                inputs[i].Enqueue(i);
                computers[i] = new Computer(program, NetworkInput(i));
            }

            outputs = Interleave(computers.Select(c => c.Walk()));
            result = Route(outputs, part1: false);

            Console.WriteLine($"Part II: {result} in {sw.ElapsedMilliseconds}ms");

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

            long Route(IEnumerable<(int comp, long? output, long ticks)> outputs, bool part1)
            {
                var packets = new Queue<long>[Size];
                (long X, long Y) memory = (0L, 0L);
                (long X, long Y) lastSend = (-1L, -1L);
                const long idleTimeout = 1000;
                long timestamp = 0; // ticks of the last routed pocket

                for (int i = 0; i < Size; i++)
                {
                    packets[i] = new Queue<long>();
                }

                foreach ((int comp, long? output, long ticks) in outputs)
                {
                    if (part1 && !output.HasValue) continue;

                    if (!part1 && ticks > timestamp + idleTimeout && packets.All(IsEmpty) && inputs.All(IsEmpty))
                    {
                        if (memory.Y == lastSend.Y)
                        {
                            return memory.Y;
                        }

                        var input = inputs[0];
                        input.Enqueue(memory.X);
                        input.Enqueue(memory.Y);
                        lastSend = memory;
                        timestamp = ticks;
                    }

                    if (!output.HasValue) continue;

                    var packet = packets[comp];
                    packet.Enqueue(output.Value);

                    if (packet.Count == 3)
                    {
                        int adr = (int)packet.Dequeue();
                        long x = packet.Dequeue();
                        long y = packet.Dequeue();

                        //Console.WriteLine($"{ticks};{comp};{adr};{x};{y}");

                        if (adr == 255)
                        {
                            if (part1) return y;
                            memory = (x, y);
                        }
                        else
                        {
                            var input = inputs[adr];
                            input.Enqueue(x);
                            input.Enqueue(y);
                            
                        }

                        timestamp = ticks;
                    }
                }

                return -1; // all computers ended without sending anything to 255
            }

            bool IsEmpty(Queue<long> q) => q.Count == 0;
        }

        public static IEnumerable<(int comp, long? output, long ticks)> Interleave(IEnumerable<IEnumerable<(long ticks, long? output)>> outputs)
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
                        yield return (comp: i, output, ticks);
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
