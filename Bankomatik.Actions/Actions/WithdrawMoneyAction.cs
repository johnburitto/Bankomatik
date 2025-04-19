using System.Text.Json;

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

			var response = await context.HttpClient.GetAsync($"/withdraw-money?pin={pin}&withdrawAmount={withdrawAmount}");
			var status = JsonSerializer.Deserialize<WithdrawStatus>(await response.Content.ReadAsStringAsync());

			if (status == WithdrawStatus.Approved)
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
