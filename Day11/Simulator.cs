using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JakubSturc.AdventOfCode2019.Day11
{

    public class Simulator
    {
        private int _x = 0;
        private int _y = 0;
        private Direction _dir;
        private readonly IRobot _robot;
        private readonly IView _view;
        private readonly Dictionary<(int x, int y), Color> _hull;

        public Simulator(IRobot? robot = null, IView? view = null, Color init = Color.Black)
        {
            _dir = Direction.N;
            _robot = robot ?? new PaintingRobot(GetCamera());
            _hull = new Dictionary<(int x, int y), Color>() { { (0, 0), init } };
            _view = view ?? IView.Null;
        }

        public void Run()
        {
            foreach (var (cmd, x) in _robot.Run())
            {
                switch (cmd)
                {
                    case Command.Paint:
                        _hull[(_x, _y)] = (Color)x;
                        break;
                    case Command.Move:
                        _dir = _dir.Turn((Side)x);
                        _dir.Move(ref _x, ref _y);
                        break;
                    default: throw new NotSupportedException();
                }
                Print();
            }
        }

        private void Print()
        {
            foreach (var tile in _hull)
            {
                (int x, int y) = tile.Key;
                _view.PrintTile(x, y, tile.Value);
            }
            _view.PrintRobot(_x, _y, _dir);
        }

        public int PaintedCount { get => _hull.Count; }

        private IEnumerable<long> GetCamera()
        {
            while (true)
            {
                yield return (long)GetColor(_x, _y);
            }
        }

        private Color GetColor(int x, int y)
        {
            return _hull.TryGetValue((x, y), out var color) ? color : Color.Black;
        }
    }
}
