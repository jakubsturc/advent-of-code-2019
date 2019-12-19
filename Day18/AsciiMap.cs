using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JakubSturc.AdventOfCode2019.Day18
{
    public class AsciiMap
    {
        private readonly char[][] _map;

        public int Height => _map.Length;
        public int Width => _map[0].Length;

        internal AsciiMap(char[][] map)
        {
            _map = map;
        }

        public char this[int col, int row]
        {
            get => _map[row][col];
            set => _map[row][col] = value;
        }

        public void WriteTo(TextWriter writer)
        {
            var h = Height;
            for (int row = 0; row < h; row++)
            {
                writer.WriteLine(_map[row]);
            }
        }

        public AbstractMap<T> ToAbstractMap<T>(Func<char,T> selector)
        {
            var h = Height;
            var w = Width;
            var r = new AbstractMap<T>(w, h);
            for (int row = 0; row < h; row++)
            {
                for (int col = 0; col < w; col++)
                {
                    r[col, row] = selector(this[col,row]);
                }
            }

            return r;
        }

        public override string ToString()
        {
            var h = Height;
            var sb = new StringBuilder();
            for (int row = 0; row < h; row++)
            {
                var line = new string(_map[row]);
                sb.AppendLine(line);
            }
            return sb.ToString();
        }

        public static AsciiMap CreateFrom(string text)
        {
            var lines = AllLines(text);
            return CreateFrom(lines);

            static IEnumerable<string> AllLines(string str)
            {
                using var reader = new StringReader(str);
                while (true)
                {
                    var line = reader.ReadLine();

                    if (line == null)
                    {
                        yield break;
                    }
                    else
                    {
                        yield return line;
                    }
                }
            }
        }

        public static AsciiMap CreateFrom(IEnumerable<string> lines)
        {
            // we throw away empty lines
            var copy = lines.Where(l => l.Length > 0).ToList();
            
            var w = copy.Max(line => line.Length);
            var h = copy.Count;
            var map = new char[h][];

            for (int y = 0; y < h; y++)
            {
                var str = copy[y];
                var cou = map[y] = new char[w];
                str.CopyTo(0, cou, 0, str.Length);
            }

            return new AsciiMap(map);
        }
    }
}
