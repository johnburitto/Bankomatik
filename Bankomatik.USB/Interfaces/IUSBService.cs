using Bankomatik.USB.Dtos;

namespace Bankomatik.USB.Interfaces
{
	/// <summary>
	/// Describe all methods for operations with USB.
	/// </summary>
	public interface IUSBService
	{
		/// <summary>
		/// Detect all USB drives.
		/// </summary>
		/// <returns>List of USB drives infos.</returns>
		List<USBDriveInfo> GetUSBDrives();

		/// <summary>
		/// Create an bank card on USB drive.
		/// </summary>
		/// <param name="drive">USB drive info.</param>
		void CreateUSBBankCard(USBDriveInfo drive, CardInfo card);

		/// <summary>
		/// Delete an bank card from USB drive.
		/// </summary>
		/// <param name="drive">USB drive info.</param>
		void DeleteUSBBankCard(USBDriveInfo drive);

		/// <summary>
		/// Withdraw money from bank card.
		/// </summary>
		/// <param name="drive">USB drive info.</param>
		/// <param name="amount">Withdraw amount.</param>
		void WithdrawMoney(USBDriveInfo drive, float amount);
	}
}
