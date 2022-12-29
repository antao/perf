using BenchmarkDotNet.Attributes;

namespace Benchmarks;

public class SubstringVsSlice
{
    private string _text = null!;

    [Params(10, 1000)]
    public int CharactersCount { get; set; }

    [GlobalSetup]
    public void Setup() => _text = new string(Enumerable.Repeat('a', CharactersCount).ToArray());

    [Benchmark]
    public string Substring() => _text.Substring(0, _text.Length / 2);

    [Benchmark(Baseline = true)]
    public ReadOnlySpan<char> Slice() => _text.AsSpan().Slice(0, _text.Length / 2); // No allocation
}