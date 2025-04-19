namespace Bankomatik.Common.Extensions
{
	/// <summary>
	/// String extentions.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Convert string to float.
		/// </summary>
		/// <param name="value">Value to convert.</param>
		/// <returns>Float.</returns>
		public static float ToFloat(this string value)
			=> float.Parse(value);
	}
}
