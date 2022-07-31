# IndieRegex

IndieRegex or IndieSystem.Text.RegularExpressions is a drop-in replacement for .NET's Regex class. It is built using the source of Regex from .NET 7.0, modified and patched to run on .NET Framework 4.6.1, .NET Standard 2.0, .NET Core 3.1, .NET 5.0 and .NET 6.0.

In .NET 7.0 the team has invested heavily in Regex both in terms of performance and functionality. [This blog post contains](https://devblogs.microsoft.com/dotnet/regular-expression-improvements-in-dotnet-7/) a good description of the changes. The best way to benefit from these changes is to upgrade to .NET 7.0. If upgrading is possible, then package allows developers to gain some of the benefits without upgrading.

Particularly interesting are .NET 7.0's non-backtracking regex. One issue with backtracking regexes is their worst case performance can be very bad, there are many explanations of "catastrophic backtracking" on the internet, so we won't go into it further here. Non-backtracking regexes use a different algorithm to execute, while this algorithm is not necessarily faster, it does guarantee a linear execution time with respect to the search string size. A guaranteed execution time is particularly useful if the regex is to be run on untrusted input; it means an attacker cannot create a DOS by crafting an input that will produce catastrophic backtracking.

While the non-backtracking algorithm isn't necessarily faster than the backtracking algorithms, tests that I have run seem to find cases where non-backtracking regexes are significantly faster than backtracking regexes. An example of a benchmark from the CI is shown below.

``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.20348
Intel Xeon Platinum 8272CL CPU 2.60GHz, 1 CPU, 2 logical and 2 physical cores
.NET SDK=7.0.100-preview.6.22352.1
  [Host]     : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT
  Job-UAIUPX : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT
  Job-ARZZTG : .NET 6.0.7 (6.0.722.32202), X64 RyuJIT
  Job-WOGHCQ : .NET Core 3.1.27 (CoreCLR 4.700.22.30802, CoreFX 4.700.22.31504), X64 RyuJIT
  Job-GFKAAW : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT


```
|                 Method |        Job |              Runtime |     Toolchain |        Mean |     Error |    StdDev |      Median | Ratio | RatioSD |
|----------------------- |----------- |--------------------- |-------------- |------------:|----------:|----------:|------------:|------:|--------:|
|           Backtracking | Job-UAIUPX |             .NET 5.0 |        net5.0 |   786.86 μs | 16.672 μs | 49.159 μs |   823.20 μs |  0.68 |    0.04 |
|           Backtracking | Job-ARZZTG |             .NET 6.0 |        net6.0 |   408.08 μs |  8.145 μs | 18.048 μs |   404.15 μs |  0.35 |    0.02 |
|           Backtracking | Job-WOGHCQ |        .NET Core 3.1 | netcoreapp3.1 |   763.45 μs | 17.779 μs | 52.143 μs |   752.30 μs |  0.70 |    0.05 |
|           Backtracking | Job-GFKAAW | .NET Framework 4.6.1 |        net461 | 1,168.46 μs |  3.321 μs |  2.773 μs | 1,169.30 μs |  1.00 |    0.00 |
|                        |            |                      |               |             |           |           |             |       |         |
|    BacktrackingTimeout | Job-UAIUPX |             .NET 5.0 |        net5.0 |   727.83 μs | 13.591 μs | 24.508 μs |   715.15 μs |  0.55 |    0.02 |
|    BacktrackingTimeout | Job-ARZZTG |             .NET 6.0 |        net6.0 |   403.04 μs |  8.036 μs | 19.098 μs |   397.36 μs |  0.30 |    0.02 |
|    BacktrackingTimeout | Job-WOGHCQ |        .NET Core 3.1 | netcoreapp3.1 |   790.63 μs | 18.838 μs | 55.247 μs |   773.70 μs |  0.61 |    0.04 |
|    BacktrackingTimeout | Job-GFKAAW | .NET Framework 4.6.1 |        net461 | 1,334.48 μs |  2.514 μs |  2.229 μs | 1,334.40 μs |  1.00 |    0.00 |
|                        |            |                      |               |             |           |           |             |       |         |
|        NonBacktracking | Job-UAIUPX |             .NET 5.0 |        net5.0 |    10.88 μs |  0.134 μs |  0.131 μs |    10.81 μs |  0.20 |    0.01 |
|        NonBacktracking | Job-ARZZTG |             .NET 6.0 |        net6.0 |    10.27 μs |  0.203 μs |  0.506 μs |    10.29 μs |  0.19 |    0.01 |
|        NonBacktracking | Job-WOGHCQ |        .NET Core 3.1 | netcoreapp3.1 |    63.37 μs |  7.484 μs | 22.066 μs |    51.60 μs |  1.36 |    0.40 |
|        NonBacktracking | Job-GFKAAW | .NET Framework 4.6.1 |        net461 |    54.64 μs |  1.082 μs |  2.259 μs |    54.40 μs |  1.00 |    0.00 |
|                        |            |                      |               |             |           |           |             |       |         |
| NonBacktrackingTimeout | Job-UAIUPX |             .NET 5.0 |        net5.0 |    29.41 μs |  2.329 μs |  6.829 μs |    27.00 μs |  0.61 |    0.08 |
| NonBacktrackingTimeout | Job-ARZZTG |             .NET 6.0 |        net6.0 |    10.20 μs |  0.204 μs |  0.504 μs |    10.08 μs |  0.18 |    0.01 |
| NonBacktrackingTimeout | Job-WOGHCQ |        .NET Core 3.1 | netcoreapp3.1 |    63.14 μs |  8.233 μs | 24.275 μs |    51.20 μs |  0.98 |    0.30 |
| NonBacktrackingTimeout | Job-GFKAAW | .NET Framework 4.6.1 |        net461 |    58.05 μs |  1.150 μs |  1.824 μs |    57.80 μs |  1.00 |    0.00 |
|                        |            |                      |               |             |           |           |             |       |         |
|                 Native | Job-UAIUPX |             .NET 5.0 |        net5.0 |   357.80 μs | 10.238 μs | 29.045 μs |   349.10 μs |  0.24 |    0.02 |
|                 Native | Job-ARZZTG |             .NET 6.0 |        net6.0 |   305.96 μs |  6.109 μs | 15.879 μs |   302.34 μs |  0.18 |    0.01 |
|                 Native | Job-WOGHCQ |        .NET Core 3.1 | netcoreapp3.1 |   940.56 μs | 22.238 μs | 65.221 μs |   962.70 μs |  0.55 |    0.04 |
|                 Native | Job-GFKAAW | .NET Framework 4.6.1 |        net461 | 1,676.14 μs | 19.525 μs | 18.263 μs | 1,667.90 μs |  1.00 |    0.00 |
|                        |            |                      |               |             |           |           |             |       |         |
|          NativeTimeout | Job-UAIUPX |             .NET 5.0 |        net5.0 |   673.90 μs | 19.542 μs | 57.313 μs |   647.10 μs |  0.45 |    0.03 |
|          NativeTimeout | Job-ARZZTG |             .NET 6.0 |        net6.0 |   794.21 μs |  3.527 μs |  3.127 μs |   794.30 μs |  0.48 |    0.00 |
|          NativeTimeout | Job-WOGHCQ |        .NET Core 3.1 | netcoreapp3.1 | 1,128.57 μs |  4.832 μs |  4.035 μs | 1,126.80 μs |  0.68 |    0.00 |
|          NativeTimeout | Job-GFKAAW | .NET Framework 4.6.1 |        net461 | 1,663.74 μs |  2.408 μs |  2.135 μs | 1,663.85 μs |  1.00 |    0.00 |


# Disclaimer

This project reuses source from the [dotnet/runtime](https://github.com/dotnet/runtime) repository. This kind of reuse is permitted under the permissive MIT license used for both the dotnet/runtime repository and this repository.

This project is not support or endorsed by The .NET Foundation nor Microsoft.

# License

This repository is licensed under the [MIT](LICENCE.TXT) license.