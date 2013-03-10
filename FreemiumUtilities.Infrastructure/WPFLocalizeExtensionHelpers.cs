using System;
using WPFLocalizeExtension.Extensions;

namespace FreemiumUtilities.Infrastructure
{
	/// <summary>
	/// Provides a methods to operate with <c>WPFLocalizeExtension</c> library
	/// </summary>
	public class WPFLocalizeExtensionHelpers
	{
		/// <summary>
		/// Gets the specified UI string, localized with a current thread culture
		/// </summary>
		/// <param name="key">Resource key</param>
		/// <returns>Localized string for the specified resource key</returns>
		public static string GetUIString(string key)
		{
			string uiString;
			var locExtension = new LocTextExtension(String.Format("FreemiumUtilities:Resources:{0}", key));
			locExtension.ResolveLocalizedValue(out uiString);
			return uiString;
		}
	}
}