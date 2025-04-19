using Bankomatik.Actions.Interfaces;
using Bankomatik.Common.Logging;
using Bankomatik.Pipeline.Common.Dtos;
using Bankomatik.USB.Dtos;
using System.Text.Json;

namespace Bankomatik.Actions.Actions
{
	/// <summary>
	/// Repair bank card action.
	/// </summary>
	public class RepairBankCardAction : IAction
	{
		#region Public properties

		/// <inheritdoc />
		public List<IAction>? SubActions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		#endregion

		#region Implementations of IAction

		/// <inheritdoc />
		public async Task<bool> InvokeAsync(PipelineContext context)
		{
			Logger.UserInput("Input your PIN => ", out string pin);

			var response = await context.HttpClient.GetAsync($"/repair-bank-card?pin={pin}");
			var cardInfo = JsonSerializer.Deserialize<CardInfo>(await response.Content.ReadAsStringAsync());

			context.USBService?.DeleteUSBBankCard(context.SelectedDrive ?? throw new ArgumentNullException(nameof(context.SelectedDrive)));
			context.USBService?.CreateUSBBankCard(context.SelectedDrive ?? throw new ArgumentNullException(nameof(context.SelectedDrive)),
				cardInfo ?? throw new ArgumentNullException(nameof(cardInfo)));

			return true;
		}

		#endregion
	}
}
