using Bankomatik.Actions.Interfaces;

namespace Bankomatik.Pipeline.Interfaces
{
	/// <summary>
	///  Describe all methods for pipeline service.
	/// </summary>
	public interface IPipeline
	{
		/// <summary>
		/// List of all pipeline actions.
		/// </summary>
		List<IAction> Actions { get; set; }

		/// <summary>
		/// Add action to pipeline.
		/// </summary>
		/// <typeparam name="T">Action type.</typeparam>
		/// <returns>App pipeline.</returns>
		IPipeline AddAction<T>() where T : IAction, new();

		/// <summary>
		/// Add sub-action to previous action.
		/// </summary>
		/// <typeparam name="T">Action type.</typeparam>
		/// <returns>App pipeline.</returns>
		IPipeline AddSubAction<T>() where T : IAction, new();

		/// <summary>
		/// Start pipeline.
		/// </summary>
		Task StartAsync();
	}
}
