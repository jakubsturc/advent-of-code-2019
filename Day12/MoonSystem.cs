using System;
using System.Linq;

namespace JakubSturc.AdventOfCode2019.Day12
{
    public class MoonSystem
    {
        private readonly Moon[] _current;
        private readonly Moon[] _initial;
        private readonly int _len;
        private readonly P3[] _tmp;

        public int Energy => _current.Sum(m => m.Energy);

        public MoonSystem(params Moon[] moons)
        {
            _initial = moons.ToArray();
            _current = moons.ToArray();
            _len = _initial.Length;
            _tmp = new P3[_len];
        }

        public void Turn(int steps)
        {
            for (var i = 0; i < steps; i++)
            {
                for (int j = 0; j < _len; j++)
                {
                    _tmp[j] = _current[j].Position;
                }

                for (int j = 0; j < _len; j++)
                {
                    _current[j].ApplyGravity(_tmp);
                    _current[j].ApplyVelocity();
                }
            }
        }

        public ulong CalculateCycle()
        {
            ulong res = 0;
            do
            {
                Turn(1);
                res++;
            } while (!IsInInitialState());

            return res;
        }

        private bool IsInInitialState()
        {
            for (int j = 0; j < _len; j++)
            {
                if (_current[j].Position != _initial[j].Position) return false;
                if (_current[j].Velocity != _initial[j].Velocity) return false;
            }

            return true;
        }

        public Moon this[int i]
        {
            get => _current[i];
        }
    }
}
