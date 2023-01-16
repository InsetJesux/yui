using System;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace InteractionFramework.Utils
{
	public class StringExtension
	{

		/// <summary>
		/// Checkea si el string en cuestion cumple el regex otorgado
		/// </summary>
		/// <param name="str">String a revisar</param>
		/// <param name="pattern">Patron regex</param>
		/// <returns></returns>
		public static bool RegexMatch(String str, String pattern)
		{
			return Regex.Match(str, pattern);
		}



	}
}
