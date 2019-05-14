using System.Linq;

namespace Acquaint.Util
{
	public static class StringExtensions
	{
		public static string SanitizePhoneNumber(this string value)
		{
			return new string(value.ToCharArray().Where(char.IsDigit).ToArray());
		}

		public static bool IsNullOrWhiteSpace(this string value) 
		{
			return string.IsNullOrWhiteSpace(value);
		}
	}
}

