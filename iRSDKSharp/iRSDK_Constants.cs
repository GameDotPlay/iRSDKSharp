namespace iRSDKSharp
{
	public static class iRSDK_Constants
	{
		public const int iRSDK_MaxBuffers = 4;
		public const int iRSDK_MaxString = 32;
		public const int IRSDK_MaxDescription = 64;  // descriptions can be longer than max_string!

		public const int SizeOf_iRSDK_Header = 112;
		public const int SizeOf_iRSDK_DiskSubHeader = 32;
		public const int SizeOf_iRSDK_VarHeader = 144;
		public const int SizeOf_iRSDK_VarBuffer = 16;

		// define markers for unlimited session lap and time
		public const int iRSDK_UnlimitedLaps = 32767;
		public const float iRSDK_UnlimitedTime = 604800.0f;

		// latest version of our telemetry headers
		public const int iRSDK_Version = 2;

		public static int[] iRSDK_VarTypeBytes = new int[(int)Enums.iRSDK_VarType.irsdk_ETCount]
		{
			1,	// irsdk_char
			1,	// irsdk_bool

			4,	// irsdk_int
			4,	// irsdk_bitField
			4,	// irsdk_float

			8	// irsdk_double
		};
	}
}
