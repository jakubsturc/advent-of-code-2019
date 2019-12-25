using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JakubSturc.AdventOfCode2019.IntComp
{
    public class Computer
    {
        public IEnumerator<long> Input { get; }

        /// <summary>
        /// Gets total count of executed instructions
        /// </summary>
        public long Ticks { get; private set; } = 0;

        public long IP { get; private set; }

        public Memory Memory { get; }

        internal long OppCode { get => Memory.Immediate[IP]; }

        internal byte OP { get => (byte)(OppCode % 100); }

        internal byte M1 { get => (byte)(OppCode / 100 % 10); }
        internal byte M2 { get => (byte)(OppCode / 1000 % 10); }
        internal byte M3 { get => (byte)(OppCode / 10000 % 10); }

        internal long A1 { get => IP + 1; }
        internal long A2 { get => IP + 2; }
        internal long A3 { get => IP + 3; }

        internal long Mem1 { get => Memory[M1, A1]; set => Memory[M1, A1] = value; }
        internal long Mem2 { get => Memory[M2, A2]; set => Memory[M2, A2] = value; }
        internal long Mem3 { get => Memory[M3, A3]; set => Memory[M3, A3] = value; }

        public Computer(long[] program, IEnumerable<long> input)
        {
            Memory = InitMemory(program);
            Input = input.GetEnumerator();
            IP = 0;
        }

        private Memory InitMemory(long[] program)
        {
            return new Memory(program);
        }

        internal IEnumerable<long> GetMemory(long count)
        {
            for (long i = 0; i < count; i++)
            {
                yield return Memory.Immediate[i];
            }
        }

        public IEnumerable<long> Run()
        {
            foreach ((_, var output) in Walk())
            {
                if (output.HasValue)
                {
                    yield return output.Value;
                }
            }
        }

        public IEnumerable<(long ticks, long? output)> Walk()
        {
            while (true)
            {
                long? output = null;

                switch (OP)
                {
                    case 01: { Mem3 = Mem1 + Mem2; IP += 4; break; }
                    case 02: { Mem3 = Mem1 * Mem2; IP += 4; break; }
                    case 03: { Input.MoveNext(); Mem1 = Input.Current; IP += 2; break; }
                    case 04: { output = Mem1; IP += 2; break; }
                    case 05: { if (Mem1 != 0) IP = Mem2; else IP += 3; break; }
                    case 06: { if (Mem1 == 0) IP = Mem2; else IP += 3; break; }
                    case 07: { Mem3 = Mem1 < Mem2 ? 1 : 0; IP += 4; break; }
                    case 08: { Mem3 = Mem1 == Mem2 ? 1 : 0; IP += 4; break; }
                    case 09: { Memory.Relative.Adjust(Mem1); IP += 2; break; }
                    case 99: yield break;

                    default: throw new NotSupportedException();
                }

                yield return (++Ticks, output);
            }
        }
    }
}
