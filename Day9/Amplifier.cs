using System.Collections.Generic;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day9
{
    public class Amplifier
    {
        public long[] Program { get; }
        public long[] Sequence { get; }
        public IEnumerable<long> Input { get; }

        public Amplifier(long[] program, long[] sequence, IEnumerable<long> input)
        {
            Program = program;
            Sequence = sequence;
            Input = input;
        }

        public IEnumerable<long> Run()
        {
            var buffer = new Queue<long>();
            var input = Input.Concat(LoopBuffer());

            foreach (int i in Sequence)
            {
                var test = new IntComp(
                    program: Program.ToArray(), // a copy
                    input: input.Prepend(i)
                );

                input = test.Run(); // output is the next input
            }

            return CopyIntoBuffer();

            IEnumerable<long> CopyIntoBuffer()
            {
                foreach (var x in input)
                {
                    buffer.Enqueue(x);
                    yield return x;
                }
            }

            IEnumerable<long> LoopBuffer()
            {
                while (buffer.Count > 0)
                {
                    yield return buffer.Dequeue();
                }
            }
        }
    }
}
