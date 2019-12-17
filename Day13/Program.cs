using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JakubSturc.AdventOfCode2019.IntComp;

namespace JakubSturc.AdventOfCode2019.Day13
{
    partial class Program
    {
        static void Main(string[] args)
        {
            
            var prog = IntProgram.ParseFrom("input.txt");
            var comp = new Computer(prog, input: Enumerable.Empty<long>());
            var output = comp.Run().ToList();

            var tiles = new List<(long x, long y, long t)>(output.Count / 3);
            for (var i = 0; i < output.Count; i += 3)
            {
                tiles.Add((output[i], output[i + 1], output[i + 2]));
            }

            Console.WriteLine();
            Console.WriteLine($"Part 1: {tiles.Count(c => c.t == 2)}");
            Console.WriteLine("Press ENTER to start Part II");
            Console.ReadLine();

            var screen = Screen.Create();
            long ballX = 0, paddleX = 0;
            
            prog[0] = 2; // set it to 2 to play for free
            comp = new Computer(prog, input: Joystick());

            var dsp = new long[3];
            long cnt = 0;
            foreach (var o in comp.Run())
            {
                dsp[cnt % 3] = o;
                cnt++;
                if (cnt % 3 == 0)
                {
                    screen.PutChar(dsp[0], dsp[1], dsp[2]);

                    if (dsp[2] == 4)
                    {
                        ballX = dsp[0];
                    }

                    if (dsp[2] == 3)
                    {
                        paddleX = dsp[0];
                    }
                }
            }


            IEnumerable<long> Joystick()
            {
                while (true)
                {
                    Thread.Sleep(25);
                    if (ballX > paddleX) yield return 1;
                    if (ballX == paddleX) yield return 0;
                    if (ballX < paddleX) yield return -1;
                }
            }
        }
    }
}
