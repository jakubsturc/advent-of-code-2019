using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day10
{
    [DebuggerDisplay("Asteroid = ({X},{Y})")]
    public struct Asteroid
    {
        public int X { get; }
        public int Y { get; }

        public Asteroid(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static double Angle(Asteroid from, Asteroid to)
        {
            var x = from.X - to.X;
            var y = from.Y - to.Y;
            return Math.Round(Math.Atan2(x, y), 5);
        }

        public static IEnumerable<Asteroid> ParseMap(string str)
        {
            int x = 1;
            int y = 1;

            foreach (char c in str)
            {
                switch (c)
                {
                    case '.': x++; continue;
                    case '#': yield return new Asteroid(x, y); x++; continue;
                    case '\n': y++; x = 1; continue;
                    default: continue;
                }
            }

        }

        public static int CountVisible(Asteroid from, IEnumerable<Asteroid> map)
        {
            return map
                .Where(a => !from.Equals(a))
                .GroupBy(a => Angle(from, a))
                .Count();
        }

        public static int IdealLocationIndex(IList<Asteroid> map)
        {
            return map.Select(a => CountVisible(a, map)).Max();
        }
    }
}