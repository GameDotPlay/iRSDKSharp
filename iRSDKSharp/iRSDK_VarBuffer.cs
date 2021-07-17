namespace iRSDKSharp
{
	using System;
	using iRSDKSharp.Properties;

	public class iRSDK_VarBuffer
	{
		#region Fields
		private const int PadLength = sizeof(long);

		private int tickCount;    // used to detect changes in data
		private int bufferOffset; // offset from header
		private long pad;       // (16 byte align) (2).
		#endregion Fields

		#region Constructors
		public iRSDK_VarBuffer(int tickCount, int bufferOffset)
		{
			this.tickCount = tickCount;
			this.bufferOffset = bufferOffset;
			this.pad = 0;
		}

		public iRSDK_VarBuffer(byte[] buffer)
		{
			if(buffer == null)
			{
				throw new ArgumentNullException($"{buffer}");
			}

			int offset = 0;

			try
			{
				this.tickCount = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.bufferOffset = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.pad = BitConverter.ToInt64(buffer, offset);
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
			byte[] buffer = new byte[iRSDK_Constants.SizeOf_iRSDK_VarBuffer];

			int offset = 0;

			try
			{
				Buffer.BlockCopy(BitConverter.GetBytes(tickCount), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(bufferOffset), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(pad), 0, buffer, offset, PadLength);
			}
			catch(Exception exception)
			{
				throw exception;
			}

			return buffer;
		}
		#endregion Methods

		#region Properties
		public int TickCount { get { return this.tickCount; } }
		public int BufferOffset { get { return this.bufferOffset; } }
		#endregion Properties
	}
}
