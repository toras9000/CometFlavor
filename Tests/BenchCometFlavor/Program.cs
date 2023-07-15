using BenchCometFlavor.Reflection;
using BenchmarkDotNet.Running;

namespace BenchCometFlavor.Unicode;

internal class Program
{
    static void Main(string[] args)
    {
        var mode = 1;
        switch (mode)
        {
        case 1: BenchmarkRunner.Run<BenchCreatePropertyGetter>(); break;
        default: break;
        }
    }
}
