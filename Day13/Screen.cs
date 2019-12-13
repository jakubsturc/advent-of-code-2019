using System;

namespace Day13
{
    partial class Program
    {
        public class Screen
        {

            private readonly char[] _tiles = new char[] { ' ', '█', '░', '=', 'o' };

            private Screen() { }

            public static Screen Create()
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Green;
                return new Screen();
            }

            public void PutChar(long left, long top, long c)
            {
                if (left == -1 && top == 0)
                {
                    Console.SetCursorPosition(52, 0);
                    Console.Write($"Score: {c}");
                    return;
                }
                Console.SetCursorPosition((int)left, (int)top);
                Console.Write(_tiles[c]);
            }
        }
    }
}
