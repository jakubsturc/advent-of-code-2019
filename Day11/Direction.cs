using System;

namespace JakubSturc.AdventOfCode2019.Day11
{
    public abstract class Direction
    {
        public static readonly Direction N = new North();
        public static readonly Direction E = new East();
        public static readonly Direction S = new South();
        public static readonly Direction W = new West();

        private Direction() { }

        public abstract char Code { get; }
        public abstract Direction TurnLeft();
        public abstract Direction TurnRight();
        public abstract void Move(ref int x, ref int y);

        public Direction Turn(Side side)
        {
            return side switch
            {
                Side.Left => TurnLeft(),
                Side.Right => TurnRight(),
                _ => throw new NotSupportedException()
            };
        }

        private class North : Direction
        {
            public override char Code => '^';
            public override void Move(ref int x, ref int y) => y--;

            public override Direction TurnLeft() => W;

            public override Direction TurnRight() => E;
        }

        private class East : Direction
        {
            public override char Code => '>';
            public override void Move(ref int x, ref int y) => x++;

            public override Direction TurnLeft() => N;

            public override Direction TurnRight() => S;
        }

        private class South : Direction
        {
            public override char Code => 'v';
            public override void Move(ref int x, ref int y) => y++;

            public override Direction TurnLeft() => E;

            public override Direction TurnRight() => W;
        }

        private class West : Direction
        {
            public override char Code => '<';
            public override void Move(ref int x, ref int y) => x--;

            public override Direction TurnLeft() => S;

            public override Direction TurnRight() => N;
        }
    }
}
