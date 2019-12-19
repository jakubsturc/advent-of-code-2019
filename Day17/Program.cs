using JakubSturc.AdventOfCode2019.IntComp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JakubSturc.AdventOfCode2019.Day17
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var prog = IntProgram.ParseFrom("input.txt");
            var comp = new Computer(prog, input: new long[0]);
            var camera = Camera.Process(comp.Run());
            Console.WriteLine(camera);

            var sum = camera.DetectIntersections().Select(p => p.x * p.y).Sum();

            Console.WriteLine($"Part I: {sum}");

            Console.ReadLine();
            Console.Clear();


            while (true)
            {

                var logic = File.ReadAllText("movementlogic.ascii").Select(c => (long)c).ToArray();

                prog[0] = 2;
                comp = new Computer(prog, input: logic);

                foreach (char c in comp.Run())
                {
                    Console.Write(c);
                }

                Console.WriteLine("DONE! Press enter to retry");
                Console.ReadLine();
            }
        }
    }

    public class Camera
    {
        private const char Scaffold = '#';
        private const char Space = '.';
        private const char NewLine = (char)10;
        private const int XLen = 50;
        private const int YLen = 43;

        private readonly char[,] _pixels;

        private Camera(char[,] pixels)
        {
            _pixels = pixels;
        }

        public static Camera Process(IEnumerable<long> input)
        {
            int x = 0, y = 0;
            var pixels = new char[XLen, YLen];
            foreach (char c in input)
            {
                switch (c)
                {
                    case NewLine:
                        y++; x = 0;
                        continue;
                    default:
                        pixels[x++, y] = c;
                        continue;
                }
            }

            return new Camera(pixels);
        }

        public IEnumerable<(int x, int y)> DetectIntersections()
        {
            return Enumerable
                .Range(0, YLen)
                .SelectMany(y => Enumerable.Range(0, XLen).Select(x => (x, y)))
                .Where(p => AttachedPixels(p).All(IsScafolling));

            bool IsScafolling((int x, int y) p)
            {
                var c = _pixels[p.x, p.y];
                return c == Scaffold;
            }

            IEnumerable<(int x, int y)> AttachedPixels((int x, int y) p)
            {
                int x = p.x; int y = p.y;

                yield return (x, y);
                if (x > 0) yield return (x-1, y);
                if (y > 0) yield return (x, y-1);
                if (x < XLen - 1) yield return (x+1,y);
                if (y < YLen - 1) yield return (x,y+1);
            }
        }

        public override string ToString()
        {
            var intersections = DetectIntersections().ToHashSet();

            var result = new StringBuilder();
            for (int y = 0; y < YLen; y++) 
            {
                for (int x = 0; x < XLen; x++)
                {
                    if (intersections.Contains((x, y)))
                        result.Append('O');
                    else
                        result.Append(_pixels[x, y]);
                }
                result.AppendLine();
            }
            return result.ToString();
        }




    }

}
