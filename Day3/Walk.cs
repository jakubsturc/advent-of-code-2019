using System;
using System.Collections.Generic;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day3
{
    public class Walk
    {
        protected readonly HashSet<(int, int)> _steps;

        private Walk(Path path)
        {
            _steps = new HashSet<(int, int)>(path);
        }

        public static IEnumerable<(int x, int y)> Cross(Path path1, Path path2)
        {
            var walk1 = new Walk(path1);
            var walk2 = new Walk(path2);
            return walk1._steps.Intersect(walk2._steps);
        }

        public static int MinX(Path path1, Path path2)
        {
            var walk1 = new Walk(path1);
            var walk2 = new Walk(path2);
            return walk1._steps.Intersect(walk2._steps)
                .Select(p => Math.Abs(p.Item1) + Math.Abs(p.Item2)).Min();
        }
    }

}
