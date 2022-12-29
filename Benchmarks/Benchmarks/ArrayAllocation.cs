using BenchmarkDotNet.Attributes;

namespace Benchmarks;

[MemoryDiagnoser]
public class ArrayAllocation
{
    [Benchmark]
    public void WithNew()
    {
        var points = new PointStruct[5];
        for (int i = 0; i < 5; i++)
        {
            points[i].X = 5;
            points[i].Y = 10;
        }
    }
    
    [Benchmark]
    public void WithStackAllocSpan() 
    {
        Span<PointStruct> points = stackalloc PointStruct[5];
        for (int i = 0; i < 5; i++)
        {
            points[i].X = 5;
            points[i].Y = 10;
        }
    }
}

