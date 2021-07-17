namespace iRSDKSharp
{
	using System;
	using System.Text;
	public class TelemetryVariable<T>
	{
		private int offset;
		private string name;
		private string description;
		private string unit;
		private T value;

		public TelemetryVariable() {}

		public TelemetryVariable(iRSDK_VarHeader varHeader, iRSDK_VarBuffer varBuffer)
		{
			this.offset = varHeader.Offset;
			this.name = varHeader.Name;
			this.description = varHeader.Description;
			this.unit = varHeader.Unit;
			this.offset = varHeader.Offset;

			switch(varHeader.Type)
			{
				case (int)Enums.iRSDK_VarType.irsdk_char:

					byte[] temp = new byte[(int)Enums.iRSDK_VarType.irsdk_char * varHeader.Count];
					Buffer.BlockCopy(varBuffer.ToBuffer(), varHeader.Offset, temp, 0, temp.Length);
					this.value = (T)(object)Encoding.ASCII.GetString(temp);
					break;

				case (int)Enums.iRSDK_VarType.irsdk_bool:

					this.value = (T)(object)BitConverter.ToBoolean(varBuffer.ToBuffer(), varHeader.Offset);
					break;

				case (int)Enums.iRSDK_VarType.irsdk_int:

					this.value = (T)(object)BitConverter.ToInt32(varBuffer.ToBuffer(), varHeader.Offset);
					break;

				case (int)Enums.iRSDK_VarType.irsdk_bitField:

					this.value = (T)(object)BitConverter.ToUInt32(varBuffer.ToBuffer(), varHeader.Offset);
					break;

				case (int)Enums.iRSDK_VarType.irsdk_float:

					this.value = (T)(object)BitConverter.ToSingle(varBuffer.ToBuffer(), varHeader.Offset);
					break;

				case (int)Enums.iRSDK_VarType.irsdk_double:

					this.value = (T)(object)BitConverter.ToDouble(varBuffer.ToBuffer(), varHeader.Offset);
					break;

				default:
					this.name = "undefined";
					this.description = "undefined";
					this.unit = "undefined";
					this.offset = -1;
					this.value = default;

					break;
			}
		}
	}
}
