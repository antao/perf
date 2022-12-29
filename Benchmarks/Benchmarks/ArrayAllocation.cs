using BenchmarkDotNet.Attributes;

namespace Benchmarks;

internal struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

[MemoryDiagnoser]
public class ArrayAllocation
{
    [Benchmark]
    public void WithNew()
    {
        var points = new Point[5];
        for (int i = 0; i < 5; i++)
        {
            points[i].X = 5;
            points[i].Y = 10;
        }
    }
    [Benchmark]
    public void WithStackAllocSpan() 
    {
        Span<Point> points = stackalloc Point[5];
        for (int i = 0; i < 5; i++)
        {
            points[i].X = 5;
            points[i].Y = 10;
        }
    }
}

