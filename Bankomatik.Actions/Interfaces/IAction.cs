using Bankomatik.Pipeline.Common.Dtos;

namespace Bankomatik.Actions.Interfaces
{
	/// <summary>
	/// Describe all methods for action.
	/// </summary>
	public interface IAction
	{
		/// <summary>
		/// List of sub-actions.
		/// </summary>
		List<IAction>? SubActions { get; set; }

		/// <summary>
		/// Invoke action.
		/// </summary>
		/// <param name="context">Pipeline context.</param>
		Task<bool> InvokeAsync(PipelineContext context);
	}
}
