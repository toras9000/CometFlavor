using System;
using CometFlavor.Utility;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestCometFlavor.Utility;

[TestClass()]
public class ProcCounterTests
{
    [TestMethod()]
    public void Constructor()
    {
        new ProcCounter(5).Threshold.Should().Be(5);
        new ProcCounter(100).Threshold.Should().Be(100);
        new ProcCounter(0).Threshold.Should().Be(0);
    }

    [TestMethod()]
    public void Entry()
    {
        var counter = new ProcCounter(5);

        counter.Entry();
        counter.Status.Total.Should().Be(1);
        counter.Status.Successful.Should().Be(0);
        counter.Status.Failed.Should().Be(0);
        counter.Status.Unknown.Should().Be(0);

        counter.Entry();
        counter.Status.Total.Should().Be(2);
        counter.Status.Successful.Should().Be(0);
        counter.Status.Failed.Should().Be(0);
        counter.Status.Unknown.Should().Be(1);

        counter.Entry();
        counter.Status.Total.Should().Be(3);
        counter.Status.Successful.Should().Be(0);
        counter.Status.Failed.Should().Be(0);
        counter.Status.Unknown.Should().Be(2);
    }

    [TestMethod()]
    public void Success()
    {
        var counter = new ProcCounter(5);

        counter.Success();
        counter.Status.Total.Should().Be(1);
        counter.Status.Successful.Should().Be(1);
        counter.Status.Failed.Should().Be(0);
        counter.Status.Unknown.Should().Be(0);

        counter.Success();
        counter.Status.Total.Should().Be(2);
        counter.Status.Successful.Should().Be(2);
        counter.Status.Failed.Should().Be(0);
        counter.Status.Unknown.Should().Be(0);

        counter.Success();
        counter.Status.Total.Should().Be(3);
        counter.Status.Successful.Should().Be(3);
        counter.Status.Failed.Should().Be(0);
        counter.Status.Unknown.Should().Be(0);
    }

    [TestMethod()]
    public void Fail()
    {
        var counter = new ProcCounter(5);

        counter.Fail();
        counter.Status.Total.Should().Be(1);
        counter.Status.Successful.Should().Be(0);
        counter.Status.Failed.Should().Be(1);
        counter.Status.Unknown.Should().Be(0);

        counter.Fail();
        counter.Status.Total.Should().Be(2);
        counter.Status.Successful.Should().Be(0);
        counter.Status.Failed.Should().Be(2);
        counter.Status.Unknown.Should().Be(0);

        counter.Fail();
        counter.Status.Total.Should().Be(3);
        counter.Status.Successful.Should().Be(0);
        counter.Status.Failed.Should().Be(3);
        counter.Status.Unknown.Should().Be(0);
    }

    [TestMethod()]
    public void CounterScenario()
    {
        var counter = new ProcCounter(5);

        counter.Entry();
        counter.Status.Total.Should().Be(1);
        counter.Status.Successful.Should().Be(0);
        counter.Status.Failed.Should().Be(0);
        counter.Status.Unknown.Should().Be(0);

        counter.Success();
        counter.Status.Total.Should().Be(1);
        counter.Status.Successful.Should().Be(1);
        counter.Status.Failed.Should().Be(0);
        counter.Status.Unknown.Should().Be(0);

        counter.Entry();
        counter.Status.Total.Should().Be(2);
        counter.Status.Successful.Should().Be(1);
        counter.Status.Failed.Should().Be(0);
        counter.Status.Unknown.Should().Be(0);

        counter.Fail();
        counter.Status.Total.Should().Be(2);
        counter.Status.Successful.Should().Be(1);
        counter.Status.Failed.Should().Be(1);
        counter.Status.Unknown.Should().Be(0);
    }

    [TestMethod()]
    public void Progress_ByEntry()
    {
        var counter = new ProcCounter(3);
        using (var mon = counter.Monitor())
        {
            counter.Entry(); mon.Should().NotRaise(nameof(counter.Progress));
            counter.Entry(); mon.Should().NotRaise(nameof(counter.Progress));   // ここで Unknown が 1 になる。
            counter.Entry(); mon.Should().NotRaise(nameof(counter.Progress));
            counter.Entry(); mon.Should().Raise(nameof(counter.Progress));      // ここで Unknown が 3 になる。

            mon.Clear();

            counter.Entry(); mon.Should().NotRaise(nameof(counter.Progress));
            counter.Entry(); mon.Should().NotRaise(nameof(counter.Progress));
            counter.Entry(); mon.Should().Raise(nameof(counter.Progress));
        }
    }

    [TestMethod()]
    public void Progress_ByResult()
    {
        var counter = new ProcCounter(3);
        using (var mon = counter.Monitor())
        {
            counter.Success(); mon.Should().NotRaise(nameof(counter.Progress));
            counter.Success(); mon.Should().NotRaise(nameof(counter.Progress));
            counter.Success(); mon.Should().Raise(nameof(counter.Progress));      // ここで Unknown が 3 になる。

            mon.Clear();

            counter.Fail(); mon.Should().NotRaise(nameof(counter.Progress));
            counter.Fail(); mon.Should().NotRaise(nameof(counter.Progress));
            counter.Fail(); mon.Should().Raise(nameof(counter.Progress));

            mon.Clear();

            counter.Success(); mon.Should().NotRaise(nameof(counter.Progress));
            counter.Fail(); mon.Should().NotRaise(nameof(counter.Progress));
            counter.Success(); mon.Should().Raise(nameof(counter.Progress));
        }
    }

    [TestMethod()]
    public void Progress_None()
    {
        var counter = new ProcCounter(0);
        using (var mon = counter.Monitor())
        {
            counter.Entry();
            counter.Entry();
            counter.Entry();
            counter.Success();
            counter.Success();
            counter.Success();
            counter.Fail();
            counter.Fail();
            counter.Fail();

            mon.Should().NotRaise(nameof(counter.Progress));
        }
    }

}
