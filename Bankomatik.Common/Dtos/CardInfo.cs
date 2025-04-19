namespace Bankomatik.USB.Dtos
{
	/// <summary>
	/// Represents a bank card information.
	/// </summary>
	public class CardInfo
	{
		/// <summary>
		/// Provides information about account.
		/// </summary>
		public string? Account { get; set; }

		/// <summary>
		/// Provides information about balance.
		/// </summary>
		public float Balance { get; set; }

		/// <summary>
		/// Provides information about CVV.
		/// </summary>
		public int CVV { get; set; }

		/// <summary>
		/// Provides information about PIN.
		/// </summary>
		public int Pin { get; set; }
	}
}
