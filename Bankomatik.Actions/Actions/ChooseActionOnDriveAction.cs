using Bankomatik.USB.Dtos;
using Bankomatik.Common.Enums;
using Bankomatik.Common.Logging;
using Bankomatik.Common.Extensions;
using Bankomatik.Actions.Interfaces;
using Bankomatik.Pipeline.Common.Dtos;

namespace Bankomatik.Actions.Actions
{
	/// <summary>
	/// Choose action on drive action.
	/// </summary>
	public class ChooseActionOnDriveAction : IAction
	{
		#region Public properties

		/// <inheritdoc />
		public List<IAction>? SubActions { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Initializa instance of <see cref="ChooseActionOnDriveAction"/> class.
		/// </summary>
		public ChooseActionOnDriveAction()
		{
			SubActions = [];
		}

		#endregion

		#region Implementation of IAction

		/// <inheritdoc />
		public async Task<bool> InvokeAsync(PipelineContext context)
		{
			GetActionsOnDrive(context.SelectedDrive)
				.Select((action, index) => new { Action = action, Index = index + 1 })
				.ToList().ForEach(action =>
				{
					Logger.Information($"{action.Index}. {action.Action}");
				});

			while (true)
			{
				Logger.UserInput("Select action that you want to perform on drive/card => ", out string actionIndex);

				try
				{
					await SubActions![Convert.ToInt32(actionIndex) - 1].InvokeAsync(context);

					break;
				}
				catch (Exception)
				{
					Logger.Error("Invalid drive index.");
				}
			}

			Logger.UserInput("Press 'Enter' to continue.", out _);

			return true;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Get actions on drive.
		/// </summary>
		/// <param name="drive">Drive info.</param>
		/// <returns>List of actions.</returns>
		private List<string> GetActionsOnDrive(USBDriveInfo? drive)
		{
			var actions = string.Empty;

			if (!drive!.IsBankCard)
			{
				actions += "Create bank card.\n";
			}
			else if (drive.IsBankCard)
			{
				actions += "Withdraw money.\n";
				actions += "Delete bank card.\n";
			}

			return actions.Split("\n").ToList();
		}

		#endregion
	}
}
