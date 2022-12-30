using BenchmarkDotNet.Attributes;

namespace Benchmarks;

// Span<T> is a struct (value type) and it will NOT cause heap allocation. Span<T> uses contiguous memory in a more convenient fashion ensuring memory and type safety.

// Limitation of Span<T>
// - Span<T> cannot be a member of a class. (because when you make it a field in a class it will be stored on the heap. This is prohibited!)
// - Span<T> cannot be used in an async method. The result is whenever async & await are used, an AsyncMethodBuilder is created. The builder creates an asynchronous state machine, and in some situations might put parameters of the method on the heap so that Span<T> violates this rule.
// - Span<T> cannot implement any interface

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