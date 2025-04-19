using System.Text;
using System.Text.Json;

using Bankomatik.Common.Dtos;

namespace Bankomatik.Common.Extensions
{
	/// <summary>
	/// DTO extensions.
	/// </summary>
	public static class DtoExtensions
	{
		/// <summary>
		/// Converts <see cref="ServerRequestDto"/> to <see cref="HttpContent"/>.
		/// </summary>
		/// <param name="dto">Dto.</param>
		/// <returns>Request body content.</returns>
		public static HttpContent ToHttpBody(this ServerRequestDto dto)
		{
			var json = JsonSerializer.Serialize(dto);

			return new StringContent(json, Encoding.UTF8, "application/json");
		}
	}
}
