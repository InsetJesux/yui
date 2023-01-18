using System;
using System.Text.RegularExpressions;

namespace Yui.Utils
{
	/// <summary>
	/// Extensiones para <see cref="string"/>
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Checkea si el string en cuestion cumple el regex otorgado
		/// </summary>
		/// <param name="str">String a revisar</param>
		/// <param name="pattern">Patron regex</param>
		/// <returns><see langword="true"/> si <paramref name="str"/> coincide con <paramref name="pattern"/>, <see langword="false"/> de lo contrario</returns>
		public static bool RegexMatch(this string str, string pattern)
		{
			return Regex.IsMatch(str, pattern);
		}
	}
}
