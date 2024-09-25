# Benchmarking Native AOT code

This project compares the performance of the managed .NET code with the corresponding Native AOT version.
We use 2 types of benchmarks:
* Based on [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet)
* Based on [hyperfine](https://github.com/sharkdp/hyperfine)

Read the [.NET Native AOT benchmarking](https://bitmiracle.com/pdf-library/howto/native-aot-performance) article for more detail.

## Prerequisites

Install [prerequisites](https://docs.microsoft.com/en-us/dotnet/core/deploying/native-aot/#prerequisites) required by the Native AOT compiler.

[Install hyperfine](https://github.com/sharkdp/hyperfine#installation) to run corresponding benchmarks.

This project uses the [Docotic.Pdf](https://bitmiracle.com/pdf-library/) library for PDF to PNG conversion.
You need to provide a license key in the [Helper.cs](/Helper.cs#L15).
You can get a free license key here: https://bitmiracle.com/pdf-library/download.

## BenchmarkDotNet

Run the [NativeAotBenchmarks](/NativeAotBenchmarks) project in the `Release` configuration and
choose a corresponding benchmark to run.

## hyperfine

Run the [benchmark.bat](/NativeAotTestApp/benchmark.bat) script on Windows.
