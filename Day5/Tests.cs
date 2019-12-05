using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JakubSturc.AdventOfCode2019.Day5
{
    public class Tests
    {
        [Theory]
        [InlineData(new[] { 1, 0, 0, 0, 99 }, new[] { 2, 0, 0, 0, 99 })]
        [InlineData(new[] { 2, 3, 0, 3, 99 }, new[] { 2, 3, 0, 6, 99 })]
        [InlineData(new[] { 2, 4, 4, 5, 99, 0 }, new[] { 2, 4, 4, 5, 99, 9801 })]
        [InlineData(new[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 }, new[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 })]
        public void Day2_Samples(int[] initial, int[] final)
        {
            var interpreter = new ThermalEnvironmentSupervisionTerminal(initial, IO.None);
            interpreter.Run();
            Assert.Equal(final, initial);
        }

        [Theory]
        [InlineData(new[] { 01002, 4, 3, 4, 33 }, new[] { 01002, 4, 3, 4, 99 })]
        [InlineData(new[] { 01102, 33, 3, 4, 0 }, new[] { 01102, 33, 3, 4, 99 })]
        public void Day5_Indirect_Samples(int[] initial, int[] final)
        {
            var interpreter = new ThermalEnvironmentSupervisionTerminal(initial, IO.None);
            interpreter.Run();
            Assert.Equal(final, initial);
        }

        [Fact]
        public void Day5_IO_Read_Samples()
        {
            var program = new[] { 3, 1, 99 };
            var io = new QueuedIO(new Queue<int>(new [] { 42 }), new Queue<int>());
            var interpreter = new ThermalEnvironmentSupervisionTerminal(program, io);
            interpreter.Run();
            Assert.Empty(io.Input);
            Assert.Equal(new[] { 3, 42, 99 }, program);
        }

        [Fact]
        public void Day5_IO_Write_Samples()
        {
            var program = new[] { 4, 2, 99 };
            var io = new QueuedIO(new Queue<int>(), new Queue<int>());
            var interpreter = new ThermalEnvironmentSupervisionTerminal(program, io);
            interpreter.Run();
            Assert.Equal(new[] { 99 }, io.Output);
        }

        [Theory]
        [InlineData(new[] { 3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9 }, new[] { 0 }, new[] { 0 })]
        [InlineData(new[] { 3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9 }, new[] { 1 }, new[] { 1 })]
        [InlineData(new[] { 3,3,1105,-1,9,1101,0,0,12,4,12,99,1 }, new[] { 0 }, new[] { 0 })]
        [InlineData(new[] { 3,3,1105,-1,9,1101,0,0,12,4,12,99, 1 }, new[] { 1 }, new[] { 1 })]
        [InlineData(new[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
                            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
                            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 }, new[] { 7 }, new[] { 999 })]
        [InlineData(new[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
                            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
                            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 }, new[] { 8 }, new[] { 1000 })]
        [InlineData(new[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
                            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
                            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 }, new[] { 9 }, new[] { 1001 })]
        public void Day5_PartII_Samples(int[] program, int[] input, int[] output)
        {
            var io = new QueuedIO(new Queue<int>(input), new Queue<int>());
            var interpreter = new ThermalEnvironmentSupervisionTerminal(program, io);
            interpreter.Run();
            Assert.Empty(io.Input);
            Assert.Equal(output, io.Output);
        }
    }
}
