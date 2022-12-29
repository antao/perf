using BenchmarkDotNet.Attributes;

namespace Benchmarks;

public class DynamicCollections
{
    [Params(10, 1000)]
    public int Size { get; set; }
    
    [Benchmark]
    public void ListDynamicCapacity()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < Size; i++)
        {
            list.Add(i);
        }
    }
    
    [Benchmark]
    public void ListPlannedCapacity()
    {
        List<int> list = new List<int>(Size);
        for (int i = 0; i < Size; i++)
        {
            list.Add(i);
        }
    }
}