namespace TelemetryReadTest
{
	using System;
	using System.IO;
	using iRSDKSharp;

	class Program
	{
		

		static void Main(string[] args)
		{
			string csvOutputPath = Path.GetFullPath(@"Test csv Output");
			string ibtInputPath = Path.GetFullPath(args[0]);
			string ibtOutputPath = Path.GetFullPath($"{args[0]}\\Sharp Output");

			TelemetrySession telemetrySession;

			try
			{
				foreach(string filePath in Directory.GetFiles(ibtInputPath, "*.ibt", SearchOption.TopDirectoryOnly))
				{
					telemetrySession = new TelemetrySession(filePath);

					telemetrySession.WriteToBinaryFile(ibtOutputPath);
				}
			}
			catch (Exception exception)
			{
				Console.WriteLine($"Failed to read in telemetry file. {exception.Message}");
				return;
			}
		}
	}
}
