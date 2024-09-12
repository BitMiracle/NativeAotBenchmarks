using BenchmarkDotNet.Attributes;
using BitMiracle.Docotic.Pdf;

namespace NativeAotBenchmarks
{
    public abstract class PdfToImageBenchmarkBase
    {
        private readonly string m_fileName;
        private readonly PdfDrawOptions m_options;

        private MemoryStream m_input = null!;
        private MemoryStream m_output = null!;

        protected PdfToImageBenchmarkBase(string fileName, ImageCompressionOptions compression, int resolution = 72)
        {
            m_fileName = fileName;

            m_options = PdfDrawOptions.Create();
            m_options.Compression = compression;
            m_options.HorizontalResolution = resolution;
            m_options.VerticalResolution = resolution;
        }

        [GlobalSetup]
        public void Setup()
        {
            Helper.ApplyDocoticLicense();

            m_input = new MemoryStream(File.ReadAllBytes(m_fileName));
            m_output = new MemoryStream(2 * 1024 * 1024);
        }

        [IterationSetup]
        public void IterationSetup()
        {
            m_input.Position = 0;
            m_output.Position = 0;
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            m_input.Dispose();
            m_output.Dispose();
        }

        [Benchmark]
        public void Convert()
        {
            using var streamProvider = new PdfMemoryStreamProvider();
            PdfConfigurationOptions configuration = PdfConfigurationOptions.CreateWithStreamProvider(streamProvider);
            using var pdf = new PdfDocument(m_input, configuration);

            pdf.Pages[0].Save(m_output, m_options);
        }
    }
}
