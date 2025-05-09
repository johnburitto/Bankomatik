﻿using System.Text;

using Bankomatik.USB.Dtos;
using Bankomatik.USB.Interfaces;
using Bankomatik.Common.Constants;
using Bankomatik.Crypto.Interfaces;
using Bankomatik.Common.Enums;
using Bankomatik.Common.Logging;

namespace Bankomatik.USB.Implementations
{
	/// <summary>
	/// Realization of <see cref="IUSBService"/> interface.
	/// </summary>
	public class USBService : IUSBService
	{
		#region Private fields

		/// <summary>
		/// Crypto service.
		/// </summary>
		private readonly ICryptoService _cryptoService;

		#endregion

		#region Constructor

		/// <summary>
		/// Inizialise instance of <see cref="USBService"/> class.
		/// </summary>
		/// <param name="cryptoService">Crypto service.</param>
		public USBService(ICryptoService cryptoService)
		{
			_cryptoService = cryptoService;
		}

		#endregion

		#region Implementation of IUSBService

		/// <inheritdoc />
		public void CreateUSBBankCard(USBDriveInfo drive, CardInfo card)
		{
			Directory.CreateDirectory($"{drive.Info?.Name}{BankConstants.BankCardDirectory}");
			File.WriteAllBytes($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.CardNumberFileName}", 
				_cryptoService.EncryptData(Encoding.UTF8.GetBytes(card.CardNumber ?? "")));
			File.WriteAllBytes($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.CVVFileName}",
				_cryptoService.EncryptData(Encoding.UTF8.GetBytes(card.CVV ?? "")));
			File.WriteAllBytes($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.BalanceFileName}",
				_cryptoService.EncryptData(BitConverter.GetBytes(card.Balance)));
			File.WriteAllBytes($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.PinFileName}",
				_cryptoService.EncryptData(Encoding.UTF8.GetBytes(card.Pin ?? "")));

			Logger.Information($"Bank card created on USB drive '{drive.Info?.Name}'");
			Logger.Information($"Your PIN => '{card.Pin}'");
		}

		/// <inheritdoc />
		public void DeleteUSBBankCard(USBDriveInfo drive)
		{
			File.Delete($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.CardNumberFileName}");
			File.Delete($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.CVVFileName}");
			File.Delete($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.BalanceFileName}");
			File.Delete($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.PinFileName}");
			Directory.Delete($"{drive.Info?.Name}{BankConstants.BankCardDirectory}");
		}

		/// <inheritdoc />
		public void WithdrawMoney(USBDriveInfo drive, float amount)
		{
			var cardBalance = BitConverter.ToSingle(_cryptoService.DecryptData(
				File.ReadAllBytes($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.BalanceFileName}")), 0);

			cardBalance -= amount;

			File.WriteAllBytes($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.BalanceFileName}",
				_cryptoService.EncryptData(BitConverter.GetBytes(cardBalance)));
		}

		/// <inheritdoc />
		public List<USBDriveInfo> GetUSBDrives()
			=> DriveInfo.GetDrives()
				.Where(drive => drive.DriveType == DriveType.Removable && drive.IsReady)
				.Select((drive, index) => new USBDriveInfo
				{
					Info = drive,
					Index = index
				})
				.ToList();

		/// <inheritdoc />
		public string GetCardData(USBDriveInfo drive, CardData type)
			=> type switch
			{
				CardData.CardNumber => Encoding.UTF8.GetString(_cryptoService.DecryptData(
					File.ReadAllBytes($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.CardNumberFileName}"))),
				CardData.CVV => Encoding.UTF8.GetString(_cryptoService.DecryptData(
					File.ReadAllBytes($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.CVVFileName}"))),
				CardData.Balance => Encoding.UTF8.GetString(_cryptoService.DecryptData(
					File.ReadAllBytes($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.BalanceFileName}"))),
				CardData.PIN => Encoding.UTF8.GetString(_cryptoService.DecryptData(
					File.ReadAllBytes($"{drive.Info?.Name}{BankConstants.BankCardDirectory}\\{BankConstants.PinFileName}"))),
				_ => throw new Exception($"There is not valid data type '{type}'")
			};

		#endregion
	}
}
