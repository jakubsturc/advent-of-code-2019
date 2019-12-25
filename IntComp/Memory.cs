using System;
using System.Collections.Generic;

namespace JakubSturc.AdventOfCode2019.IntComp
{
    public class Memory
    {
        public const int PrimaryMemorySize = 64 * 1024; // 64kB must be enought for everyone

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

        public Memory(long[] memory)
        {
            Immediate = new ImmediateMemory(memory);
            Position = new PositionMemory(Immediate);
            Relative = new RelativeMemory(Immediate);
        }

        public class ImmediateMemory
        {
            public readonly long[] _primary;
            public readonly Dictionary<long, long> _extended;

            public ImmediateMemory(long[] initial)
            {
                var copy = new long[PrimaryMemorySize];
                Array.Copy(initial, 0, copy, 0, initial.Length);
                _primary = copy;
                _extended = new Dictionary<long, long>();
            }

            public long this[long i]
            {
                get
                {
                    if (i < PrimaryMemorySize)
                    {
                        return _primary[i];
                    }
                    else
                    {
                        return _extended.TryGetValue(i, out long value) ? value : 0;
                    }
                }
                set
                {
                    if (i < PrimaryMemorySize)
                    {
                        _primary[i] = value;
                    }
                    else
                    {
                        _extended[i] = value;
                    }
                }
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