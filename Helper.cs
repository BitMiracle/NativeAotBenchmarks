using System.Text;
using BitMiracle.Docotic;

namespace NativeAotBenchmarks
{
    static class Helper
    {
        public static void ApplyDocoticLicense()
        {
            // NOTE:
            // Without a license, the library won't allow you to create or read PDF documents.
            // To get a free time-limited license key, use the form on
            // https://bitmiracle.com/pdf-library/download

            LicenseManager.AddLicenseData("PUT-LICENSE-HERE");
        }

        public static string CompressString(string s)
        {
            StringBuilder compressed = new(s.Length);

            for (int i = 0; i < s.Length; ++i)
            {
                char c = s[i];
                for (int j = i + 1; j <= s.Length; ++j)
                {
                    if (j == s.Length || s[j] != c)
                    {
                        compressed.Append(c + $"{j - i}");
                        i = j - 1;

                        if (compressed.Length > s.Length)
                            return s;

                        break;
                    }
                }
            }

            if (compressed.Length <= s.Length)
                return compressed.ToString();

            return s;
        }
    }
}
