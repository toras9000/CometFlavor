using System.Collections.Generic;
using CometFlavor.Extensions.Linq;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Extensions.Linq;

[TestClass]
public class EnumerableExtensionsTests
{
    [TestMethod]
    public void WhereElse()
    {
        var source = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, };
        var skipped = new List<int>();

        source.WhereElse(n => n % 2 == 0, n => skipped.Add(n))
            .Should().Equal(new[] { 2, 4, 6, 8, });

        skipped.Should().Equal(new[] { 1, 3, 5, 7, 9, });
    }
}
