using System;
using System.IO;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var ascii = AsciiMap.CreateFrom(lines);
            var map = ascii.ToAbstractMap(MapItem.FromChar);
            var interestPoints = map.Where(item => item.IsDoor || item.IsKey).Select(JustCoords).ToHashSet();
            var crossroads = map.Where((col, row, item) => {
                if (item.IsWall) return false;
                return map.Neibneighbors(col, row)
                    .Where(_ => _.item.IsOpen)
                    .Count() != 2;
            }).Select(JustCoords).ToHashSet();
            var turns = map.Where((col, row, item) => {
                if (item.IsWall) return false;
                var neibneighbors = map.Neibneighbors(col, row).Where(_ => _.item.IsOpen).ToList();
                if (neibneighbors.Count != 2) return false;
                if (neibneighbors[0].col == neibneighbors[1].col) return false;
                if (neibneighbors[0].row == neibneighbors[1].row) return false;
                return true;
            }).Select(JustCoords).ToHashSet();
            var nodes = interestPoints.Union(crossroads).Union(turns);
            
            
            
            foreach ((var col, var row) in nodes)
            {
                ascii[col, row] = 'O';
            }

            ascii.WriteTo(Console.Out);

            (int col, int row) JustCoords((int col, int row, MapItem _) item) => (item.col, item.row);
        }
    }

    public struct MapItem
    { 
        public bool IsWall { get => !IsOpen; }
        public bool IsOpen { get; }
        public bool IsKey { get => Key != 0; }
        public bool IsDoor { get => Door != 0; }
        public bool IsPlayer { get; }

        public char Key { get; }
        public char Door { get; }

        private MapItem(bool isOpen, bool isPlayer, char key, char door)
        {
            IsOpen = isOpen;
            IsPlayer = isPlayer;
            Key = key;
            Door = door;
        }

        public override string ToString() 
        {
            return ToChar().ToString();
        }

        public char ToChar() => this switch
        {
            _ when IsWall => '#',
            _ when IsPlayer => '@',
            _ when IsKey => Key,
            _ when IsDoor => Door,
            _ => '.'
        };

        public static MapItem FromChar(char c) => c switch
        {
            '#' => new MapItem(false, false, (char)0, (char)0),
            '.' => new MapItem(true, false, (char)0, (char)0),
            '@' => new MapItem(true, true, (char)0, (char)0),
            _ when char.IsLetter(c) && char.IsLower(c) => new MapItem(true, false, c, (char)0),
            _ when char.IsLetter(c) && char.IsUpper(c) => new MapItem(true, false, (char)0, c),
            _ => throw new NotSupportedException($"Unsupported character '{c}'.")
        };
    }
}
