using System.Reflection;
using BenchmarkDotNet.Attributes;
using CometFlavor.Reflection;

namespace BenchCometFlavor.Reflection;

public class BenchPropertyGetter
{
    public class Item
    {
        public string PropRef { get; set; } = "aaa";
        public int PropVal { get; set; } = 10;

        public static string StaticPropRef { get; set; } = "aaa";
        public static int StaticPropVal { get; set; } = 10;
    }

    [GlobalSetup]
    public void Setup()
    {
        this.Target = new();
        this.ReflectRef = typeof(Item).GetProperty(nameof(Item.PropRef))!;
        this.ReflectVal = typeof(Item).GetProperty(nameof(Item.PropVal))!;
        this.InstanceRefDirect = new Func<Item, object?>(o => o.PropRef);
        this.InstanceValDirect = new Func<Item, object?>(o => o.PropRef);
        this.InstanceRefGenerated = MemberAccessor.CreatePropertyGetter<Item>(nameof(Item.PropRef));
        this.InstanceValGenerated = MemberAccessor.CreatePropertyGetter<Item>(nameof(Item.PropVal));
        this.InstanceRefCompiled = MemberAccessor.CompilePropertyGetter<Item>(this.ReflectRef);
        this.InstanceValCompiled = MemberAccessor.CompilePropertyGetter<Item>(this.ReflectVal);
    }

    public Item Target { get; private set; } = default!;

    public PropertyInfo ReflectRef { get; private set; } = default!;
    public PropertyInfo ReflectVal { get; private set; } = default!;

    public Func<Item, object?> InstanceRefDirect { get; private set; } = default!;
    public Func<Item, object?> InstanceValDirect { get; private set; } = default!;

    public Func<Item?, object?> InstanceRefGenerated { get; private set; } = default!;
    public Func<Item?, object?> InstanceValGenerated { get; private set; } = default!;

    public Func<Item?, object?> InstanceRefCompiled { get; private set; } = default!;
    public Func<Item?, object?> InstanceValCompiled { get; private set; } = default!;

    [Benchmark]
    public void InstanceRefDirectDelegate() => this.InstanceRefDirect(this.Target);

    [Benchmark]
    public void InstanceValDirectDelegate() => this.InstanceValDirect(this.Target);

    [Benchmark]
    public void Reflect() => typeof(Item).GetProperty(nameof(Item.PropRef))!.GetValue(this.Target);

    [Benchmark]
    public void InstanceRefReflect() => this.ReflectRef.GetValue(this.Target);

    [Benchmark]
    public void InstanceValReflect() => this.ReflectVal.GetValue(this.Target);

    [Benchmark]
    public void GenerateDelegate() => MemberAccessor.CreatePropertyGetter<Item>(nameof(Item.PropRef))(this.Target);

    [Benchmark]
    public void InstanceRefGeneratedDelegate() => this.InstanceRefGenerated(this.Target);

    [Benchmark]
    public void InstanceValGeneratedDelegate() => this.InstanceValGenerated(this.Target);

    [Benchmark]
    public void CompileDelegate() => MemberAccessor.CompilePropertyGetter<Item>(this.ReflectRef)(this.Target);

    [Benchmark]
    public void InstanceRefCompiledDelegate() => this.InstanceRefCompiled(this.Target);

    [Benchmark]
    public void InstanceValCompiledDelegate() => this.InstanceValCompiled(this.Target);
}
