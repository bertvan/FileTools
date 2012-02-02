using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileTools
{
	public class CheckFileMaximumLength : ToolBase
	{
		public override string ToolName
		{
			get { return "checkfilemaxlength"; }
		}

		public CheckFileMaximumLength(string[] args): base(args) { }

		public override void Run()
		{
			var maxLength = new ArgsReader().ExtractKeyValue<int>(Args, "max");
			var directoryPath = new ArgsReader().ExtractKeyValue(Args, "dir");

			Console.WriteLine(
				string.Format(
					"Looking for files, longer than {0} in '{1}'",
					maxLength, directoryPath));

			var dir = new DirectoryInfo(directoryPath);

			checkDir(dir, maxLength);
			
		}

		private void checkDir(DirectoryInfo dir, int maxLength)
		{
			foreach (var file in dir.GetFiles())
			{
				if (file.FullName.Length > maxLength)
				{
					Console.WriteLine(file.FullName);
				}
			}

			foreach (var subdir in dir.GetDirectories())
			{
				checkDir(subdir, maxLength);
			}
		}
	}
}
