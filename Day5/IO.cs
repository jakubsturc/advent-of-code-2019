using System;
using System.Collections.Generic;
using System.Text;

namespace JakubSturc.AdventOfCode2019.Day5
{
    public interface IO
    {
        public static readonly IO None = new NoneIO();
        public static readonly IO Console = new ConsoleIO();

        public int Read();
        public void Write(int i);

        private class NoneIO : IO
        {
            public int Read() => throw new NotSupportedException();
            public void Write(int _) => throw new NotSupportedException();
        }

        private class ConsoleIO : IO
        {
            public int Read() => int.Parse(System.Console.ReadLine());
            public void Write(int i) => System.Console.Write($"{i} ");
        }
    }

    public class QueuedIO : IO
    {
        public Queue<int> Input { get; }
        public Queue<int> Output { get; }

        public QueuedIO(Queue<int> input, Queue<int> output)
        {
            Input = input;
            Output = output;
        }

        public int Read() => Input.Dequeue();

        public void Write(int i) => Output.Enqueue(i);
    }

}
