using Bankomatik.Actions.Interfaces;
using Bankomatik.Pipeline.Interfaces;
using Bankomatik.USB.Implementations;
using Bankomatik.Pipeline.Common.Dtos;
using Bankomatik.Crypto.Implementations;

namespace Bankomatik.Pipeline.Entities
{
	/// <summary>
	/// Realization of <see cref="IPipeline"/>.
	/// </summary>
	public class AppPipeline : IPipeline
	{
		#region Private fields

		/// <summary>
		/// Context of pipeline.
		/// </summary>
		private PipelineContext _context;

		#endregion

		#region Public properties

		/// <inheritdoc />
		public List<IAction> Actions { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Inizialise instance of <see cref="AppPipeline"/> class.
		/// </summary>
		public AppPipeline()
		{
			Actions = [];
			_context = CreatePipelineContext();
		}

		#endregion

		#region Implementation of IPipeline

		/// <inheritdoc />
		public IPipeline AddAction<T>() where T : IAction, new()
		{
			Actions.Add(new T());

			return this;
		}

		/// <inheritdoc />
		public IPipeline AddSubAction<TAction, TSubaction>()
			where TAction : IAction
			where TSubaction : IAction, new()
		{
			Actions.FirstOrDefault(action => action.GetType() == typeof(TAction))?.SubActions?.Add(new TSubaction());

			return this;
		}

		/// <inheritdoc />
		public async Task StartAsync()
		{
			while (true)
			{
				foreach (var action in Actions)
				{
					if (!await action.InvokeAsync(_context))
					{
						break;
					}
				}
			}
		}

		#endregion

		#region Private methods

		private PipelineContext CreatePipelineContext()
		{
			var context = new PipelineContext();
			var publicKey = File.ReadAllText("Keys/public.pub").Replace("\n", "").Replace("\r", "");
			var privateKey = File.ReadAllText("Keys/private.pem").Replace("\n", "").Replace("\r", "");

			context.CryptoService = new CryptoService(publicKey, privateKey);
			context.USBService = new USBService(context.CryptoService);

			return context;
		}

		#endregion
	}
}
