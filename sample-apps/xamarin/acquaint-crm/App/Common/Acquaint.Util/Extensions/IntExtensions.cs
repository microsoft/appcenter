using System;

namespace Acquaint.Util
{
	public static class IntExtensions
	{
		public static int RoundToLowestHundreds (this int i)
		{
			return (int)Math.Floor(i / 100.0) * 100;
		}
	}
}

