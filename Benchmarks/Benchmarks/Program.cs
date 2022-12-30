using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using Benchmarks;

Console.WriteLine("Performance is a feature");

var gcMode = new GcMode { Force = false };
var config = ManualConfig
    .Create(DefaultConfig.Instance)
    .AddDiagnoser(new MemoryDiagnoser(new MemoryDiagnoserConfig()))
    .AddValidator(JitOptimizationsValidator.FailOnError)
    .AddJob(Job.Default.WithGcMode(gcMode));

BenchmarkRunner.Run<SubstringVsSlice>(config);