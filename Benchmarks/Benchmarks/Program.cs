using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using Benchmarks;

Console.WriteLine("Performance is a feature");

// Span<T> is a struct (value type) and it will NOT cause heap allocation. Span<T> uses contiguous memory in a more convenient fashion ensuring memory and type safety.

// Limitation of Span<T>
// - Span<T> cannot be a member of a class. (because when you make it a field in a class it will be stored on the heap. This is prohibited!)
// - Span<T> cannot be used in an async method. The result is whenever async & await are used, an AsyncMethodBuilder is created. The builder creates an asynchronous state machine, and in some situations might put parameters of the method on the heap so that Span<T> violates this rule.
// - Span<T> cannot implement any interface

var gcMode = new GcMode { Force = false };
var config = ManualConfig
    .Create(DefaultConfig.Instance)
    .AddDiagnoser(new MemoryDiagnoser(new MemoryDiagnoserConfig()))
    .AddValidator(JitOptimizationsValidator.FailOnError)
    .AddJob(Job.Default.WithGcMode(gcMode));

BenchmarkRunner.Run<SubstringVsSlice>(config);