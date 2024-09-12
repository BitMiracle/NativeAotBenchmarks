using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BitMiracle.Docotic.Pdf;

namespace NativeAotBenchmarks
{
    [SimpleJob(RuntimeMoniker.NativeAot80)]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class PdfToPng_3BigPreview : PdfToImageBenchmarkBase
    {
        public PdfToPng_3BigPreview()
            : base("3BigPreview.pdf", ImageCompressionOptions.CreatePng(), 300)
        {
        }
    }
}
