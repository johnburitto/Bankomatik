using Bankomatik.Common.Enums;
using Bankomatik.Common.Constants;
using Bankomatik.Common.Extensions;

namespace Bankomatik.USB.Dtos
{
	/// <summary>
	/// Extend class for <see cref="DriveInfo"/>.
	/// </summary>
	public class USBDriveInfo
	{
		#region Public properties

		/// <summary>
		/// USB drive info.
		/// </summary>
		public DriveInfo? Info { get; set; }

		/// <summary>
		/// USB drive index.
		/// </summary>
		public int Index { get; set; }

		/// <summary>
		/// USB drive directories.
		/// </summary>
		public List<string> Directories => Directory.GetDirectories(Info!.RootDirectory.FullName).ToList();

		/// <summary>
		/// Check if USB drive is bank card.
		/// </summary>
		public bool IsBankCard => Directories.Any(directory => directory.Equals($"{Info?.Name}{BankConstants.BankCardDirectory}", StringComparison.OrdinalIgnoreCase));

		/// <summary>
		/// Check if USB drive is bank card.
		/// </summary>
		public string CardStatus => GetDriveStatus().GetDescription();

		#endregion

		#region Private methods

		/// <summary>
		/// Get drive status.
		/// </summary>
		/// <returns>Drive status.</returns>
		private DriveStatus GetDriveStatus()
		{
			if (Directories.Contains($"{Info!.Name}{BankConstants.BankCardDirectory}"))
			{
				var files = Directory.GetFiles($"{Info.Name}{BankConstants.BankCardDirectory}");

				if (files.Contains($"{Info.Name}{BankConstants.BankCardDirectory}\\{BankConstants.AccountFileName}")
					&& files.Contains($"{Info.Name}{BankConstants.BankCardDirectory}\\{BankConstants.CVVFileName}")
					&& files.Contains($"{Info.Name}{BankConstants.BankCardDirectory}\\{BankConstants.BalanceFileName}")
					&& files.Contains($"{Info.Name}{BankConstants.BankCardDirectory}\\{BankConstants.PinFileName}"))
				{
					return DriveStatus.Card;
				}
				else
				{
					return DriveStatus.Corrupted;
				}
			}
			else
			{
				return DriveStatus.NotCard;
			}
		}

		#endregion
	}
}
