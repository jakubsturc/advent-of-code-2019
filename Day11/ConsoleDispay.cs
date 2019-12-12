using System;
using System.Threading;

namespace JakubSturc.AdventOfCode2019.Day11
{
    public class ConsoleView : IView
    {
        private int _shiftX;
        private int _shiftY;

        private ConsoleView(int widht, int height)
        {
            _shiftX = widht / 2;
            _shiftY = height / 2;
        }

        public static ConsoleView Create(int widht, int height)
        {
            Console.CursorVisible = false;
            widht = Console.WindowWidth;
            height = Console.WindowHeight;
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < widht; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write('.');
                }
                Console.WriteLine();
            }

            return new ConsoleView(widht, height);
        }

        public void PutChar(int x, int y, char c)
        {
            Console.SetCursorPosition(x + _shiftX, y + _shiftY);
            Console.Write(c);
        }

        public void PrintRobot(int x, int y, Direction dir)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            PutChar(x, y, dir.Code);
        }

        public void PrintTile(int x, int y, Color color)
        {
            Console.ForegroundColor = ConsoleColor.White;
            PutChar(x, y, color switch { Color.Black => '.', Color.White => '#', _ => '?' });
        }
    }
}
