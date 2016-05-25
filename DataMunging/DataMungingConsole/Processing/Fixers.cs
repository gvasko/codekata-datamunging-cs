using System;
using System.Text;

namespace DataMungingConsole.Processing
{
    public sealed class Fixers
    {
        private Fixers()
        {
            throw new InvalidOperationException("No instances");
        }

        public static string KeepDigitsOnly(int column, string original)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in original)
            {
                if (Char.IsDigit(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
