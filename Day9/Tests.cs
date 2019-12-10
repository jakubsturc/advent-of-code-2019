using System;
using System.Linq;
using System.Text;
using Xunit;

namespace JakubSturc.AdventOfCode2019.Day9
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

        [Theory]
        [InlineData(new long[] { 3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0 }, new long[] { 4,3,2,1,0 }, 43210)]
        [InlineData(new long[] { 3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0 }, new long[] { 0,1,2,3,4 }, 54321)]
        [InlineData(new long[] { 3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0 }, new long[] { 1,0,4,3,2 }, 65210)]
        public void Day7_PartI_Sample(long[] program, long[] sequence, long expected)
        {
            var amplifier = new Amplifier(program, sequence, new long[] { 0 });
            var actual = amplifier.Run();
            Assert.Equal(expected, actual.First());
        }

        [Theory]
        [InlineData(new long[] { 3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,
                            27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5 }, new long[] { 9,8,7,6,5 }, 139629729)]
        [InlineData(new long[] { 3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,
                            -5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,
                            53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10 }, new long[] { 9,7,8,5,6 }, 18216)]
        public void Day7_PartII_Sample(long[] program, long[] sequence, long expected)
        {
            var amplifier = new Amplifier(program, sequence, new long[] { 0 });
            var actual = amplifier.Run();
            Assert.Equal(expected, actual.Last());
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
