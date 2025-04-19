namespace Bankomatik.Crypto.Interfaces
{
	/// <summary>
	/// Describe all methods to process crypto operations.
	/// </summary>
	public interface ICryptoService
	{
		/// <summary>
		/// Ecrypt data.
		/// </summary>
		/// <param name="data">Data to encrypt.</param>
		/// <returns>Encrypted data.</returns>
		byte[] EncryptData(byte[] data);

		/// <summary>
		/// Decrypt data.
		/// </summary>
		/// <param name="data">Data to decrypt.</param>
		/// <returns>Decrypted data.</returns>
		byte[] DecryptData(byte[] data);
	}
}
