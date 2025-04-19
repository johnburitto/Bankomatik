using System.Text.Json;

using Bankomatik.USB.Dtos;
using Bankomatik.Actions.Interfaces;
using Bankomatik.Pipeline.Common.Dtos;

namespace Bankomatik.Actions.Actions
{
	/// <summary>
	/// Create bank card action.
	/// </summary>
	public class CreateBankCardAction : IAction
	{
		#region Public properties

		/// <inheritdoc />
		public List<IAction>? SubActions { get; set; }

		#endregion

		#region Implementation of IAction

		/// <inheritdoc />
		public async Task<bool> InvokeAsync(PipelineContext context)
		{
			var response = await context.HttpClient.GetAsync("/create-bank-card");
			var cardInfo = JsonSerializer.Deserialize<CardInfo>(await response.Content.ReadAsStringAsync());

			context.USBService?.CreateUSBBankCard(context.SelectedDrive ?? throw new ArgumentNullException(nameof(context.SelectedDrive)),
				cardInfo ?? throw new ArgumentNullException(nameof(cardInfo)));

			return true;
		}

		#endregion
	}
}
