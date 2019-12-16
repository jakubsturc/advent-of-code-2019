using System;
using System.Collections.Generic;

namespace JakubSturc.AdventOfCode2019.Day14
{
    public class Chemical
    {
        private static readonly Dictionary<string, Chemical> _byName 
            = new Dictionary<string, Chemical>(StringComparer.OrdinalIgnoreCase);

        private static int _counter = 0;

        
        public static readonly Chemical ORE = Create("ORE");
        public static readonly Chemical FUEL = Create("FUEL");


        public string Name { get; }
        public int Id { get; }


        private Chemical(string name)
        {
            Name = name;
            Id = _counter++;
        }

        public static IEnumerable<Chemical> All => _byName.Values;

        public static Chemical Create(string name)
        {
            if (_byName.ContainsKey(name))
            {
                return _byName[name];
            }
            else
            {
                return _byName[name] = new Chemical(name);
            }
        }
    }

}
