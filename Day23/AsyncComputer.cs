using System.Collections.Generic;
using JakubSturc.AdventOfCode2019.IntComp;

namespace JakubSturc.AdventOfCode2019.Day23
{
    public class AsyncComputer
    {
        public long Id { get; }
        public PerfStat Stat { get; }

        private readonly Queue<long> _input;
        private readonly Computer _computer;
        private readonly Network _network;
        private readonly Scheduler _scheduler;

        private volatile bool _inputQueueIsEmpty;

        public AsyncComputer(long id, long[] program, Network network, Scheduler scheduler)
        {
            Id = id;
            Stat = new PerfStat();

            _input = new Queue<long>();
            _inputQueueIsEmpty = true;
            _network = network;
            _scheduler = scheduler;            
            _computer = new Computer(program, Read());

            _network.Register(this);
        }

        public void Run()
        {
            _scheduler.WaitOne(this);

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

                _scheduler.WaitOne(this);
            }
        }

        public void Write(long x)
        {
            lock (_input)
            {
                _input.Enqueue(x);
                _inputQueueIsEmpty = false;
            }

            Stat.Update(_computer.Ticks, allWrites: 1);
        }

        public void Write(long x, long y)
        {
            lock (_input)
            {
                _input.Enqueue(x);
                _input.Enqueue(y);
                _inputQueueIsEmpty = false;
            }

            Stat.Update(_computer.Ticks, allWrites: 2);
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

            if (res == -1)
            {
                Stat.Update(_computer.Ticks, realReads: 1);
                _scheduler.WaitOne(this);
            }
            else
            {
                Stat.Update(_computer.Ticks, emptyReads: 1);
            }

            yield return res;
        }

        public class PerfStat
        {
            public volatile uint AllWrites;
            public volatile uint EmptyReads;
            public volatile uint RealReads;
            public volatile uint Ticks;

            public void Update(long ticks, uint allWrites = 0, uint emptyReads = 0, uint realReads = 0)
            {
                AllWrites += allWrites;
                EmptyReads += emptyReads;
                RealReads += realReads;
                Ticks = (uint)ticks;
            }

            public override string ToString()
            {
                return $"Ticks: {Ticks}; Writes: {AllWrites}; Reads:{EmptyReads}/{RealReads}";
            }
        }
    }
}