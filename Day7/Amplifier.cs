using System.Collections.Generic;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day7
{
    public class Amplifier
    {
        public int[] Program { get; }
        public int[] Sequence { get; }
        public IEnumerable<int> Input { get; }

        public Amplifier(int[] program, int[] sequence, IEnumerable<int> input)
        {
            Program = program;
            Sequence = sequence;
            Input = input;
        }

        public IEnumerable<int> Run()
        {
            var buffer = new Queue<int>();
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

            IEnumerable<int> CopyIntoBuffer()
            {
                foreach (var x in input)
                {
                    buffer.Enqueue(x);
                    yield return x;
                }
            }

            IEnumerable<int> LoopBuffer()
            {
                while (buffer.Count > 0)
                {
                    yield return buffer.Dequeue();
                }
            }
        }
    }
}
