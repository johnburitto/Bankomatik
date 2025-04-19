using System.ComponentModel;

namespace Bankomatik.Common.Enums
{
	/// <summary>
	/// Represents the status of a bank card.
	/// </summary>
	public enum DriveStatus
	{
		[Description("Card")]
		Card,

		[Description("Not card")]
		NotCard,

		[Description("Card/Corrupted")]
		Corrupted
	}
}
