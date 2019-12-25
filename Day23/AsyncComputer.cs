using System.Collections.Generic;
using JakubSturc.AdventOfCode2019.IntComp;

namespace JakubSturc.AdventOfCode2019.Day23
{
    public class AsyncComputer
    {
        public long Id { get; }

        private readonly Queue<long> _input;
        private readonly Computer _computer;
        private readonly Network _network;
        private readonly PerfStat _stat = new PerfStat();

        private volatile bool _inputQueueIsEmpty;

        public AsyncComputer(long id, long[] program, Network network)
        {
            Id = id;
            _input = new Queue<long>();
            _inputQueueIsEmpty = true;
            _network = network;
            _computer = new Computer(program, Read());
            _network.Register(this);
        }

        public void Run()
        {
            long cnt = 0;
            var buffer = new long[3];
            foreach (var x in _computer.Run())
            {
                if (!_network.IsRunning())
                {
                    return;
                }

                int i = (int)(cnt++ % 3);
                buffer[i] = x;
                if (i == 2)
                {
                    _network.Send(address: buffer[0], x: buffer[1], y: buffer[2]);
                }
            }
        }

        public void Write(long x)
        {
            lock (_input)
            {
                _input.Enqueue(x);
                _inputQueueIsEmpty = false;
            }

            _stat.UpdateWrites(by: 1);
        }

        public void Write(long x, long y)
        {
            lock (_input)
            {
                _input.Enqueue(x);
                _input.Enqueue(y);
                _inputQueueIsEmpty = false;
            }

            _stat.UpdateWrites(by: 2);
        }

        private IEnumerable<long> Read()
        {
            long res = -1;
                
            // a small optimalization to avoid unnecessary locking.
            // reading/writing bool is atomic operation and the worst case is
            // that we will read one additional -1
            if (!_inputQueueIsEmpty)
            {
                lock (_input)
                {
                    if (_input.Count > 0)
                    {
                        res = _input.Dequeue();
                    }

                    _inputQueueIsEmpty = _input.Count == 0;
                }
            }

            _stat.UpdateReads(wasReal: res != -1);

            yield return res;
        }

        public struct PerfStat
        {
            public volatile uint AllWrites;
            public volatile uint AllReads;
            public volatile uint EmptyReads;
            public volatile uint RealReads;

            public void UpdateReads(bool wasReal)
            {
                AllReads++;
                if (wasReal)
                {
                    RealReads++;
                }
                else
                {
                    EmptyReads++;
                }
            }

            public void UpdateWrites(uint by)
            {
                AllWrites += by;
            }
        }
    }
}