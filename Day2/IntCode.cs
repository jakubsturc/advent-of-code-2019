namespace JakubSturc.AdventOfCode2019.Day2
{
    public class IntCode
    {
        private readonly int[] _memory;
        private int _instPointer;

        public int OppCode => _memory[_instPointer];

        public int In1Addr => _memory[_instPointer + 1];
        public int In2Addr => _memory[_instPointer + 2];
        public int OutAddr => _memory[_instPointer + 3];

        public int In1Val => _memory[In1Addr];
        public int In2Val => _memory[In2Addr];
        public int OutValue { get => _memory[OutAddr]; set => _memory[OutAddr] = value; }

        public IntCode(int[] program)
        {
            _memory = program;
            _instPointer = 0;
        }

        public void Run()
        {
            while (Step()) ;
        }

        /// <summary>
        /// Perform on step of the program. Returns false iff halt (99) is called
        /// </summary>
        public bool Step()
        {
            switch (OppCode)
            {
                case 1: { OutValue = In1Val + In2Val; break; }
                case 2: { OutValue = In1Val * In2Val; break; }
                case 99: return false;
            }

            _instPointer += 4;
            return true;
        }


    }
}
