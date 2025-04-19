using Bankomatik.Actions.Interfaces;
using Bankomatik.Pipeline.Common.Dtos;

namespace Bankomatik.Actions.Actions
{
	/// <summary>
	/// Delete bank card action.
	/// </summary>
	public class DeleteBankCardAction : IAction
	{
		#region Public properties

		/// <inheritdoc />
		public List<IAction>? SubActions { get; set; }

		#endregion

		#region Implementations of IAction

		/// <inheritdoc />
		public Task<bool> InvokeAsync(PipelineContext context)
		{
			context.USBService?.DeleteUSBBankCard(context.SelectedDrive ?? throw new ArgumentNullException(nameof(context.SelectedDrive)));

			return Task.FromResult(true);
		}

		#endregion
	}
}
