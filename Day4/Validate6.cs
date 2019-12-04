namespace JakubSturc.AdventOfCode2019.Day4
{
    public static class Validate6
    {
        public static bool NeverDecrease(int[] arr)
        {
            return arr[0] <= arr[1]
                && arr[1] <= arr[2]
                && arr[2] <= arr[3]
                && arr[3] <= arr[4]
                && arr[4] <= arr[5];
        }

        public static bool AtLeastTwoAdjacentDigitsAreSame(int[] arr)
        {
            return arr[0] == arr[1]
                || arr[1] == arr[2]
                || arr[2] == arr[3]
                || arr[3] == arr[4]
                || arr[4] == arr[5];
        }

        public static bool TwoAdjacentDigitsAreSame(int[] arr)
        {
            return (                    arr[0] == arr[1] && arr[1] != arr[2])
                || (arr[0] != arr[1] && arr[1] == arr[2] && arr[2] != arr[3])
                || (arr[1] != arr[2] && arr[2] == arr[3] && arr[3] != arr[4])
                || (arr[2] != arr[3] && arr[3] == arr[4] && arr[4] != arr[5])
                || (arr[3] != arr[4] && arr[4] == arr[5]);
        }
    }
}
