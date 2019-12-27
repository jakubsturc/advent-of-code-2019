using System;
using System.Collections.Generic;
using Xunit;

namespace Day24
{
    public class Tests
    {
        [Fact]
        public void BinaryShift()
        {
            Assert.Equal(2L, 1L << 1);
        }
    }
}
