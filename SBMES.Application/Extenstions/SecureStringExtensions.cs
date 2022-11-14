using System.Runtime.InteropServices;
using System.Security;

namespace SBMES.Application.Extenstions
{
    public static class SecureStringExtensions
    {
        public static string ToUnsecureString(this SecureString source)
        {
            var returnValue = IntPtr.Zero;
            try 
            {
                returnValue = Marshal.SecureStringToGlobalAllocUnicode(source);
                return Marshal.PtrToStringUni(returnValue);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(returnValue);
            }
        }

        public static SecureString ToSecureString(this string plainString)
        {
            if (plainString == null)
            {
                return null;
            }

            SecureString secureString = new();
            foreach (char c in plainString)
            {
                secureString.AppendChar(c);
            }
            return secureString;
        }
    }
}
