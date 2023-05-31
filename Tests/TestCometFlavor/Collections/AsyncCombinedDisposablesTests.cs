using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CometFlavor.Collections;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestCometFlavor.Collections;

[TestClass]
public class AsyncCombinedDisposablesTests
{
    [TestMethod]
    public void Constructor_Default()
    {
        var target = new AsyncCombinedDisposables();
        target.ReverseDispose.Should().Be(true);
        target.DisposeOnRemove.Should().Be(false);
        target.LatestException.Should().BeNull();
        target.IsDisposed.Should().Be(false);
        target.IsReadOnly.Should().Be(false);
        target.Count.Should().Be(0);
        target.Should().BeEmpty();
    }

    [TestMethod]
    public void Constructor_Param1()
    {
        {
            var target = new AsyncCombinedDisposables(true);
            target.ReverseDispose.Should().Be(true);
            target.DisposeOnRemove.Should().Be(false);
            target.LatestException.Should().BeNull();
            target.IsDisposed.Should().Be(false);
            target.IsReadOnly.Should().Be(false);
            target.Count.Should().Be(0);
            target.Should().BeEmpty();
        }
        {
            var target = new AsyncCombinedDisposables(false);
            target.ReverseDispose.Should().Be(false);
            target.DisposeOnRemove.Should().Be(false);
            target.LatestException.Should().BeNull();
            target.IsDisposed.Should().Be(false);
            target.IsReadOnly.Should().Be(false);
            target.Count.Should().Be(0);
            target.Should().BeEmpty();
        }
    }

    [TestMethod]
    public void Constructor_Param2()
    {
        {
            var target = new AsyncCombinedDisposables(true, true);
            target.ReverseDispose.Should().Be(true);
            target.DisposeOnRemove.Should().Be(true);
            target.LatestException.Should().BeNull();
            target.IsDisposed.Should().Be(false);
            target.IsReadOnly.Should().Be(false);
            target.Count.Should().Be(0);
            target.Should().BeEmpty();
        }
        {
            var target = new AsyncCombinedDisposables(true, false);
            target.ReverseDispose.Should().Be(true);
            target.DisposeOnRemove.Should().Be(false);
            target.LatestException.Should().BeNull();
            target.IsDisposed.Should().Be(false);
            target.IsReadOnly.Should().Be(false);
            target.Count.Should().Be(0);
            target.Should().BeEmpty();
        }
        {
            var target = new AsyncCombinedDisposables(false, true);
            target.ReverseDispose.Should().Be(false);
            target.DisposeOnRemove.Should().Be(true);
            target.LatestException.Should().BeNull();
            target.IsDisposed.Should().Be(false);
            target.IsReadOnly.Should().Be(false);
            target.Count.Should().Be(0);
            target.Should().BeEmpty();
        }
        {
            var target = new AsyncCombinedDisposables(false, false);
            target.ReverseDispose.Should().Be(false);
            target.DisposeOnRemove.Should().Be(false);
            target.LatestException.Should().BeNull();
            target.IsDisposed.Should().Be(false);
            target.IsReadOnly.Should().Be(false);
            target.Count.Should().Be(0);
            target.Should().BeEmpty();
        }
    }

    [TestMethod]
    public async Task AddAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();

        var target = new AsyncCombinedDisposables();
        await target.AddAsync(item1.Object);
        target.Count.Should().Be(1);
        await target.AddAsync(item2.Object);
        target.Count.Should().Be(2);
        target.Should().Equal(item1.Object, item2.Object);
    }

    [TestMethod]
    public async Task AddAsync_ArgNull()
    {
        var target = new AsyncCombinedDisposables();

        await FluentActions.Awaiting(() => target.AddAsync(null).AsTask()).Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod]
    public async Task RemoveAsync_NoDispose()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var target = new AsyncCombinedDisposables(reverse: true, removeDispose: false);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);
        target.Should().HaveCount(5);

        target.DisposeOnRemove.Should().Be(false, "念のため前提を確認");

        (await target.RemoveAsync(item2.Object)).Should().Be(true);
        target.Should().Equal(
            item1.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );
        item2.Verify(o => o.DisposeAsync(), Times.Never());

        (await target.RemoveAsync(item2.Object)).Should().Be(false, "除去済みオブジェクトを再指定");
        target.Should().Equal(
            item1.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );
        item2.Verify(o => o.DisposeAsync(), Times.Never());

        (await target.RemoveAsync(item1.Object)).Should().Be(true);
        (await target.RemoveAsync(item3.Object)).Should().Be(true);
        (await target.RemoveAsync(item4.Object)).Should().Be(true);
        (await target.RemoveAsync(item5.Object)).Should().Be(true);
        target.Should().HaveCount(0);
        item1.Verify(o => o.DisposeAsync(), Times.Never());
        item3.Verify(o => o.DisposeAsync(), Times.Never());
        item4.Verify(o => o.DisposeAsync(), Times.Never());
        item5.Verify(o => o.DisposeAsync(), Times.Never());
    }

    [TestMethod]
    public async Task RemoveAsync_WithDispose()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var target = new AsyncCombinedDisposables(reverse: true, removeDispose: true);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);
        target.Should().HaveCount(5);

        target.DisposeOnRemove.Should().Be(true, "念のため前提を確認");

        (await target.RemoveAsync(item2.Object)).Should().Be(true);
        target.Should().Equal(
            item1.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );
        item2.Verify(o => o.DisposeAsync(), Times.Once());

        (await target.RemoveAsync(item2.Object)).Should().Be(false, "除去済みオブジェクトを再指定");
        target.Should().Equal(
            item1.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );
        item2.Verify(o => o.DisposeAsync(), Times.Exactly(2));

        (await target.RemoveAsync(item1.Object)).Should().Be(true);
        (await target.RemoveAsync(item3.Object)).Should().Be(true);
        (await target.RemoveAsync(item4.Object)).Should().Be(true);
        (await target.RemoveAsync(item5.Object)).Should().Be(true);
        target.Should().HaveCount(0);
        item1.Verify(o => o.DisposeAsync(), Times.Once());
        item3.Verify(o => o.DisposeAsync(), Times.Once());
        item4.Verify(o => o.DisposeAsync(), Times.Once());
        item5.Verify(o => o.DisposeAsync(), Times.Once());

        var item6 = new Mock<IAsyncDisposable>();
        (await target.RemoveAsync(item6.Object)).Should().Be(false);
        item6.Verify(o => o.DisposeAsync(), Times.Once());
    }

    [TestMethod]
    public async Task RemoveAsync_ArgNullAsync()
    {
        var target = new AsyncCombinedDisposables();

        await FluentActions.Awaiting(() => target.RemoveAsync(null).AsTask()).Should().ThrowAsync<ArgumentNullException>();
    }

    [TestMethod]
    public async Task ClearAsync_NoDisposeAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var target = new AsyncCombinedDisposables();
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);
        target.Should().HaveCount(5);

        target.DisposeOnRemove.Should().Be(false, "念のため前提を確認");
        await target.ClearAsync();
        target.Should().HaveCount(0);

        item1.Verify(o => o.DisposeAsync(), Times.Never());
        item2.Verify(o => o.DisposeAsync(), Times.Never());
        item3.Verify(o => o.DisposeAsync(), Times.Never());
        item4.Verify(o => o.DisposeAsync(), Times.Never());
        item5.Verify(o => o.DisposeAsync(), Times.Never());
    }

    [TestMethod]
    public async Task ClearAsync_WithDispose_ForwardAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var log = new List<IAsyncDisposable>();
        item1.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item1.Object));
        item2.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item2.Object));
        item3.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item3.Object));
        item4.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item4.Object));
        item5.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item5.Object));

        var target = new AsyncCombinedDisposables(reverse: false, removeDispose: true);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);
        target.Should().HaveCount(5);

        target.DisposeOnRemove.Should().Be(true, "念のため前提を確認");
        await target.ClearAsync();
        target.Should().HaveCount(0);
        target.IsDisposed.Should().Be(false);

        item1.Verify(o => o.DisposeAsync(), Times.Once());
        item2.Verify(o => o.DisposeAsync(), Times.Once());
        item3.Verify(o => o.DisposeAsync(), Times.Once());
        item4.Verify(o => o.DisposeAsync(), Times.Once());
        item5.Verify(o => o.DisposeAsync(), Times.Once());

        log.Should().Equal(
            item1.Object,
            item2.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );
    }

    [TestMethod]
    public async Task ClearAsync_WithDispose_ReverseAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var log = new List<IAsyncDisposable>();
        item1.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item1.Object));
        item2.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item2.Object));
        item3.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item3.Object));
        item4.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item4.Object));
        item5.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item5.Object));

        var target = new AsyncCombinedDisposables(reverse: true, removeDispose: true);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);
        target.Should().HaveCount(5);

        target.DisposeOnRemove.Should().Be(true, "念のため前提を確認");
        await target.ClearAsync();
        target.Should().HaveCount(0);
        target.IsDisposed.Should().Be(false);

        item1.Verify(o => o.DisposeAsync(), Times.Once());
        item2.Verify(o => o.DisposeAsync(), Times.Once());
        item3.Verify(o => o.DisposeAsync(), Times.Once());
        item4.Verify(o => o.DisposeAsync(), Times.Once());
        item5.Verify(o => o.DisposeAsync(), Times.Once());

        log.Should().Equal(
            item5.Object,
            item4.Object,
            item3.Object,
            item2.Object,
            item1.Object
        );
    }

    [TestMethod]
    public async Task ContainsAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var target = new AsyncCombinedDisposables(reverse: true, removeDispose: false);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);
        target.Should().HaveCount(5);

        await target.RemoveAsync(item2.Object);
        target.Contains(item1.Object).Should().Be(true);
        target.Contains(item2.Object).Should().Be(false);
        target.Contains(item3.Object).Should().Be(true);
        target.Contains(item4.Object).Should().Be(true);
        target.Contains(item5.Object).Should().Be(true);

        await target.RemoveAsync(item2.Object);    // 同じオブジェクトを再度指定
        target.Contains(item1.Object).Should().Be(true);
        target.Contains(item2.Object).Should().Be(false);
        target.Contains(item3.Object).Should().Be(true);
        target.Contains(item4.Object).Should().Be(true);
        target.Contains(item5.Object).Should().Be(true);

        await target.RemoveAsync(item1.Object);
        await target.RemoveAsync(item3.Object);
        await target.RemoveAsync(item4.Object);
        await target.RemoveAsync(item5.Object);
        target.Contains(item1.Object).Should().Be(false);
        target.Contains(item2.Object).Should().Be(false);
        target.Contains(item3.Object).Should().Be(false);
        target.Contains(item4.Object).Should().Be(false);
        target.Contains(item5.Object).Should().Be(false);
    }

    [TestMethod]
    public void Contains_ArgNull()
    {
        var target = new AsyncCombinedDisposables();

        new Action(() => target.Contains(null)).Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public async Task EnumerateAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var target = new AsyncCombinedDisposables(reverse: true, removeDispose: false);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);

        target.AsEnumerable().Should().Equal(
            item1.Object,
            item2.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );

        // for Un generic enumeration
        var ungeneric = new List<object>();
        foreach (var obj in ((System.Collections.IEnumerable)target))
        {
            ungeneric.Add(obj);
        }
        ungeneric.Should().Equal(
            item1.Object,
            item2.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );
    }

    [TestMethod]
    public async Task CopyToAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var target = new AsyncCombinedDisposables(reverse: true, removeDispose: false);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);

        var source = (ICollection<IAsyncDisposable>)target;

        var dest1 = new IAsyncDisposable[5];
        source.CopyTo(dest1, 0);
        dest1.Should().Equal(
            item1.Object,
            item2.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );

        var dest2 = new IAsyncDisposable[10];
        source.CopyTo(dest2, 3);
        dest2.Should().Equal(
            null,
            null,
            null,
            item1.Object,
            item2.Object,
            item3.Object,
            item4.Object,
            item5.Object,
            null,
            null
        );
    }

    [TestMethod]
    public async Task Dispose_ForwardAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var log = new List<IAsyncDisposable>();
        item1.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item1.Object));
        item2.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item2.Object));
        item3.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item3.Object));
        item4.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item4.Object));
        item5.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item5.Object));

        var target = new AsyncCombinedDisposables(reverse: false, removeDispose: false);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);

        await target.DisposeAsync();

        target.Should().HaveCount(0);
        target.IsDisposed.Should().Be(true);
        item1.Verify(o => o.DisposeAsync(), Times.Once());
        item2.Verify(o => o.DisposeAsync(), Times.Once());
        item3.Verify(o => o.DisposeAsync(), Times.Once());
        item4.Verify(o => o.DisposeAsync(), Times.Once());
        item5.Verify(o => o.DisposeAsync(), Times.Once());

        log.Should().Equal(
            item1.Object,
            item2.Object,
            item3.Object,
            item4.Object,
            item5.Object
        );

        var item6 = new Mock<IAsyncDisposable>();
        await target.AddAsync(item6.Object);
        item6.Verify(o => o.DisposeAsync(), Times.Once());
    }

    [TestMethod]
    public async Task Dispose_ReverseAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var log = new List<IAsyncDisposable>();
        item1.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item1.Object));
        item2.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item2.Object));
        item3.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item3.Object));
        item4.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item4.Object));
        item5.Setup(o => o.DisposeAsync()).Callback(() => log.Add(item5.Object));

        var target = new AsyncCombinedDisposables(reverse: true, removeDispose: false);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);

        await target.DisposeAsync();

        target.Should().HaveCount(0);
        target.IsDisposed.Should().Be(true);
        item1.Verify(o => o.DisposeAsync(), Times.Once());
        item2.Verify(o => o.DisposeAsync(), Times.Once());
        item3.Verify(o => o.DisposeAsync(), Times.Once());
        item4.Verify(o => o.DisposeAsync(), Times.Once());
        item5.Verify(o => o.DisposeAsync(), Times.Once());

        log.Should().Equal(
            item5.Object,
            item4.Object,
            item3.Object,
            item2.Object,
            item1.Object
        );

        var item6 = new Mock<IAsyncDisposable>();
        await target.AddAsync(item6.Object);
        item6.Verify(o => o.DisposeAsync(), Times.Once());
    }

    [TestMethod]
    public async Task LatestException_RemoveAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var ex2 = new Exception("2");
        item2.Setup(o => o.DisposeAsync()).Callback(() => throw ex2);

        var ex4 = new Exception("4");
        item4.Setup(o => o.DisposeAsync()).Callback(() => throw ex4);

        var target = new AsyncCombinedDisposables(reverse: true, removeDispose: true);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);

        (await target.RemoveAsync(item1.Object)).Should().Be(true);
        target.LatestException.Should().BeNull();

        (await target.RemoveAsync(item2.Object)).Should().Be(true);
        target.LatestException.Should().BeSameAs(ex2);

        (await target.RemoveAsync(item3.Object)).Should().Be(true);
        target.LatestException.Should().BeNull();

        (await target.RemoveAsync(item4.Object)).Should().Be(true);
        target.LatestException.Should().BeSameAs(ex4);

        (await target.RemoveAsync(item5.Object)).Should().Be(true);
        target.LatestException.Should().BeNull();
    }

    [TestMethod]
    public async Task LatestException_Clear_SingleAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var ex4 = new Exception("4");
        item4.Setup(o => o.DisposeAsync()).Callback(() => throw ex4);

        var target = new AsyncCombinedDisposables(reverse: false, removeDispose: true);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);

        await target.ClearAsync();
        target.LatestException.Should()
            .BeOfType<Exception>()
            .Which.Should().BeSameAs(ex4);
    }

    [TestMethod]
    public async Task LatestException_Clear_ForwardAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var ex2 = new Exception("2");
        item2.Setup(o => o.DisposeAsync()).Callback(() => throw ex2);

        var ex4 = new Exception("4");
        item4.Setup(o => o.DisposeAsync()).Callback(() => throw ex4);

        var target = new AsyncCombinedDisposables(reverse: false, removeDispose: true);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);

        await target.ClearAsync();
        target.LatestException.Should()
            .BeOfType<AggregateException>()
            .Which.InnerExceptions.Should().Equal(ex2, ex4);
    }

    [TestMethod]
    public async Task LatestException_Clear_ReverseAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var ex2 = new Exception("2");
        item2.Setup(o => o.DisposeAsync()).Callback(() => throw ex2);

        var ex4 = new Exception("4");
        item4.Setup(o => o.DisposeAsync()).Callback(() => throw ex4);

        var target = new AsyncCombinedDisposables(reverse: true, removeDispose: true);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);

        await target.ClearAsync();
        target.LatestException.Should()
            .BeOfType<AggregateException>()
            .Which.InnerExceptions.Should().Equal(ex4, ex2);
    }

    [TestMethod]
    public async Task LatestException_Dispose_ForwardAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var ex2 = new Exception("2");
        item2.Setup(o => o.DisposeAsync()).Callback(() => throw ex2);

        var ex4 = new Exception("4");
        item4.Setup(o => o.DisposeAsync()).Callback(() => throw ex4);

        var target = new AsyncCombinedDisposables(reverse: false, removeDispose: true);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);

        await target.DisposeAsync();
        target.LatestException.Should()
            .BeOfType<AggregateException>()
            .Which.InnerExceptions.Should().Equal(ex2, ex4);
    }

    [TestMethod]
    public async Task LatestException_Dispose_ReserveAsync()
    {
        var item1 = new Mock<IAsyncDisposable>();
        var item2 = new Mock<IAsyncDisposable>();
        var item3 = new Mock<IAsyncDisposable>();
        var item4 = new Mock<IAsyncDisposable>();
        var item5 = new Mock<IAsyncDisposable>();

        var ex2 = new Exception("2");
        item2.Setup(o => o.DisposeAsync()).Callback(() => throw ex2);

        var ex4 = new Exception("4");
        item4.Setup(o => o.DisposeAsync()).Callback(() => throw ex4);

        var target = new AsyncCombinedDisposables(reverse: true, removeDispose: true);
        await target.AddAsync(item1.Object);
        await target.AddAsync(item2.Object);
        await target.AddAsync(item3.Object);
        await target.AddAsync(item4.Object);
        await target.AddAsync(item5.Object);

        await target.DisposeAsync();
        target.LatestException.Should()
            .BeOfType<AggregateException>()
            .Which.InnerExceptions.Should().Equal(ex4, ex2);
    }
}