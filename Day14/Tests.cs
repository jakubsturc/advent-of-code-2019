using System.Linq;
using Xunit;

namespace JakubSturc.AdventOfCode2019.Day14
{
    public partial class Tests
    {
        [Theory]
        [InlineData("10 ORE => 10 A")]
        [InlineData("1 ORE => 1 B")]
        [InlineData("7 A, 1 B => 1 C")]
        [InlineData("7 A, 1 E => 1 FUEL")]
        [InlineData("1 HDTJ, 1 LBZJ, 1 SLPCX, 5 SMCGZ, 3 MFMX, 4 CHZT, 12 BKBCB => 1 HRNSK")]
        public void Parse_ToString_Idenity(string str)
        {
            var reaction = Reaction.Parse(str);
            Assert.Equal(str, reaction.ToString());
        }

        [Fact]
        public void Part1_Sample1()
        {
            var reactions = new Reaction[]
            {
                Reaction.Parse("10 ORE => 10 A"),
                Reaction.Parse("1 ORE => 1 B"),
                Reaction.Parse("7 A, 1 B => 1 C"),
                Reaction.Parse("7 A, 1 C => 1 D"),
                Reaction.Parse("7 A, 1 D => 1 E"),
                Reaction.Parse("7 A, 1 E => 1 FUEL")
            };

            var factory = new NanoFactory(reactions);
            var res = factory.GetOreRequirementFor(new ReactionItem(Chemical.FUEL, 1));
            Assert.Equal(31, res);
        }

        [Fact]
        public void Part1_Sample2()
        {
            var reactions = new Reaction[]
            {
                Reaction.Parse("9 ORE => 2 A"),
                Reaction.Parse("8 ORE => 3 B"),
                Reaction.Parse("7 ORE => 5 C"),
                Reaction.Parse("3 A, 4 B => 1 AB"),
                Reaction.Parse("5 B, 7 C => 1 BC"),
                Reaction.Parse("4 C, 1 A => 1 CA"),
                Reaction.Parse("2 AB, 3 BC, 4 CA => 1 FUEL")
            };

            var factory = new NanoFactory(reactions);
            var res = factory.GetOreRequirementFor(new ReactionItem(Chemical.FUEL, 1));
            Assert.Equal(165, res);
        }
    }
}
