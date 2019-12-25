using System;
using System.Threading;
using JakubSturc.AdventOfCode2019.IntComp;

namespace JakubSturc.AdventOfCode2019.Day23
{
    public partial class Program
    {
        public const int Size = 50;

        static void Main(string[] args)
        {
            var program = IntProgram.ParseFrom("input.txt");

            var network = new Network(Size);
            var computers = new AsyncComputer[Size];
            
            for (int i = 0; i < Size; i++)
            {
                computers[i] = new AsyncComputer(i, program, network);
            }

            for (int i = 0; i < Size; i++)
            {
                var c = Size - i - 1;
                ThreadPool.QueueUserWorkItem(_ => computers[c].Run());
            }

            network.WaitForOutput();

            Console.WriteLine($"Part 1: {network.Result}");
        }
    }
}