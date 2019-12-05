namespace JakubSturc.AdventOfCode2019.Day5
{
    public class ImmediateMemory
    {
        public readonly int[] _memory;

        public ImmediateMemory(int[] memory)
        {
            _memory = memory;
        }

        public int this[int i]
        {
            get => _memory[i];
            set => _memory[i] = value;
        }
    }

    public class PositionMemory
    {
        public readonly int[] _memory;

        public PositionMemory(int[] memory)
        {
            _memory = memory;
        }

        public int this[int i]
        {
            get => _memory[_memory[i]];
            set => _memory[_memory[i]] = value;
        }
    }
}