namespace Bankomatik.Common.Logging
{
	/// <summary>
	/// App logger.
	/// </summary>
	public class Logger
	{
		/// <summary>
		/// Log debug message.
		/// </summary>
		/// <param name="message">Message.</param>
		public static void Debug(string message)
		{
			Console.Write($"[{DateTime.UtcNow}] ");
			Console.BackgroundColor = ConsoleColor.DarkGray;
			Console.Write($" D ");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine($" {message}");
		}

		/// <summary>
		/// Log warning message.
		/// </summary>
		/// <param name="message">Message.</param>
		public static void Warning(string message)
		{
			Console.Write($"[{DateTime.UtcNow}] ");
			Console.BackgroundColor = ConsoleColor.DarkYellow;
			Console.Write($" W ");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine($" {message}");
		}

		/// <summary>
		/// Log error message.
		/// </summary>
		/// <param name="message">Message.</param>
		public static void Error(string message)
		{
			Console.Write($"[{DateTime.UtcNow}] ");
			Console.BackgroundColor = ConsoleColor.DarkRed;
			Console.Write($" E ");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine($" {message}");
		}

		/// <summary>
		/// Log information message.
		/// </summary>
		/// <param name="message">Message.</param>
		public static void Information(string message)
		{
			Console.Write($"[{DateTime.UtcNow}] ");
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.Write($" I ");
			Console.BackgroundColor = ConsoleColor.Black;
			Console.WriteLine($" {message}");
		}

		/// <summary>
		/// Log user input.
		/// </summary>
		/// <param name="message">Message that shown with input.</param>
		/// <param name="input">Data that user enter.</param>
		public static void UserInput(string message, out string input)
		{
			Console.Write($"[{DateTime.UtcNow}] {message}");

			input = Console.ReadLine() ?? "";
		}
	}
}
