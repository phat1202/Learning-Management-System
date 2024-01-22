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
        public static string GetRelativeTime(this DateTime? timestamp)
        {
            if (timestamp.HasValue)
            {
                DateTime now = DateTime.Now;
                TimeSpan timeDifference = now - timestamp.Value;

                if (timeDifference.TotalDays >= 1)
                {
                    int days = (int)timeDifference.TotalDays;
                    return days == 1 ? "1 day" : $"{days} days";
                }
                else if (timeDifference.TotalHours >= 1)
                {
                    int hours = (int)timeDifference.TotalHours;
                    return hours == 1 ? "1 hour" : $"{hours} hours";
                }
                else if (timeDifference.TotalMinutes >= 1)
                {
                    int minutes = (int)timeDifference.TotalMinutes;
                    return minutes == 1 ? "1 minute" : $"{minutes} minutes";
                }
                else
                {
                    return "Just now";
                }
            }
            else
            {
                return "Error";
            }
        }
    }
}
