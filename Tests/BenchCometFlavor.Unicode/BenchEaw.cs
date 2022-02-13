#nullable disable
using System.Linq;
using BenchCometFlavor.Unicode.Codes;
using BenchmarkDotNet.Attributes;

namespace BenchCometFlavor.Unicode
{
    public class BenchEaw
    {
        [Params(10000)]
        public int Count;

        [GlobalSetup]
        public void Setup()
        {
            var interval = 0x300000 / this.Count;
            this.Codes = Enumerable.Range(0, this.Count).Select(n => n * interval).ToArray();
        }

        public int[] Codes { get; private set; }

        [Benchmark]
        public void EawLinear()
        {
            for (var i = 0; i < this.Codes.Length; i++)
            {
                EawLinearV14.GetEastAsianWidth(this.Codes[i]);
            }
        }

        [Benchmark]
        public void EawSwitchExp()
        {
            for (var i = 0; i < this.Codes.Length; i++)
            {
                EawSwitchExpV14.GetEastAsianWidth(this.Codes[i]);
            }
        }

        [Benchmark]
        public void EawIfBin()
        {
            for (var i = 0; i < this.Codes.Length; i++)
            {
                EawIfBinV14.GetEastAsianWidth(this.Codes[i]);
            }
        }
    }
}
