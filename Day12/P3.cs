using System;
using System.Diagnostics.CodeAnalysis;

namespace JakubSturc.AdventOfCode2019.Day12
{
    public struct P3
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;

        public P3(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static P3 operator +(P3 a, P3 b) => new P3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static P3 operator -(P3 a, P3 b) => new P3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        public P3 Sign() => new P3(Math.Sign(X), Math.Sign(Y), Math.Sign(Z));

        public int Energy => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);

        public override string ToString() => $"(<x={X}, y={Y}, z={Z}>)";

        public override bool Equals(object? obj)
        {
            if (obj is P3)
            {
                return (P3)obj == this;
            }

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y, Z);

        public static bool operator ==(P3 left, P3 right)
        {
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        public static bool operator !=(P3 left, P3 right)
        {
            return !(left == right);
        }
    }
}
