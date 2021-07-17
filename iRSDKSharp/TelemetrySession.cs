namespace iRSDKSharp
{
	using System;
	using System.IO;
	using System.Text;
	using System.Collections.Generic;

	public class TelemetrySession
	{
		string fullPath;
		private iRSDK_Header header;
		private iRSDK_DiskSubHeader diskSubHeader;
		private string sessionString;
		private iRSDK_VarHeader[] varHeaders;
		private List<byte[]> varLines;

		public TelemetrySession() { }

		public TelemetrySession(string filePath)
		{
			if(filePath == null)
			{
				throw new ArgumentNullException($"{filePath}");
			}

			if(filePath.Length == 0)
			{
				throw new ArgumentNullException($"{filePath}");
			}

			this.fullPath = Path.GetFullPath(filePath);
			this.varLines = new List<byte[]>();

			try
			{
				using (BinaryReader file = new BinaryReader(new FileStream(this.fullPath, FileMode.Open, FileAccess.Read), Encoding.ASCII))
				{
					// Read a header sized array of bytes from the file.
					this.header = new iRSDK_Header(file.ReadBytes(iRSDK_Constants.SizeOf_iRSDK_Header));

					// Read a disk sub header size array of bytes from the file.
					this.diskSubHeader = new iRSDK_DiskSubHeader(file.ReadBytes(iRSDK_Constants.SizeOf_iRSDK_DiskSubHeader));

					// Set the file position at the session info offset.
					file.BaseStream.Position = this.header.SessionInfoOffset;

					// Read in the entire sessionInfoString and decode it into a string.
					byte[] sessionInfoBuffer = file.ReadBytes(this.header.SessionInfoLength);
					this.sessionString = Encoding.ASCII.GetString(sessionInfoBuffer);

					varHeaders = new iRSDK_VarHeader[this.header.NumVars];

					// Set the file position at the varHeader offset.
					file.BaseStream.Position = this.header.VarHeaderOffset;

					// For each variable, read in the varHeader.
					for (int i = 0; i < this.header.NumVars; i++)
					{
						byte[] buffer = new byte[iRSDK_Constants.SizeOf_iRSDK_VarHeader];
						buffer = file.ReadBytes(buffer.Length);
						varHeaders[i] = new iRSDK_VarHeader(buffer);
					}

					// Set the file position at the varBuffer offset.
					file.BaseStream.Position = this.header.VarBuffer[0].BufferOffset;

					// Get lines of variables until the EOF.
					byte[] varBuf = file.ReadBytes(header.BufferLength);
					while (varBuf.Length == this.header.BufferLength)
					{
						this.varLines.Add(varBuf);
						varBuf = file.ReadBytes(header.BufferLength);
					}
				}
			}
			catch(Exception exception)
			{
				throw exception;
			}
		}

		public void WriteToBinaryFile(string outputPath = "")
		{
			string outputFilePath;
			if(outputPath.Length > 0)
			{
				outputFilePath = Path.Combine(outputPath, $"Sharp-{this.Filename}");
			}
			else
			{
				outputFilePath = $"Sharp-{this.Filename}";
			}

			outputFilePath = Path.ChangeExtension(outputFilePath, "ibt");

			using (BinaryWriter binaryFileOut = new BinaryWriter(new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write), Encoding.ASCII))
			{
				binaryFileOut.Write(this.header.ToBuffer());
				binaryFileOut.Write(this.diskSubHeader.ToBuffer());
				binaryFileOut.BaseStream.Position = this.header.SessionInfoOffset;
				binaryFileOut.Write(this.sessionString.ToCharArray());
				binaryFileOut.BaseStream.Position = header.VarHeaderOffset;
				
				for(int i = 0; i < this.header.NumVars; i++)
				{
					binaryFileOut.Write(this.varHeaders[i].ToBuffer());
				}

				binaryFileOut.BaseStream.Position = this.header.VarBuffer[0].BufferOffset;

				for(int i = 0; i < this.varLines.Count; i++)
				{
					binaryFileOut.Write(this.varLines[i]);
				}
			}
		}

		public void WriteToCsv(string outputPath = "")
		{
			string outputFilePath;
			if (outputPath.Length > 0)
			{
				outputFilePath = Path.Combine(outputPath, $"Sharp-{this.Filename}");
			}
			else
			{
				outputFilePath = $"Sharp-{ this.Filename}";
			}

			// Create a .csv file name by just replacing ".ibt" with ".csv"
			outputFilePath = Path.ChangeExtension(outputFilePath, "csv");

			using (StreamWriter fout = new StreamWriter(outputFilePath))
			{
				this.WriteCSVSessionString(fout);

				this.WriteCSVVarHeaders(fout);

				for(int i = 0; i < this.varLines.Count; i++)
				{
					this.WriteCSVData(fout, this.varLines[i]);
				}
			}
		}

		private void WriteCSVSessionString(StreamWriter fout)
		{
			if (fout == null)
			{
				throw new ArgumentNullException($"{fout}");
			}

			if (this.sessionString.Length > 0)
			{
				// Remove trailing ... from string.
				this.sessionString = sessionString.Replace("...\n", "");
				fout.Write(sessionString);
			}
			else
			{
				fout.WriteLine("---");
			}

			if (diskSubHeader != null)
			{
				fout.WriteLine("SessionLogInfo:");

				DateTime localTime = new DateTime(this.diskSubHeader.SessionStartDate).ToLocalTime();
				fout.WriteLine($" SessionStartDate: {localTime.ToString()}");
				fout.WriteLine($" SessionStartTime: {this.diskSubHeader.SessionStartTime}");
				fout.WriteLine($" SessionEndTime: {this.diskSubHeader.SessionEndTime}");
				fout.WriteLine($" SessionLapCountL {this.diskSubHeader.SessionLapCount}");
				fout.WriteLine($" SessionRecordCount: {this.diskSubHeader.SessionRecordCount}");
			}

			// Re-add the "..." at the end of the string which marks the end of a YAML string.
			fout.WriteLine("...");
		}

		private void WriteCSVVarHeaders(StreamWriter fout)
		{
			if (fout == null)
			{
				throw new ArgumentNullException($"{fout}");
			}

			for (int i = 0; i < this.header.NumVars; i++)
			{
				int count = (this.varHeaders[i].Type == (int)Enums.iRSDK_VarType.irsdk_char) ? 1 : this.varHeaders[i].Count;
				for (int j = 0; j < count; j++)
				{
					if ((i + j) > 0)
						fout.Write(",");

					if (count > 1)
						fout.Write(String.Format("{0}_{1:D2}", this.varHeaders[i].Name, j));   //fprintf(file, "%s_%02d", varHeaders[i].name, j);
					else
						fout.Write(this.varHeaders[i].Name);
				}
			}

			fout.Write("\n");

			for (int i = 0; i < this.header.NumVars; i++)
			{
				int count = (this.varHeaders[i].Type == (int)Enums.iRSDK_VarType.irsdk_char) ? 1 : this.varHeaders[i].Count;
				for (int j = 0; j < count; j++)
				{
					if ((i + j) > 0)
						fout.Write(",");

					fout.Write(this.varHeaders[i].Description);
				}
			}

			fout.Write("\n");

			for (int i = 0; i < this.header.NumVars; i++)
			{
				int count = (this.varHeaders[i].Type == (int)Enums.iRSDK_VarType.irsdk_char) ? 1 : this.varHeaders[i].Count;
				for (int j = 0; j < count; j++)
				{
					if ((i + j) > 0)
						fout.Write(",");

					fout.Write(this.varHeaders[i].Unit);
				}
			}

			fout.Write("\n");

			for (int i = 0; i < this.header.NumVars; i++)
			{
				int count = (this.varHeaders[i].Type == (int)Enums.iRSDK_VarType.irsdk_char) ? 1 : this.varHeaders[i].Count;
				for (int j = 0; j < count; j++)
				{
					if ((i + j) > 0)
						fout.Write(",");

					switch (this.varHeaders[i].Type)
					{
						case (int)Enums.iRSDK_VarType.irsdk_char: fout.Write("string"); break;
						case (int)Enums.iRSDK_VarType.irsdk_bool: fout.Write("boolean"); break;
						case (int)Enums.iRSDK_VarType.irsdk_int: fout.Write("integer"); break;
						case (int)Enums.iRSDK_VarType.irsdk_bitField: fout.Write("bitfield"); break;
						case (int)Enums.iRSDK_VarType.irsdk_float: fout.Write("float"); break;
						case (int)Enums.iRSDK_VarType.irsdk_double: fout.Write("double"); break;
						default: fout.Write("unknown"); break;
					}
				}
			}

			fout.Write("\n");
		}

		private void WriteCSVData(StreamWriter fout, byte[] lineBuf)
		{
			iRSDK_VarHeader record;

			for (int i = 0; i < this.header.NumVars; i++)
			{
				record = this.varHeaders[i];

				int count = (record.Type == (int)Enums.iRSDK_VarType.irsdk_char) ? 1 : record.Count;
				for (int j = 0; j < count; j++)
				{
					if ((i + j) > 0)
						fout.Write(",");

					switch (record.Type)
					{
						case (int)Enums.iRSDK_VarType.irsdk_char:
							fout.Write(lineBuf[record.Offset]); break;
						case (int)Enums.iRSDK_VarType.irsdk_bool:
							int result = BitConverter.ToBoolean(lineBuf, record.Offset) ? 1 : 0;
							fout.Write(result); break;
						case (int)Enums.iRSDK_VarType.irsdk_int:
							fout.Write(BitConverter.ToInt32(lineBuf, record.Offset)); break;
						case (int)Enums.iRSDK_VarType.irsdk_bitField:
							fout.Write(BitConverter.ToUInt32(lineBuf, record.Offset)); break;
						case (int)Enums.iRSDK_VarType.irsdk_float:
							fout.Write(String.Format("{0:G}", BitConverter.ToSingle(lineBuf, record.Offset))); break;
						case (int)Enums.iRSDK_VarType.irsdk_double:
							fout.Write(String.Format("{0:G}", BitConverter.ToDouble(lineBuf, record.Offset))); break;
						default:
							fout.WriteLine("found unknown type!"); break;
					}
				}
			}

			fout.Write("\n");
		}

		public iRSDK_Header Header
		{
			get => this.header;

			set
			{
				if(value == null) throw new ArgumentNullException($"{value}");

				this.header = value;
			}
		}

		public iRSDK_DiskSubHeader DiskSubHeader
		{
			get => this.diskSubHeader;

			set
			{
				if (value == null) throw new ArgumentNullException($"{value}");

				this.diskSubHeader = value;
			}
		}

		public string SessionString
		{
			get => this.sessionString;

			set
			{
				if (value == null) throw new ArgumentNullException($"{value}");

				this.sessionString = value;
			}
		}

		public iRSDK_VarHeader[] VarHeaders
		{
			get => this.varHeaders;

			set
			{
				if (value == null) throw new ArgumentNullException($"{value}");

				this.varHeaders = value;
			}
		}

		public string FullPath
		{
			get => Path.GetFullPath(this.fullPath);
		}

		public string Filename
		{
			get
			{
				return Path.GetFileName(this.fullPath);
			}
		}
	}
}
