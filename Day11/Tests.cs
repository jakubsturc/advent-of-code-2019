using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2019.Day11
{
    public partial class Tests
    {
        [Theory]
        [InlineData(new long[] { 1, 0, 0, 0, 99 }, new long[] { 2, 0, 0, 0, 99 })]
        [InlineData(new long[] { 2, 3, 0, 3, 99 }, new long[] { 2, 3, 0, 6, 99 })]
        [InlineData(new long[] { 2, 4, 4, 5, 99, 0 }, new long[] { 2, 4, 4, 5, 99, 9801 })]
        [InlineData(new long[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 }, new long[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 })]
        public void Day2_Samples(long[] initial, long[] final)
        {
            var interpreter = new IntComp(initial, Enumerable.Empty<long>());
            _ = interpreter.Run().ToList();
            var memory = interpreter.GetMemory(initial.Length);
            Assert.Equal(final, memory);
        }

        [Theory]
        [InlineData(new long[] { 01002, 4, 3, 4, 33 }, new long[] { 01002, 4, 3, 4, 99 })]
        [InlineData(new long[] { 01102, 33, 3, 4, 0 }, new long[] { 01102, 33, 3, 4, 99 })]
        public void Day5_Indirect_Samples(long[] initial, long[] final)
        {
            var interpreter = new IntComp(initial, Enumerable.Empty<long>());
            _ = interpreter.Run().ToList();
            var memory = interpreter.GetMemory(initial.Length);
            Assert.Equal(final, memory);
        }

        [Fact]
        public void Day5_IO_Read_Samples()
        {
            var program = new long[] { 3, 1, 99 };            
            var interpreter = new IntComp(program, new long[] { 42 });
            _ = interpreter.Run().ToList();
            var memory = interpreter.GetMemory(program.Length);
            Assert.Equal(new long[] { 3, 42, 99 }, memory);
        }

        [Fact]
        public void Day5_IO_Write_Samples()
        {
            var program = new long[] { 4, 2, 99 };
            var interpreter = new IntComp(program, Enumerable.Empty<long>());
            var output = interpreter.Run();
            Assert.Equal(new long[] { 99 }, output);
        }

        [Theory]
        [InlineData(new long[] { 3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9 }, new long[] { 0 }, new long[] { 0 })]
        [InlineData(new long[] { 3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9 }, new long[] { 1 }, new long[] { 1 })]
        [InlineData(new long[] { 3,3,1105,-1,9,1101,0,0,12,4,12,99,1 }, new long[] { 0 }, new long[] { 0 })]
        [InlineData(new long[] { 3,3,1105,-1,9,1101,0,0,12,4,12,99, 1 }, new long[] { 1 }, new long[] { 1 })]
        [InlineData(new long[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
                            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
                            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 }, new long[] { 7 }, new long[] { 999 })]
        [InlineData(new long[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
                            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
                            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 }, new long[] { 8 }, new long[] { 1000 })]
        [InlineData(new long[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
                            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
                            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 }, new long[] { 9 }, new long[] { 1001 })]
        public void Day5_PartII_Samples(long[] program, long[] input, long[] expected)
        {
            var interpreter = new IntComp(program, input);
            var actual = interpreter.Run();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Day9_PartI_Quine()
        {
            var quine = new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };
            var interpreter = new IntComp(quine, Enumerable.Empty<long>());
            var output = interpreter.Run().ToList();
            Assert.Equal(quine, output);
        }

        [Fact]
        public void Day9_PartI_16DigitNumber()
        {
            var quine = new long[] { 1102, 34915192, 34915192, 7, 4, 7, 99, 0 };
            var interpreter = new IntComp(quine, Enumerable.Empty<long>());
            var output = interpreter.Run().ToList();
            Assert.Equal(16, output[0].ToString().Length);
        }

        [Fact]
        public void Day9_PartI_LargeNumber()
        {
            var quine = new long[] { 104, 1125899906842624, 99 };
            var interpreter = new IntComp(quine, Enumerable.Empty<long>());
            var output = interpreter.Run();
            Assert.Equal(1125899906842624, output.First());
        }

        [Fact]
        public void Day9_PartI_OutOfInitialMemory()
        {
            var quine = new long[] { 1001, 1, 0, 1000, 04, 1000, 99 };
            var interpreter = new IntComp(quine, Enumerable.Empty<long>());
            var output = interpreter.Run();
            Assert.Equal(1, output.First());
        }
    }
}
