using System;
using BenchCometFlavor.Reflection;
using BenchmarkDotNet.Running;

namespace BenchCometFlavor.Unicode;

internal class Program
{
    static void Main(string[] args)
    {
        var mode = 2;
        switch (mode)
        {
        case 1: BenchmarkRunner.Run<BenchPropertyGetter>(); break;
        case 2: BenchmarkRunner.Run<BenchFieldGetter>(); break;
        default: break;
        }
    }
}
