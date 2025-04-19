namespace Bankomatik.Pipeline.Interfaces
{
	/// <summary>
	/// Describe all methods for pipeline service.
	/// </summary>
	public interface IPipelineService
	{
		/// <summary>
		/// Create a pipeline.
		/// </summary>
		/// <returns>App pipeline.</returns>
		Task<IPipeline> CreatePipelineAsync();
	}
}
