using System.Reflection;
using BenchmarkDotNet.Attributes;
using CometFlavor.Reflection;

namespace BenchCometFlavor.Reflection;

public class BenchCreatePropertyGetter
{
    public class ClassItem
    {
        public string RefProp { get; set; } = "aaa";
        public int ValProp { get; set; } = 10;
    }

    public struct StructItem
    {
        public StructItem() { }
        public string RefProp { get; set; } = "aaa";
        public int ValProp { get; set; } = 10;
    }

    [GlobalSetup]
    public void Setup()
    {
        this.ClassTarget = new();
        this.StructTarget = new();

        this.CacheDirectDelegateClassRefProp = new Func<ClassItem, object?>(o => o.RefProp);
        this.CacheDirectDelegateClassValProp = new Func<ClassItem, object?>(o => o.RefProp);
        this.CacheReflectionClassRefProp = typeof(ClassItem).GetProperty(nameof(ClassItem.RefProp))!;
        this.CacheReflectionClassValProp = typeof(ClassItem).GetProperty(nameof(ClassItem.ValProp))!;
        this.CacheCreatePropertyGetterClassRefProp = MemberAccessor.CreatePropertyGetter<ClassItem>(nameof(ClassItem.RefProp));
        this.CacheCreatePropertyGetterClassValProp = MemberAccessor.CreatePropertyGetter<ClassItem>(nameof(ClassItem.ValProp));
        this.CacheGeneratePropertyGetterClassRefProp = MemberAccessor.GeneratePropertyGetter<ClassItem>(nameof(ClassItem.RefProp));
        this.CacheGeneratePropertyGetterClassValProp = MemberAccessor.GeneratePropertyGetter<ClassItem>(nameof(ClassItem.ValProp));
        this.CacheCompilePropertyGetterClassRefProp = MemberAccessor.CompilePropertyGetter<ClassItem>(nameof(ClassItem.RefProp));
        this.CacheCompilePropertyGetterClassValProp = MemberAccessor.CompilePropertyGetter<ClassItem>(nameof(ClassItem.ValProp));

        this.CacheDirectDelegateStructRefProp = new Func<StructItem, object?>(o => o.RefProp);
        this.CacheDirectDelegateStructValProp = new Func<StructItem, object?>(o => o.RefProp);
        this.CacheReflectionStructRefProp = typeof(StructItem).GetProperty(nameof(StructItem.RefProp))!;
        this.CacheReflectionStructValProp = typeof(StructItem).GetProperty(nameof(StructItem.ValProp))!;
        this.CacheCreatePropertyGetterStructRefProp = MemberAccessor.CreatePropertyGetter<StructItem>(nameof(StructItem.RefProp));
        this.CacheCreatePropertyGetterStructValProp = MemberAccessor.CreatePropertyGetter<StructItem>(nameof(StructItem.ValProp));
        this.CacheGeneratePropertyGetterStructRefProp = MemberAccessor.GeneratePropertyGetter<StructItem>(nameof(StructItem.RefProp));
        this.CacheGeneratePropertyGetterStructValProp = MemberAccessor.GeneratePropertyGetter<StructItem>(nameof(StructItem.ValProp));
        this.CacheCompilePropertyGetterStructRefProp = MemberAccessor.CompilePropertyGetter<StructItem>(nameof(StructItem.RefProp));
        this.CacheCompilePropertyGetterStructValProp = MemberAccessor.CompilePropertyGetter<StructItem>(nameof(StructItem.ValProp));
    }

    public ClassItem ClassTarget { get; private set; } = default!;
    public StructItem StructTarget { get; private set; } = default;

    public Func<ClassItem, object?> CacheDirectDelegateClassRefProp { get; private set; } = default!;
    public Func<ClassItem, object?> CacheDirectDelegateClassValProp { get; private set; } = default!;
    public PropertyInfo CacheReflectionClassRefProp { get; private set; } = default!;
    public PropertyInfo CacheReflectionClassValProp { get; private set; } = default!;
    public Func<ClassItem?, object?> CacheCreatePropertyGetterClassRefProp { get; private set; } = default!;
    public Func<ClassItem?, object?> CacheCreatePropertyGetterClassValProp { get; private set; } = default!;
    public Func<ClassItem?, object?> CacheGeneratePropertyGetterClassRefProp { get; private set; } = default!;
    public Func<ClassItem?, object?> CacheGeneratePropertyGetterClassValProp { get; private set; } = default!;
    public Func<ClassItem?, object?> CacheCompilePropertyGetterClassRefProp { get; private set; } = default!;
    public Func<ClassItem?, object?> CacheCompilePropertyGetterClassValProp { get; private set; } = default!;

    public Func<StructItem, object?> CacheDirectDelegateStructRefProp { get; private set; } = default!;
    public Func<StructItem, object?> CacheDirectDelegateStructValProp { get; private set; } = default!;
    public PropertyInfo CacheReflectionStructRefProp { get; private set; } = default!;
    public PropertyInfo CacheReflectionStructValProp { get; private set; } = default!;
    public Func<StructItem, object?> CacheCreatePropertyGetterStructRefProp { get; private set; } = default!;
    public Func<StructItem, object?> CacheCreatePropertyGetterStructValProp { get; private set; } = default!;
    public Func<StructItem, object?> CacheGeneratePropertyGetterStructRefProp { get; private set; } = default!;
    public Func<StructItem, object?> CacheGeneratePropertyGetterStructValProp { get; private set; } = default!;
    public Func<StructItem, object?> CacheCompilePropertyGetterStructRefProp { get; private set; } = default!;
    public Func<StructItem, object?> CacheCompilePropertyGetterStructValProp { get; private set; } = default!;

    [Benchmark]
    public void DirectDelegateClassRefProp() => this.CacheDirectDelegateClassRefProp(this.ClassTarget);
    [Benchmark]
    public void DirectDelegateClassValProp() => this.CacheDirectDelegateClassValProp(this.ClassTarget);
    [Benchmark]
    public void DirectDelegateStructRefProp() => this.CacheDirectDelegateStructRefProp(this.StructTarget);
    [Benchmark]
    public void DirectDelegateStructValProp() => this.CacheDirectDelegateStructValProp(this.StructTarget);

    [Benchmark]
    public void ReflecttionClassPrepare() => typeof(ClassItem).GetProperty(nameof(ClassItem.RefProp))!.GetValue(this.ClassTarget);
    [Benchmark]
    public void ReflecttionClassRefProp() => this.CacheReflectionClassRefProp.GetValue(this.ClassTarget);
    [Benchmark]
    public void ReflecttionClassValProp() => this.CacheReflectionClassValProp.GetValue(this.ClassTarget);

    [Benchmark]
    public void ReflecttionStructPrepare() => typeof(StructItem).GetProperty(nameof(StructItem.RefProp))!.GetValue(this.StructTarget);
    [Benchmark]
    public void ReflecttionStructRefProp() => this.CacheReflectionStructRefProp.GetValue(this.StructTarget);
    [Benchmark]
    public void ReflecttionStructValProp() => this.CacheReflectionStructValProp.GetValue(this.StructTarget);

    [Benchmark]
    public void CreatePropertyGetterClassPrepare() => MemberAccessor.CreatePropertyGetter<ClassItem>(nameof(ClassItem.RefProp))(this.ClassTarget);
    [Benchmark]
    public void CreatePropertyGetterClassRefProp() => this.CacheCreatePropertyGetterClassRefProp(this.ClassTarget);
    [Benchmark]
    public void CreatePropertyGetterClassValProp() => this.CacheCreatePropertyGetterClassValProp(this.ClassTarget);

    [Benchmark]
    public void CreatePropertyGetterStructPrepare() => MemberAccessor.CreatePropertyGetter<StructItem>(nameof(StructItem.RefProp))(this.StructTarget);
    [Benchmark]
    public void CreatePropertyGetterStructRefProp() => this.CacheCreatePropertyGetterStructRefProp(this.StructTarget);
    [Benchmark]
    public void CreatePropertyGetterStructValProp() => this.CacheCreatePropertyGetterStructValProp(this.StructTarget);

    [Benchmark]
    public void GeneratePropertyGetterClassPrepare() => MemberAccessor.GeneratePropertyGetter<ClassItem>(nameof(ClassItem.RefProp))(this.ClassTarget);
    [Benchmark]
    public void GeneratePropertyGetterClassRefProp() => this.CacheGeneratePropertyGetterClassRefProp(this.ClassTarget);
    [Benchmark]
    public void GeneratePropertyGetterClassValProp() => this.CacheGeneratePropertyGetterClassValProp(this.ClassTarget);

    [Benchmark]
    public void GeneratePropertyGetterStructPrepare() => MemberAccessor.GeneratePropertyGetter<StructItem>(nameof(StructItem.RefProp))(this.StructTarget);
    [Benchmark]
    public void GeneratePropertyGetterStructRefProp() => this.CacheGeneratePropertyGetterStructRefProp(this.StructTarget);
    [Benchmark]
    public void GeneratePropertyGetterStructValProp() => this.CacheGeneratePropertyGetterStructValProp(this.StructTarget);

    [Benchmark]
    public void CompilePropertyGetterClassPrepare() => MemberAccessor.CompilePropertyGetter<ClassItem>(nameof(ClassItem.RefProp))(this.ClassTarget);
    [Benchmark]
    public void CompilePropertyGetterClassRefProp() => this.CacheCompilePropertyGetterClassRefProp(this.ClassTarget);
    [Benchmark]
    public void CompilePropertyGetterClassValProp() => this.CacheCompilePropertyGetterClassValProp(this.ClassTarget);

    [Benchmark]
    public void CompilePropertyGetterStructPrepare() => MemberAccessor.CompilePropertyGetter<StructItem>(nameof(StructItem.RefProp))(this.StructTarget);
    [Benchmark]
    public void CompilePropertyGetterStructRefProp() => this.CacheCompilePropertyGetterStructRefProp(this.StructTarget);
    [Benchmark]
    public void CompilePropertyGetterStructValProp() => this.CacheCompilePropertyGetterStructValProp(this.StructTarget);
}
