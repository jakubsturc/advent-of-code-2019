namespace JakubSturc.AdventOfCode2019.Day12
{
    public struct Moon
    {
        public P3 Position { get; set; }
        public P3 Velocity { get; set; }

        public void ApplyVelocity()
        {
            Position += Velocity;
        }

        public void ApplyGravity(P3[] poss)
        { 
            for (int i = 0; i < poss.Length; i++)
            {
                Velocity += (poss[i] - Position).Sign();
            }
        }

        public int Energy => Position.Energy * Velocity.Energy;
    }
}
