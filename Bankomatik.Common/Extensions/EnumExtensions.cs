using System.ComponentModel;

namespace Bankomatik.Common.Extensions
{
	/// <summary>
	/// Enum extentions.
	/// </summary>
	public static class EnumExtensions
	{
		/// <summary>
		/// Get value from Description attribte of enum.
		/// </summary>
		/// <param name="value">Enum value.</param>
		/// <returns>Description attribte value.</returns>
		public static string GetDescription(this Enum value)
		{
			var field = value.GetType().GetField(value.ToString());
			var attribute = (DescriptionAttribute?)Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute));

			return attribute == null ? value.ToString() : attribute.Description;
		}
	}
}
