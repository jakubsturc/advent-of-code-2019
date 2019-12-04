using System;

namespace JakubSturc.AdventOfCode2019.Day2
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Once you have a working computer, the first step is to restore the gravity assist program
            // (your puzzle input) to the "1202 program alarm" state it had just before the last computer
            // caught fire. To do this, before running the program, replace position 1 with the value 12 
            // and replace position 2 with the value 2.What value is left at position 0 after the program
            // halts?
            {
                var program = (int[])Input.Program.Clone();
                program[1] = 12;
                program[2] = 2;
                var interpreter = new IntCode(program);
                interpreter.Run();
                Console.WriteLine($"Part1: Value at position 0: {program[0]}");
            }            

            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    var program = (int[])Input.Program.Clone();
                    program[1] = noun;
                    program[2] = verb;
                    var interpreter = new IntCode(program);
                    interpreter.Run();

                    if (program[0] == 19690720)
                    {
                        Console.WriteLine($"What is 100 * noun + verb: {100 * noun + verb}");
                    }
                }
            }
        }
    }
}
