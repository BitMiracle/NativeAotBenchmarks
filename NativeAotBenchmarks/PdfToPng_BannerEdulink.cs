using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BitMiracle.Docotic.Pdf;

namespace NativeAotBenchmarks
{
    [SimpleJob(RuntimeMoniker.NativeAot80)]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class PdfToPng_BannerEdulink : PdfToImageBenchmarkBase
    {
        public PdfToPng_BannerEdulink()
            : base("Banner Edulink One.pdf", ImageCompressionOptions.CreatePng())
        {
        }
    }
}
