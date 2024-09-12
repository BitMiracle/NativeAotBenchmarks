using BitMiracle.Docotic.Pdf;
using NativeAotBenchmarks;

namespace NativeAotTestApp
{
    internal class Program
    {
        static int Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                    throw new ArgumentException("Missing arguments. " + getSupportedOptionsInfo());

                string commandType = args[0];
                int iterationCount = int.Parse(args[1]);

                switch (commandType)
                {
                    case Commands.PdfToPng:
                        Helper.ApplyDocoticLicense();

                        for (int i = 0; i < iterationCount; ++i)
                            DrawPage(args);

                        break;

                    case Commands.CompressString:
                        for (int i = 0; i < iterationCount; ++i)
                            CompressString(args);

                        break;

                    default:
                        throw new ArgumentException($"Invalid command type {commandType}. {getSupportedOptionsInfo()}");
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return -1;
            }
        }

        static void DrawPage(string[] args)
        {
            if (args.Length < 5)
                throw new ArgumentException($"Wrong syntax. {getSupportedOptionsInfo()}");

            if (!int.TryParse(args[4], out int pageIndex))
                throw new ArgumentException($"Wrong page index. {getSupportedOptionsInfo()}");

            using var pdf = new PdfDocument(args[3]);

            PdfDrawOptions options = PdfDrawOptions.Create();
            options.BackgroundColor = new PdfRgbColor(255, 255, 255);

            pdf.Pages[pageIndex].Save(args[2], options);
        }

        static string CompressString(string[] args)
        {
            if (args.Length < 3)
                throw new ArgumentException($"Wrong syntax. {getSupportedOptionsInfo()}");

            return Helper.CompressString(args[2]);
        }

        static string getSupportedOptionsInfo()
            => $"Supported options:\r\n" +
            $"{Commands.PdfToPng} {{IterationCount}} {{OutputPath}} {{InputPath}} {{InputPageIndex}}\r\n" +
            $"{Commands.CompressString} {{IterationCount}} {{String}}";

        static class Commands
        {
            public const string PdfToPng = "pdftopng";
            public const string CompressString = "stringcompress";
        }
    }
}
