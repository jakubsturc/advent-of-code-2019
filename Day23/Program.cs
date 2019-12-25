using System;
using System.Collections.Generic;
using System.Threading;
using JakubSturc.AdventOfCode2019.IntComp;

namespace JakubSturc.AdventOfCode2019.Day23
{
    public class Program
    {
        public const int Size = 50;

        static void Main(string[] args)
        {
            var program = IntProgram.ParseFrom("input.txt");

            var network = new Network();
            var computers = new AsyncComputer[Size];
            
            for (int i = 0; i < Size; i++)
            {
                computers[i] = new AsyncComputer(i, program, network);
            }

            for (int i = 0; i < Size; i++)
            {
                var c = Size - i - 1;
                ThreadPool.QueueUserWorkItem(_ => computers[c].Run());
            }

            network.WaitForOutput();

            Console.WriteLine($"Part 1: {network.Result}");
        }

        public class Network
        {
            public long Result { get; set; }

            public readonly ManualResetEvent _hasOutputSingal = new ManualResetEvent(false);

            private readonly AsyncComputer[] _computers = new AsyncComputer[Size];

            private volatile bool _isRunnning = true;

            public void Register(AsyncComputer comp)
            {
                var id = comp.Id;
                _computers[id] = comp;
                comp.Write(id);
            }

            public void Send(long address, long x, long y)
            {
                Console.WriteLine($"{DateTime.Now.ToLongTimeString()}: addr:{address}, x:{x}, y{y}");

                if (address == 255)
                {
                    Result = y;
                    Stop();
                    return;
                }

                var comp = _computers[address];
                comp.Write(x, y);
            }

            private void Stop()
            {
                _isRunnning = false;
                _hasOutputSingal.Set();
            }

            public bool IsRunning() => _isRunnning;

            public void WaitForOutput()
            {
                _hasOutputSingal.WaitOne();
            }
        }

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
}