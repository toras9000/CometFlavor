using System;
using BenchCometFlavor.Unicode.Codes;
using BenchmarkDotNet.Running;

namespace BenchCometFlavor.Unicode;

internal class Program
{
    static void Main(string[] args)
    {
        var test = 01;
        if (test != 0)
        {
            Console.WriteLine("Test ...");
            for (var i = 0; i < 0x1FFFFF; i++)
            {
                var w1 = EawLinearV14.GetEastAsianWidth(i);
                var w2 = EawSwitchExpV14.GetEastAsianWidth(i);
                var w3 = EawIfBinV14.GetEastAsianWidth(i);
                if (w1 != w2) throw new Exception("Illegal implement");
                if (w1 != w3) throw new Exception("Illegal implement");
            }

        }

        BenchmarkRunner.Run<BenchEaw>();
    }
}
