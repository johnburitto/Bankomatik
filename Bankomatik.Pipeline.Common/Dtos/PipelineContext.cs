using Bankomatik.USB.Dtos;
using Bankomatik.USB.Interfaces;
using Bankomatik.Crypto.Interfaces;

namespace Bankomatik.Pipeline.Common.Dtos
{
	/// <summary>
	/// Context class for pipeline actions.
	/// </summary>
	public class PipelineContext
	{
		/// <summary>
		/// List of USB drives.
		/// </summary>
		public List<USBDriveInfo>? Drives { get; set; }

		/// <summary>
		/// Selected USB drive.
		/// </summary>
		public USBDriveInfo? SelectedDrive { get; set; }

		/// <summary>
		/// Selected action.
		/// </summary>
		public HttpClient HttpClient { get; set; } = new HttpClient()
		{
			BaseAddress = new Uri("http://localhost:8080")
		};

		/// <summary>
		/// Crypto service.
		/// </summary>
		public ICryptoService? CryptoService { get; set; }

		/// <summary>
		/// USB service.
		/// </summary>
		public IUSBService? USBService { get; set; }
	}
}
