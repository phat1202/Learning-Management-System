using System.Security.Cryptography;
using System.Text;

namespace Learning_Management_System.Extensions
{
    public static class Extensions
    {
        public static string Hash(this string value)
        {
            return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));
        }
    }
}
