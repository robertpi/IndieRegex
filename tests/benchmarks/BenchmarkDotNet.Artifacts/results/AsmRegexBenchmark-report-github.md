``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.22621
Intel Core i9-10885H CPU 2.40GHz, 1 CPU, 16 logical and 8 physical cores
.NET Core SDK=7.0.100-preview.5.22307.18
  [Host]     : .NET Core 6.0.3 (CoreCLR 6.0.322.12309, CoreFX 6.0.322.12309), X64 RyuJIT
  DefaultJob : .NET Core 6.0.3 (CoreCLR 6.0.322.12309, CoreFX 6.0.322.12309), X64 RyuJIT


```
|          Method |       Mean |     Error |    StdDev |
|---------------- |-----------:|----------:|----------:|
|    Backtracking | 345.847 μs | 1.8883 μs | 1.7663 μs |
| NonBacktracking |   7.565 μs | 0.0423 μs | 0.0396 μs |
