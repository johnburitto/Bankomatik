using System.Text;
using System.Security.Cryptography;

using Bankomatik.Crypto.Interfaces;

namespace Bankomatik.Crypto.Implementations
{
	/// <summary>
	/// Realisation of <see cref="ICryptoService"/>.
	/// </summary>
	public class CryptoService : ICryptoService
	{
		#region Private fields

		/// <summary>
		/// RSA public key.
		/// </summary>
		private readonly string _publicKey;

		/// <summary>
		/// RSA private key.
		/// </summary>
		private readonly string _privateKey;

		#endregion

		#region Constructor

		/// <summary>
		/// Inizialise instance of <see cref="CryptoService"/>.
		/// </summary>
		/// <param name="publicKey">RSA public key.</param>
		/// <param name="privateKey">RSA private key.</param>
		public CryptoService(string publicKey, string privateKey)
		{
			_publicKey = publicKey;
			_privateKey = privateKey;
		}

		#endregion

		#region Implementation of ICryptoService

		/// <inheritdoc />
		public byte[] DecryptData(byte[] data)
		{
			using (var rsa = GetRSA())
			{
				return rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1);
			}
		}

		/// <inheritdoc />
		public byte[] EncryptData(byte[] data)
		{
			using (var rsa = GetRSA())
			{
				return rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
			}
		}

		/// <inheritdoc />
		public byte[] HashSHA256(string data, int times = 1)
		{
			byte[] encryptedData = SHA256.HashData(Encoding.UTF8.GetBytes(data));

			for (int i = 0; i < times - 1; i++)
			{
				encryptedData = SHA256.HashData(encryptedData);
			}

			return encryptedData;
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Create and configure RSA.
		/// </summary>
		/// <returns>RSA.</returns>
		private RSA GetRSA()
		{
			RSA rsa = RSA.Create();

			rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(_publicKey), out _);
			rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(_privateKey), out _);

			return rsa;
		}

		#endregion
	}
}
