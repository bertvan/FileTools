using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileTools
{
	class ToolRunner
	{
		private string[] args;
		private Dictionary<string, ToolBase> tools = new Dictionary<string, ToolBase>();

		public ToolRunner(string[] args)
		{
			this.args = args;
		}

		internal void Run()
		{
			loadTools();

			var selectedTool = extractSelectedTool(args);

			if (string.IsNullOrEmpty(selectedTool))
			{
				Console.WriteLine("You did not select a tool, use \"tool=\" to select a tool");
				Console.Read();
				return;
			}

			Console.WriteLine("You selected " + selectedTool);

			if (!tools.ContainsKey(selectedTool))
			{
				Console.WriteLine("Never heard of that");
				Console.Read();
				return;
			}

			var tool = tools[selectedTool];

			tool.Run();
		}

		private void loadTools()
		{
			foreach (var t in this.GetType().Assembly.GetTypes())
			{
				if (typeof(ToolBase).IsAssignableFrom(t) && t != typeof(ToolBase))
				{
					var tool = (ToolBase)Activator.CreateInstance(t, new object[] { args });

					tools.Add(tool.ToolName, tool);
				}
			}
		}

		private string extractSelectedTool(string[] args)
		{
			return new ArgsReader().ExtractKeyValue(args, "tool");
		}
	}
}
