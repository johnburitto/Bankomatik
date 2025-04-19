namespace Bankomatik.USB.Dtos
{
	/// <summary>
	/// Represents a bank card information.
	/// </summary>
	public class CardInfo
	{
		/// <summary>
		/// Provides information about card number.
		/// </summary>
		public string? CardNumber { get; set; }

		/// <summary>
		/// Provides information about balance.
		/// </summary>
		public float Balance { get; set; }

		/// <summary>
		/// Provides information about CVV.
		/// </summary>
		public string? CVV { get; set; }

		/// <summary>
		/// Provides information about PIN.
		/// </summary>
		public string? Pin { get; set; }
	}
}
