namespace iRSDKSharp
{
	using System;
	using iRSDKSharp.Properties;

	public class iRSDK_Header
	{
		#region Fields
		private const int PadLength = sizeof(long);

		private int version;            // this api header version, see IRSDK_VER
		private int status;             // bitfield using irsdk_StatusField
		private int tickRate;           // ticks per second (60 or 360 etc)

		// session information, updated periodicaly
		private int sessionInfoUpdate;  // Incremented when session info changes
		private int sessionInfoLength;  // Length in bytes of session info string
		private int sessionInfoOffset;  // Session info, encoded in YAML format

		// State data, output at tickRate
		private int numVars;            // length of array pointed to by varHeaderOffset
		private int varHeaderOffset;    // offset to irsdk_varHeader[numVars] array, Describes the variables received in varBuf

		private int numBuffers;         // <= IRSDK_MAX_BUFS (3 for now)
		private int bufferLength;       // length in bytes for one line
		private long pad;            // (For 16 byte align)
		private iRSDK_VarBuffer[] varBuffer;    // IRSDK_MAX_BUFS. Buffers of data being written to
		#endregion Fields

		#region Constructors
		public iRSDK_Header(
					int version,
					int status,
					int tickRate,
					int sessionInfoUpdate,
					int sessionInfoLength,
					int sessionInfoOffset,
					int numVars,
					int varHeaderOffset,
					int numBuffers,
					int bufferLength,
					iRSDK_VarBuffer[] varBuffer)
		{
			this.version = version;
			this.status = status;
			this.tickRate = tickRate;
			this.sessionInfoUpdate = sessionInfoUpdate;
			this.sessionInfoLength = sessionInfoLength;
			this.sessionInfoOffset = sessionInfoOffset;
			this.numVars = numVars;
			this.varHeaderOffset = varHeaderOffset;
			this.numBuffers = numBuffers;
			this.bufferLength = bufferLength;
			this.pad = 0;
			this.varBuffer = varBuffer;
		}

		public iRSDK_Header(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException($"{buffer}");
			}

			if (buffer.Length != iRSDK_Constants.SizeOf_iRSDK_Header)
			{
				throw new ArgumentException(Resources.IncorrectBufferLengthExceptionMessage, $"{buffer}");
			}

			this.varBuffer = new iRSDK_VarBuffer[iRSDK_Constants.iRSDK_MaxBuffers];
			int offset = 0;

			try
			{
				this.version = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.status = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.tickRate = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.sessionInfoUpdate = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.sessionInfoLength = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.sessionInfoOffset = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.numVars = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.varHeaderOffset = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.numBuffers = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.bufferLength = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.pad = BitConverter.ToInt64(buffer, offset);
				offset += PadLength;

				for(int i = 0; i < varBuffer.Length; i++)
				{
					byte[] temp = new byte[iRSDK_Constants.SizeOf_iRSDK_VarBuffer];
					Buffer.BlockCopy(buffer, offset, temp, 0, iRSDK_Constants.SizeOf_iRSDK_VarBuffer);

					varBuffer[i] = new iRSDK_VarBuffer(temp);

					offset += iRSDK_Constants.SizeOf_iRSDK_VarBuffer;
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}
		}
		#endregion Constructors

		#region Methods
		public byte[] ToBuffer()
		{
			byte[] buffer = new byte[iRSDK_Constants.SizeOf_iRSDK_Header];

			int offset = 0;

			try
			{
				Buffer.BlockCopy(BitConverter.GetBytes(this.version), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.status), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.tickRate), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.sessionInfoUpdate), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.sessionInfoLength), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.sessionInfoOffset), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.numVars), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.varHeaderOffset), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.numBuffers), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.bufferLength), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.pad), 0, buffer, offset, PadLength);
				offset += PadLength;

				foreach (iRSDK_VarBuffer varBuf in varBuffer)
				{
					Buffer.BlockCopy(varBuf.ToBuffer(), 0, buffer, offset, iRSDK_Constants.SizeOf_iRSDK_VarBuffer);
					offset += iRSDK_Constants.SizeOf_iRSDK_VarBuffer;
				}
			}
			catch (Exception exception)
			{
				throw exception;
			}

			return buffer;
		}
		#endregion Methods

		#region Properties

		public int Version { get { return this.version; } }
		public int Status { get { return this.status; } }
		public int TickRate { get { return this.tickRate; } }
		public int SessionInfoUpdate { get { return this.sessionInfoUpdate; } }
		public int SessionInfoLength { get { return this.sessionInfoLength; } }
		public int SessionInfoOffset { get { return this.sessionInfoOffset; } }
		public int NumVars { get { return this.numVars; } }
		public int VarHeaderOffset { get { return this.varHeaderOffset; } }
		public int NumBuffers { get { return this.numBuffers; } }
		public int BufferLength { get { return this.bufferLength; } }
		public iRSDK_VarBuffer[] VarBuffer { get { return this.varBuffer; } }
		#endregion Properties
	}
}
