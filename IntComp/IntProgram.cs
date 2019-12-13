using System.IO;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.IntComp
{
    public class IntProgram
    {
        public static long[] ParseFrom(string file)
        {
            return File
                .ReadAllText(file)
                .Split(',')
                .Select(long.Parse)
                .ToArray();
        }
    }
}
