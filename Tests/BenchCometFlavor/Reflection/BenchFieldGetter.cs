using System.Reflection;
using BenchmarkDotNet.Attributes;
using CometFlavor.Reflection;

namespace BenchCometFlavor.Reflection;

public class BenchFieldGetter
{
    public class Item
    {
        public string FieldRef = "aaa";
        public int FieldVal = 10;

        public static string StaticFieldRef { get; set; } = "aaa";
        public static int StaticFieldVal { get; set; } = 10;
    }

    [GlobalSetup]
    public void Setup()
    {
        this.Target = new();
        this.ReflectRef = typeof(Item).GetField(nameof(Item.FieldRef))!;
        this.ReflectVal = typeof(Item).GetField(nameof(Item.FieldVal))!;
        this.InstanceRefDirect = new Func<Item, object?>(o => o.FieldRef);
        this.InstanceValDirect = new Func<Item, object?>(o => o.FieldRef);
        this.InstanceRefCompiled = MemberAccessor.CompileFieldGetter<Item>(this.ReflectRef);
        this.InstanceValCompiled = MemberAccessor.CompileFieldGetter<Item>(this.ReflectVal);
    }

    public Item Target { get; private set; } = default!;

    public FieldInfo ReflectRef { get; private set; } = default!;
    public FieldInfo ReflectVal { get; private set; } = default!;

    public Func<Item, object?> InstanceRefDirect { get; private set; } = default!;
    public Func<Item, object?> InstanceValDirect { get; private set; } = default!;

    public Func<Item?, object?> InstanceRefCompiled { get; private set; } = default!;
    public Func<Item?, object?> InstanceValCompiled { get; private set; } = default!;

    [Benchmark]
    public void InstanceRefDirectDelegate() => this.InstanceRefDirect(this.Target);

    [Benchmark]
    public void InstanceValDirectDelegate() => this.InstanceValDirect(this.Target);

    [Benchmark]
    public void Reflect() => typeof(Item).GetField(nameof(Item.FieldRef))!.GetValue(this.Target);

    [Benchmark]
    public void InstanceRefReflect() => this.ReflectRef.GetValue(this.Target);

    [Benchmark]
    public void InstanceValReflect() => this.ReflectVal.GetValue(this.Target);

    [Benchmark]
    public void CompileDelegate() => MemberAccessor.CompileFieldGetter<Item>(this.ReflectRef)(this.Target);

    [Benchmark]
    public void InstanceRefCompiledDelegate() => this.InstanceRefCompiled(this.Target);

    [Benchmark]
    public void InstanceValCompiledDelegate() => this.InstanceValCompiled(this.Target);
}
