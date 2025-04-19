using Bankomatik.Actions.Interfaces;
using Bankomatik.Pipeline.Common.Dtos;

namespace Bankomatik.Actions.Actions
{
	/// <summary>
	/// Detect USB drives action.
	/// </summary>
	public class DetectUSBDrivesAction : IAction
	{
		#region Public properties

		/// <inheritdoc />
		public List<IAction>? SubActions { get; set; }

		#endregion

		#region Implementation of IAction

		/// <inheritdoc />
		public Task<bool> InvokeAsync(PipelineContext context)
		{
			context.Drives = context.USBService?.GetUSBDrives();

			return Task.FromResult(true);
		}

		#endregion
	}
}
