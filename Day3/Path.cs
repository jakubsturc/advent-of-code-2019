using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day3
{
    public class Path : IEnumerable<(int x, int y)>
    {
        private readonly List<(char dir, int len)> _steps;

        private Path(List<(char dir, int len)> steps)
        {
            _steps = steps;
        }

        public static Path Parse(string str)
        {
            var steps = str.Split(',').Select(ParseStep).ToList();
            return new Path(steps);

            (char dir, int len) ParseStep(string s)
            {
                var c = s[0];
                var i = int.Parse(s.Substring(1));
                return (c, i);
            }
        }

        public IEnumerator<(int x, int y)> GetEnumerator()
        {
            var position = (x: 0, y: 0);
            foreach (var (dir, len) in _steps)
            {
                (int x, int y) d = dir switch
                {
                    'R' => (1, 0),
                    'L' => (-1, 0),
                    'U' => (0, 1),
                    'D' => (0, -1),
                    _ => throw new NotSupportedException()
                };

                for (int i = 1; i <= len; i++)
                {
                    position = (position.x + d.x, position.y + d.y);
                    yield return position;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
