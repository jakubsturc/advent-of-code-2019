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

        public void Reset()
        {
            for (int j = 0; j < _len; j++)
            {
                _current[j] = _initial[j];
            }
            
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

        public long CalculateCycle()
        {
            (var cx, var cy, var cz) = CalculateCycles();

            return LCM(LCM(cx, cy), cz);

            static long LCM(long a, long b)
            {
                return a / GCD(a, b) * b;
            }

            static long GCD(long a, long b)
            {
                while (a != 0 && b != 0)
                {
                    if (a > b)
                        a %= b;
                    else
                        b %= a;
                }

                return a == 0 ? b : a;
            }
        }

        public (long, long, long) CalculateCycles()
        {
            long step = 0;
            long cycleX = -1, cycleY = -1, cycleZ = -1;
            bool found;
            do
            {
                Turn(1);
                step++;
                found = TestX() && TestY() && TestZ();

            } while (!found);

            return (cycleX, cycleY, cycleZ);

            bool TestX()
            {
                if (cycleX != -1) return true;

                for (int i = 0; i < _len; i++)
                {
                    if (_current[i].Velocity.X != _initial[i].Velocity.X) return false;
                    if (_current[i].Position.X != _initial[i].Position.X) return false;
                }

                cycleX = step;
                return true;
            }

            bool TestY()
            {
                if (cycleY != -1) return true;

                for (int i = 0; i < _len; i++)
                {
                    if (_current[i].Velocity.Y != _initial[i].Velocity.Y) return false;
                    if (_current[i].Position.Y != _initial[i].Position.Y) return false;
                }

                cycleY = step;
                return true;
            }

            bool TestZ()
            {
                if (cycleZ != -1) return true;

                for (int i = 0; i < _len; i++)
                {
                    if (_current[i].Velocity.Z != _initial[i].Velocity.Z) return false;
                    if (_current[i].Position.Z != _initial[i].Position.Z) return false;
                }

                cycleZ = step;
                return true;
            }
        }

        public Moon this[int i]
        {
            get => _current[i];
        }
    }
}
