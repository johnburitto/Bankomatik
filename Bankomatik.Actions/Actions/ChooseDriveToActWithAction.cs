using Bankomatik.Common.Logging;
using Bankomatik.Actions.Interfaces;
using Bankomatik.Pipeline.Common.Dtos;

namespace Bankomatik.Actions.Actions
{
	/// <summary>
	/// Choose drive to act with action.
	/// </summary>
	public class ChooseDriveToActWithAction : IAction
	{
		#region Public properties

		/// <inheritdoc />
		public List<IAction>? SubActions { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Initializa instance of <see cref="ChooseDriveToActWithAction"/> class.
		/// </summary>
		public ChooseDriveToActWithAction()
		{
			SubActions = [];
		}

		#endregion

		#region Implementation of IAction

		/// <inheritdoc />
		public async Task<bool> InvokeAsync(PipelineContext context)
		{
			if (context.Drives != null && context.Drives.Count != 0)
			{
				context.Drives.ForEach(drive =>
				{
					Logger.Information($"Detected USB drive: {drive.Index}. {drive.Info?.Name} [{drive.CardStatus}]");
				});

				while (true)
				{
					Logger.UserInput("Select USB drive to act with => ", out string driveIndex);
					
					try
					{
						context.SelectedDrive = context.Drives[Convert.ToInt32(driveIndex)];

						break;
					}
					catch (Exception)
					{
						Logger.Error("Invalid drive index.");
					}
				}

				return true;
			}
			else
			{
				Logger.Warning("No USB drives detected.");
				Logger.UserInput("Press 'Enter' to continue.", out _);

				foreach (var action in SubActions!)
				{
					await action.InvokeAsync(context);
				}

				return false;
			}
		}

		#endregion
	}
}
