using BenchmarkDotNet.Attributes;

namespace Benchmarks;

// "Structs have better data locality. Value types add much less pressure for the GC than reference types. But big value types are expensive to copy and you can accidentally box them which is bad."
// In most cases, you will want to use classes. Use structs when all of the following is true:

// - The struct size is less than or equals to 16 bytes (e.g 4 integers).
// - The struct is short lived
// - The struct is immutable.
// - The struct will not have to be boxed frequently.

// https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/choosing-between-class-and-struct

// In addition, structs are passing by value. So when you’re passing a struct as a method parameter, it will be copied entirely.
// Copying is expensive and can hurt performance instead of improving it.

public class StructsVsClasses
{
    [Params(10, 1000)]
    public int Size { get; set; }
    
    [Benchmark]
    public void WithClass()
    {
        var points = new PointClass[Size];
        for (int i = 0; i < Size; i++)
        {
            points[i] = new PointClass
            {
                X = 5,
                Y = 10
            };
        }
    }
    
    [Benchmark]
    public void WithStruct()
    {
        var vectors = new PointStruct[Size];
        for (int i = 0; i < Size; i++)
        {
            vectors[i].X = 5;
            vectors[i].Y = 10;
        }
    }
}