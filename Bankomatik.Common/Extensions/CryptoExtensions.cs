using System.Text;
using System.Numerics;

namespace Bankomatik.Common.Extensions
{
	/// <summary>
	/// Extensions for crypto operations.
	/// </summary>
	public static class CryptoExtensions
	{
		/// <summary>
		/// Convert byte array to PVV.
		/// </summary>
		/// <param name="pin">Pin.</param>
		/// <returns>String.</returns>
		public static string ToPVV(this byte[] pin)
		{

			string number = new BigInteger(pin, isBigEndian: true).ToString();
			StringBuilder sb = new StringBuilder();
			char[] charArray = number.ToCharArray();

			for (int i = 0; i < charArray.Length; i++)
			{
				if (i % 2 == 0)
				{
					sb.Append(charArray[i]);
				}
			}

			return (BigInteger.Parse(sb.ToString()) % new BigInteger(10000)).ToString();
		}
	}
}
