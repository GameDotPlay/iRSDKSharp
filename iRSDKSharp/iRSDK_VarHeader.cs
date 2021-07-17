namespace iRSDKSharp
{
	using System;
	using System.Text;
	using iRSDKSharp.Properties;

	public class iRSDK_VarHeader
	{
		#region Fields
		private const int PadLength = sizeof(byte) * 3;

		private int type;           // irsdk_VarType
		private int offset;         // offset from start of buffer row
		private int count;          // number of entrys (array). So length in bytes would be irsdk_VarTypeBytes[type] * count
		private bool countAsTime;
		private byte[] pad;         // (For 16 byte align)

		private byte[] name;        // iRSDK_MaxString
		private byte[] description; // iRSDK_MaxDescription
		private byte[] unit;        // iRSDK_MaxString    // something like "kg/m^2"
		#endregion Fields

		#region Constructors
		public iRSDK_VarHeader(int type, int offset, int count, bool countAsTime, byte[] name, byte[] description, byte[] unit)
		{
			this.type = type;
			this.offset = offset;
			this.count = count;
			this.countAsTime = countAsTime;
			this.pad = new byte[PadLength];
			this.name = name;
			this.description = description;
			this.unit = unit;
		}

		public iRSDK_VarHeader(byte[] buffer)
		{
			if(buffer == null)
			{
				throw new ArgumentNullException($"{buffer}");
			}

			if(buffer.Length != iRSDK_Constants.SizeOf_iRSDK_VarHeader)
			{
				throw new ArgumentException(Resources.IncorrectBufferLengthExceptionMessage, $"{buffer}");
			}

			int offset = 0;
			this.pad = new byte[PadLength];
			this.name = new byte[iRSDK_Constants.iRSDK_MaxString];
			this.description = new byte[iRSDK_Constants.IRSDK_MaxDescription];
			this.unit = new byte[iRSDK_Constants.iRSDK_MaxString];

			try
			{
				this.type = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.offset = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.count = BitConverter.ToInt32(buffer, offset);
				offset += sizeof(int);
				this.countAsTime = BitConverter.ToBoolean(buffer, offset);
				offset += sizeof(bool);
				Buffer.BlockCopy(buffer, offset, pad, 0, PadLength);
				offset += PadLength;
				Buffer.BlockCopy(buffer, offset, name, 0, iRSDK_Constants.iRSDK_MaxString);
				offset += iRSDK_Constants.iRSDK_MaxString;
				Buffer.BlockCopy(buffer, offset, description, 0, iRSDK_Constants.IRSDK_MaxDescription);
				offset += iRSDK_Constants.IRSDK_MaxDescription;
				Buffer.BlockCopy(buffer, offset, unit, 0, iRSDK_Constants.iRSDK_MaxString);
			}
			catch(Exception exception)
			{
				throw exception;
			}
		}
		#endregion Constructors

		#region Methods
		public void Clear()
		{
			type = 0;
			offset = 0;
			count = 0;
			countAsTime = false;
			name = new byte[iRSDK_Constants.iRSDK_MaxString];
			description = new byte[iRSDK_Constants.IRSDK_MaxDescription];
			unit = new byte[iRSDK_Constants.iRSDK_MaxString];
		}

		public byte[] ToBuffer()
		{
			byte[] buffer = new byte[iRSDK_Constants.SizeOf_iRSDK_VarHeader];

			int offset = 0;

			try
			{
				Buffer.BlockCopy(BitConverter.GetBytes(this.type), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.offset), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.count), 0, buffer, offset, sizeof(int));
				offset += sizeof(int);
				Buffer.BlockCopy(BitConverter.GetBytes(this.countAsTime), 0, buffer, offset, sizeof(bool));
				offset += sizeof(bool);
				Buffer.BlockCopy(this.pad, 0, buffer, offset, PadLength);
				offset += PadLength;
				Buffer.BlockCopy(this.name, 0, buffer, offset, iRSDK_Constants.iRSDK_MaxString);
				offset += iRSDK_Constants.iRSDK_MaxString;
				Buffer.BlockCopy(this.description, 0, buffer, offset, iRSDK_Constants.IRSDK_MaxDescription);
				offset += iRSDK_Constants.IRSDK_MaxDescription;
				Buffer.BlockCopy(this.unit, 0, buffer, offset, iRSDK_Constants.iRSDK_MaxString);
			}
			catch(Exception exception)
			{
				throw exception;
			}

			return buffer;
		}
		#endregion Methods

		#region Properties
		public int Type { get { return this.type; } }
		public int Offset { get { return this.offset; } }
		public int Count { get { return this.count; } }
		public bool CountAsTime { get { return this.countAsTime; } }
		public string Name 
		{ 
			get 
			{
				return Encoding.ASCII.GetString(this.name).TrimEnd('\0');
			} 
		}
		public string Description 
		{ 
			get 
			{
				return Encoding.ASCII.GetString(this.description).TrimEnd('\0') + '.';
			} 
		}
		public string Unit 
		{ 
			get 
			{
				return Encoding.ASCII.GetString(this.unit).TrimEnd('\0');
			} 
		}
		#endregion Properties
	}
}
