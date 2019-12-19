using System;
using System.Collections.Generic;

namespace JakubSturc.AdventOfCode2019.Day18
{
    public class AbstractMap<T>
    {
        private readonly T[][] _map;
        private readonly int _height;
        private readonly int _width;

        public int Height => _height;
        public int Width => _width;

        internal AbstractMap(int width, int height)
        {
            _width = width;
            _height = height;
            _map = new T[width][];

            for (int i = 0; i < height; i++)
            {
                _map[i] = new T[width];
            }
        }

        public T this[int col, int row]
        {
            get => _map[row][col];
            set => _map[row][col] = value;
        }

        public IEnumerable<(int col, int row, T item)> Where(Func<T, bool> condition)
            => Where((col, row, item) => condition(item));


        public IEnumerable<(int col, int row, T item)> Where(Func<int, int, T, bool> condition)
        {
            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    var item = this[col, row];
                    if (condition(col, row, item))
                    {
                        yield return (col, row, item);
                    }
                }

            }
        }

        public IEnumerable<(int col, int row, T item)> Neibneighbors(int col, int row, bool includeSelf = false)
        {
            if (includeSelf) yield return (col, row, this[col, row]);
            if (col > 0) yield return (col - 1, row, this[col - 1, row]);
            if (row > 0) yield return (col, row - 1, this[col, row - 1]);
            if (col < Width - 1) yield return (col + 1, row, this[col + 1, row]);
            if (row < Height -1) yield return (col, row + 1, this[col, row + 1]);
        }

        public AsciiMap ToAsciiMap(Func<T, char> selector)
        {
            var h = Height;
            var w = Width;
            var m = new char[h][];

            for (int row = 0; row < h; row++)
            {
                var c = m[row] = new char[w];
                for (int col = 0; col < w; col++)
                {
                    c[col] = selector(this[col, row]);
                }
            }

            return new AsciiMap(m);
        }
    }
}
