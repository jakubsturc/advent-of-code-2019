using System;
using System.Threading;

namespace JakubSturc.AdventOfCode2019.Day23
{
    public class Network
    {
        public int Size { get; }

        public long Result { get; set; }

        public readonly ManualResetEvent _hasOutputSingal = new ManualResetEvent(false);

        private readonly AsyncComputer[] _computers;

        private volatile bool _isRunnning = true;

        public Network(int size)
        {
            Size = size;
            _computers = new AsyncComputer[Size];
        }

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
}