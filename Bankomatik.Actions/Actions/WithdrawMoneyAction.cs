using System.Text.Json;

using Bankomatik.Common.Dtos;
using Bankomatik.Common.Enums;
using Bankomatik.Common.Logging;
using Bankomatik.Common.Extensions;
using Bankomatik.Actions.Interfaces;
using Bankomatik.Pipeline.Common.Dtos;

namespace Bankomatik.Actions.Actions
{
	/// <summary>
	/// Withdraw money action.
	/// </summary>
	public class WithdrawMoneyAction : IAction
	{
		#region Public properties

		public List<IAction>? SubActions { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		#endregion

		#region Implementations of IAction

		public async Task<bool> InvokeAsync(PipelineContext context)
		{
			Logger.UserInput("Input your PIN => ", out string pin);
			Logger.UserInput("Input withdraw amount => ", out string withdrawAmount);

			var requesDto = new ServerRequestDto
			{
				CardNumber = context.USBService?.GetCardData(context.SelectedDrive ?? throw new ArgumentNullException(nameof(context.SelectedDrive)),
					CardData.CardNumber),
				CVV = context.USBService?.GetCardData(context.SelectedDrive ?? throw new ArgumentNullException(nameof(context.SelectedDrive)),
					CardData.CVV),
				PVV = context.CryptoService?.HashSHA256(pin, 3).ToPVV(),
				Amount = withdrawAmount.ToFloat(),
			};
			var response = await context.HttpClient.PostAsync($"/withdraw-money", requesDto.ToHttpBody());
			var status = await response.Content.ReadAsStringAsync();

			if (status.Replace("\"", "") == WithdrawStatus.Approved.ToString().ToUpper())
			{
				context.USBService?.WithdrawMoney(context.SelectedDrive ?? throw new ArgumentNullException(nameof(context.SelectedDrive)),
					withdrawAmount.ToFloat());

				Logger.Information("Withdraw completed successfully.");
			}
			else
			{
				Logger.Warning("Withdraw not approved.");
			}

			return true;
		}

		#endregion
	}
}
