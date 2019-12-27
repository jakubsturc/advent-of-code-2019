using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day24
{
    class Program
    {
        static void Main(string[] args)
        {
            //var bugs = new[] { (4, 0), (0, 1), (3, 1), (0, 2), (3, 2), (4, 2), (2, 3), (0, 4) };

            var bugs = new[] { 
                (3, 0), 
                (0, 1), (2, 1), (3, 1), 
                (0, 2), (3, 2), (4, 2), 
                (0, 3), (2, 3), (3, 3), (4, 3),
                (0, 4), (1, 4)
            };

            var eris = new Eris(5, bugs);
            
            eris.Print(Console.Out);

            var bios = new HashSet<long>();

            foreach (var x in eris.Life())
            {
                if (!bios.Add(x))
                {
                    Console.WriteLine(x);
                    return;
                }
            }
        }
    }

    public class Eris
    {
        private readonly int _size;

        private HashSet<(int, int)> _bugs;
        private HashSet<(int, int)> _prev;

        public Eris(int size, IEnumerable<(int,int)> bugs)
        {
            _size = size;
            _bugs = bugs.ToHashSet();
            _prev = new HashSet<(int, int)>();
        }

        public IEnumerable<long> Life()
        {
            while (true)
            {
                Next();
                yield return Encode();
            }
        }

        public long Encode()
        {
            long res = 0L;
            foreach ((var x, var y) in _bugs)
            {
                res |= 1L << (_size * y + x);
            }
            return res;
        }


        public void Print(TextWriter target)
        {
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    target.Write(_bugs.Contains((x, y)) ? '#' : '.');
                }

                target.WriteLine();
            }
        }

        public void Next()
        {
            _prev.Clear();

            for (int x = 0; x < _size; x++)
            {
                for (int y = 0; y < _size; y++) 
                {
                    var m = MateCount(x, y);
                    var b = _bugs.Contains((x, y));
                    
                    if (b && m == 1)
                    {
                        _prev.Add((x, y));
                    }

                    if (!b && (m == 1 || m == 2))
                    {
                        _prev.Add((x, y));
                    }
                }
            }

            Swap(ref _bugs, ref _prev);

            void Swap(ref HashSet<(int, int)> a, ref HashSet<(int, int)> b)
            {
                var temp = a;
                a = b;
                b = temp;
            }

            int MateCount(int x, int y)
            {
                var w = _bugs.Contains((x + 1, y)) ? 1 : 0;
                var s = _bugs.Contains((x, y + 1)) ? 1 : 0;
                var e = _bugs.Contains((x - 1, y)) ? 1 : 0;
                var n = _bugs.Contains((x, y - 1)) ? 1 : 0;

                return w + s + e + n;
            }
        }

    }

}
