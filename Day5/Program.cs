using System;

namespace JakubSturc.AdventOfCode2019.Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = Puzzle.Input;
            var test = new ThermalEnvironmentSupervisionTerminal(program, IO.Console);
            test.Run();
        }
    }
}
