using System.Text;

namespace SQLHydra
{
    internal static class Extensions
    {
        internal static string AsScrubbedString(this StringBuilder value)
        {
            string output = value.ToString().Trim();

            while(output.Contains("  "))
            {
                output = output.Replace("  ", " ");
            }

            return output;
        }

        internal static string ForcePrefix(this string value, string prefix)
        {
            return value.StartsWith(prefix) ? value : string.Format("{0}{1}", prefix, value);
        }

        internal static string ForceSuffix(this string value, string suffix)
        {
            return value.EndsWith(suffix) ? value : string.Format("{0}{1}", value, suffix);
        }
    }
}
