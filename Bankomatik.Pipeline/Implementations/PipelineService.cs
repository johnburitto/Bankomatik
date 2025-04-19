using Bankomatik.Pipeline.Entities;
using Bankomatik.Pipeline.Interfaces;

namespace Bankomatik.Pipeline.Implementations
{
	/// <summary>
	/// Realization of <see cref="IPipelineService"/>.
	/// </summary>
	public class PipelineService : IPipelineService
	{
		#region Implementation of IPipelineService

		/// <inheritdoc />
		public Task<IPipeline> CreatePipelineAsync()
			=> Task.FromResult<IPipeline>(new AppPipeline());

		#endregion
	}
}
