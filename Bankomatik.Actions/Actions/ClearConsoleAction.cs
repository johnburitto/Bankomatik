using Bankomatik.Actions.Interfaces;
using Bankomatik.Pipeline.Common.Dtos;

namespace Bankomatik.Actions.Actions
{
	/// <summary>
	/// Action to clear the console.
	/// </summary>
	public class ClearConsoleAction : IAction
	{
		#region Public properties

		/// <inheritdoc />
		public List<IAction>? SubActions { get; set; }

		#endregion

		#region Implementation of IAction

		/// <inheritdoc />
		public Task<bool> InvokeAsync(PipelineContext context)
		{
			Console.Clear();

			return Task.FromResult(true);
		}

		#endregion
	}
}
