namespace JakubSturc.AdventOfCode2019.Day11
{
    public interface IView
    {
        public static readonly IView Null = new NullView();

        void PrintTile(int x, int y, Color value);
        void PrintRobot(int x, int y, Direction dir);

        private class NullView : IView
        {
            public void PrintRobot(int x, int y, Direction dir) { }

            public void PrintTile(int x, int y, Color value) { }
        }

    }
}