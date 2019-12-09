using System;
using InnerMemory = System.Collections.Generic.Dictionary<long, long>;

namespace JakubSturc.AdventOfCode2019.Day9
{
    public class Memory
    {
        public ImmediateMemory Immediate { get; } 
        public PositionMemory Position { get; }
        public RelativeMemory Relative { get; }

        public long this[byte mode, long i]
        {
            get => mode switch
            {
                0 => Position[i],
                1 => Immediate[i],
                2 => Relative[i],
                _ => throw new NotSupportedException()
            };
            set
            {
                switch (mode)
                {
                    case 0: Position[i] = value; break;
                    case 1: Immediate[i] = value; break;
                    case 2: Relative[i] = value; break;
                    default:  throw new NotSupportedException();
                }
            }
        }

        public Memory(InnerMemory memory)
        {
            Immediate = new ImmediateMemory(memory);
            Position = new PositionMemory(Immediate);
            Relative = new RelativeMemory(Immediate);
        }

        public class ImmediateMemory
        {
            public readonly InnerMemory _memory;

            public ImmediateMemory(InnerMemory memory)
            {
                _memory = memory;
            }

            public long this[long i]
            {
                get => _memory.TryGetValue(i, out long value) ? value : 0;
                set => _memory[i] = value;
            }
        }

        public class PositionMemory
        {
            public readonly ImmediateMemory _memory;

            public PositionMemory(ImmediateMemory memory)
            {
                _memory = memory;
            }

            public long this[long i]
            {
                get => _memory[_memory[i]];
                set => _memory[_memory[i]] = value;
            }
        }

        public class RelativeMemory
        {
            public long Offset { get; private set; }

            public readonly ImmediateMemory _memory;

            public RelativeMemory(ImmediateMemory memory)
            {
                _memory = memory;
                Offset = 0;
            }

            public void Adjust(long x) => Offset += x;

            public long this[long i]
            {
                get => _memory[Offset + _memory[i]];
                set => _memory[Offset + _memory[i]] = value;
            }
        }
    }
}