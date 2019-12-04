using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JakubSturc.AdventOfCode2019.Day2
{
    public class Tests
    {
        [Fact]
        public void Example1()
        {
            var program = new int[] {1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50};
            var interpreter = new IntCode(program);
            interpreter.Run();
            Assert.Equal(3500, program[0]);
        }

        [Fact]
        public void Example2()
        {
            // 1,1,1,4,99,5,6,0,99 becomes 30,1,1,4,2,5,6,0,99
            var program = new int[] {1, 1, 1, 4, 99, 5, 6, 0, 99};
            var interpreter = new IntCode(program);
            interpreter.Run();
            Assert.Equal(30, program[0]);
        }
    }
}
