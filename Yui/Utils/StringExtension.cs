using System;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace Yui.Utils
{
	public class StringExtension
	{
		/// <summary>
		/// Checkea si el string en cuestion cumple el regex otorgado
		/// </summary>
		/// <param name="str">String a revisar</param>
		/// <param name="pattern">Patron regex</param>
		/// <returns><see langword="true"/> si <paramref name="str"/> coincide con <paramref name="pattern"/>, <see langword="false"/> de lo contrario</returns>
		public static bool RegexMatch(string str, string pattern)
		{
			return Regex.IsMatch(str, pattern);
		}
	}
}
