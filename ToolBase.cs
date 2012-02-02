using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileTools
{
	public abstract class ToolBase
	{
		protected string[] Args { get; set; }

		public ToolBase(string[] args)
		{
			this.Args = args;
		}

		public abstract string ToolName { get; }

		public abstract void Run();
	}
}
