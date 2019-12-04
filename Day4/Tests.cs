using Xunit;

namespace JakubSturc.AdventOfCode2019.Day4
{
    public class Tests
    {
        private static int[] arr3 = new int[3];

        [Theory]
        [InlineData(123456, new int[] { 1, 2, 3, 4, 5, 6 })]
        [InlineData(0, new int[] { 0, 0, 0, 0, 0, 0 })]
        [InlineData(1_000_001, new int[] { 0, 0, 0, 0, 0, 1 })]
        public void Split6_Works_As_Expected(int num, int[] digits)
        {
            Assert.Equal(digits, num.Split(6));
        }

        [Theory]
        [InlineData(111, new int[] { 1, 1, 1 })]
        [InlineData(123, new int[] { 1, 2, 3 })]
        [InlineData(321, new int[] { 3, 2, 1 })]
        public void Split_With_Array_Reuse(int num, int[] digits)
        {
            Assert.Equal(digits, num.SplitInto(arr3));
        }

        [Theory]
        [InlineData(111111)]
        [InlineData(123345)]
        [InlineData(245677)]
        public void Two_Adjacent_Digits_Are_Same_6(int num)
        {
            Assert.True(Validate6.AtLeastTwoAdjacentDigitsAreSame(num.Split(6)));
        }

        [Theory]
        [InlineData(123456)]
        [InlineData(245679)]
        public void Two_Adjacent_Digits_Are_Not_Same_6(int num)
        {
            Assert.False(Validate6.AtLeastTwoAdjacentDigitsAreSame(num.Split(6)));
        }

        [Theory]
        [InlineData(111111)]
        [InlineData(123345)]
        [InlineData(245677)]
        public void Never_Decrease_6(int num)
        {
            Assert.True(Validate6.NeverDecrease(num.Split(6)));
        }

        [Theory]
        [InlineData(224156)]
        [InlineData(245769)]
        public void Sometimes_Increase_6(int num)
        {
            Assert.False(Validate6.NeverDecrease(num.Split(6)));
        }

        [Theory]
        [InlineData(112233)]
        [InlineData(123345)]
        [InlineData(123445)]
        [InlineData(123455)]
        [InlineData(111122)]
        public void TwoAdjacentDigitsAreSame_Positive(int num)
        {
            Assert.True(Validate6.TwoAdjacentDigitsAreSame(num.Split(6)));
        }

        [Theory]
        [InlineData(123444)]
        [InlineData(123478)]
        [InlineData(111110)]
        [InlineData(129999)]
        public void TwoAdjacentDigitsAreSame_Negative(int num)
        {
            Assert.False(Validate6.TwoAdjacentDigitsAreSame(num.Split(6)));
        }
    }
}
