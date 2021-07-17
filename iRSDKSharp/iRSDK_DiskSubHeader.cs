namespace iRSDKSharp
{
	using System;
	using iRSDKSharp.Properties;

	public class iRSDK_DiskSubHeader
	{
		#region Fields
		private long sessionStartDate;    // This value is a time_t in irsdk_defines.h
		private double sessionStartTime;
		private double sessionEndTime;
		private int sessionLapCount;
		private int sessionRecordCount;
		#endregion Fields

		#region Constructors
		public iRSDK_DiskSubHeader(long sessionStartDate, double sessionStartTime, double sessionEndTime, int sessionLapCount, int sessionRecordCount)
		{
			this.sessionStartDate = sessionStartDate;
			this.sessionStartTime = sessionStartTime;
			this.sessionEndTime = sessionEndTime;
			this.sessionLapCount = sessionLapCount;
			this.sessionRecordCount = sessionRecordCount;
		}

		public iRSDK_DiskSubHeader(byte[] buffer)
		{
			if(buffer == null)
			{
				throw new ArgumentNullException($"{buffer}");
			}

			if(buffer.Length != iRSDK_Constants.SizeOf_iRSDK_DiskSubHeader)
			{
				throw new ArgumentException(Resources.IncorrectBufferLengthExceptionMessage, $"{buffer}");
			}

			int offset = 0;

			try
			{
				this.sessionStartDate = BitConverter.ToInt64(buffer, offset);
				offset += sizeof(long);
				this.sessionStartTime = BitConverter.ToDouble(buffer, offset);
				offset += sizeof(double);
				this.sessionEndTime = BitConverter.ToDouble(buffer, offset);
				offset += sizeof(double);
				this.sessionLapCount = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.sessionRecordCount = BitConverter.ToInt32(buffer, offset);
			}
			catch(Exception exception)
			{
				throw exception;
			}
		}
		#endregion Constructors

		#region Methods
		public byte[] ToBuffer()
		{
			byte[] buffer = new byte[iRSDK_Constants.SizeOf_iRSDK_DiskSubHeader];
			int offset = 0;

			try
			{
				Buffer.BlockCopy(BitConverter.GetBytes(this.sessionStartDate), 0, buffer, offset, sizeof(long));
				offset += sizeof(long);
				Buffer.BlockCopy(BitConverter.GetBytes(this.sessionStartTime), 0, buffer, offset, sizeof(double));
				offset += sizeof(double);
				Buffer.BlockCopy(BitConverter.GetBytes(this.sessionEndTime), 0, buffer, offset, sizeof(double));
				offset += sizeof(double);
				Buffer.BlockCopy(BitConverter.GetBytes(this.sessionLapCount), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.sessionRecordCount), 0, buffer, offset, sizeof(int));
			}
			catch(Exception exception)
			{
				throw exception;
			}
			
			return buffer;
		}
		#endregion Methods

		#region Properties
		public long SessionStartDate { get { return this.sessionStartDate; } }
		public double SessionStartTime { get { return this.sessionStartTime; } }
		public double SessionEndTime { get { return this.sessionEndTime; } }
		public int SessionLapCount { get { return this.sessionLapCount; } }
		public int SessionRecordCount { get { return this.sessionRecordCount; } }
		#endregion Properties
	}
}
