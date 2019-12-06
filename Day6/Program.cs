using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var nodes = lines.Select(Parse).ToList();

            var level = new HashSet<string>() { "COM" };
            var depth = 0;
            var sum = 0;

            while (level.Count > 0)
            {
                depth++;
                level = nodes.Where(n => level.Contains(n.parent)).Select(n => n.child).ToHashSet();
                sum += depth * level.Count();
            }

            
            Console.WriteLine($"Part I: {sum}");

            var sanPath = PathToRoot("SAN").Reverse().ToList();
            var youPath = PathToRoot("YOU").Reverse().ToList();

            for (int i=0; i< sanPath.Count; i++)
            {
                if (youPath[i] != sanPath[i])
                {
                    Console.WriteLine($"Part II: i{i}, y{youPath.Count}, s{sanPath.Count}");
                    return;
                }
            }

            IEnumerable<string> PathToRoot(string? from)
            {
                while (from != null)
                {
                    yield return from;
                    from = nodes.Where(n => n.child == from).Select(n => n.parent).FirstOrDefault();
                }
            }
        }

        static (string parent, string child) Parse(string line)
        {
            var idx = line.IndexOf(')');
            var parent = line.Substring(0, idx);
            var child = line.Substring(idx + 1);
            return (parent, child);
        }
    }
}
