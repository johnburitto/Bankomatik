namespace Bankomatik.Common.Dtos
{
	/// <summary>
	/// Holds data needed for server requests.
	/// </summary>
	public class ServerRequestDto
	{
		/// <summary>
		/// Provides information about the card number.
		/// </summary>
		public string? CardNumber { get; set; }

		/// <summary>
		/// Provides information about the CVV.
		/// </summary>
		public string? CVV { get; set; }

		/// <summary>
		/// Provides information about the PIN.
		/// </summary>
		public string? PVV { get; set; }

		/// <summary>
		/// Provides information about the amount of withdraw.
		/// </summary>
		public float Amount { get; set; }
	}
}
