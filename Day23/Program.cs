using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JakubSturc.AdventOfCode2019.IntComp;

namespace JakubSturc.AdventOfCode2019.Day23
{
    public partial class Program
    {
        public const int Size = 50;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting ...");
            var program = IntProgram.ParseFrom("input.txt");

            var scheduler = new Scheduler(Size);
            var network = new Network(Size);
            var computers = new AsyncComputer[Size];

            for (int i = 0; i < Size; i++)
            {
                computers[i] = new AsyncComputer(i, program, network, scheduler);
            }

            scheduler.RunAll(computers);

            Thread.Sleep(60_000);

            network.PrintStat();

            //network.WaitForOutput();

            //Console.WriteLine($"Part 1: {network.Result}");
        }
    }

    public class Scheduler
    {
        private readonly int _size;
        private readonly ManualResetEvent[] _resetEvents;
        //private readonly AsyncComputer.PerfStat[] _stats;
        private readonly Timer _timer;
        private readonly int _processorCount;

        private int _roundRobinCounter = 0;

        public Scheduler(int size)
        {
            _size = size;
            _processorCount = Environment.ProcessorCount;
            _resetEvents = new ManualResetEvent[size];

            for (int i = 0; i < _size; i++)
            {
                _resetEvents[i] = new ManualResetEvent(false);
            }

            _timer = new Timer(Schedule);
        }

        private void Schedule(object? _)
        {
            for (int i = 0; i < _processorCount; i++)
            {
                var toStop = (_roundRobinCounter + i) % _size;
                var resetEvent = _resetEvents[toStop];
                resetEvent.Reset();

                var toStart = (_roundRobinCounter + i + _processorCount) % _size;
                resetEvent = _resetEvents[toStart];
                resetEvent.Set();
            }
        }

        public void RunAll(AsyncComputer[] computers)
        {
            for (int i = 0; i < _size; i++)
            {
                var computer = computers[i];
                //_stats[i] = computer.Stat;
                var thread = new Thread(() => computer.Run());
                thread.Start();
            }

            _timer.Change(0, 1000);
        }

        public void WaitOne(AsyncComputer asyncComputer)
        {   
            var id = asyncComputer.Id;
            var resetEvent = _resetEvents[id];
            resetEvent.WaitOne();
        }
    }
}