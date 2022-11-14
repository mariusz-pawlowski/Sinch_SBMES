using System.Text;

namespace SBMES.Application.Extenstions
{
    public static class StringExtentions
    {
        /// <summary>
        /// ASCII encoding replaces non-ascii with question marks, so we use UTF8 to see if multi-byte sequences are there
        /// </summary>
        /// <param name="value">string to check if is ASCII encoded</param>
        /// <returns></returns>
        public static bool IsASCII(this string value)
        {
            return Encoding.UTF8.GetByteCount(value) == value.Length;
        }

        /// <summary>
        /// A byte array containing the results of encoding the specified set of characters
        /// </summary>
        /// <param name="value">string to get encoded bytes</param>
        /// <returns></returns>
        public static byte[] GetBytes(this string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        
    }
}
