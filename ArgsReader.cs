using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileTools
{
	public class ArgsReader
	{
		public KeyValuePair<string, string> ExtractKeyValuePair(string arg)
		{
			var indexOfEquals = arg.IndexOf("=");

			return new KeyValuePair<string, string>(
				arg.Substring(0, indexOfEquals),
				arg.Remove(0, indexOfEquals + 1));
		}

		public T ExtractKeyValue<T>(string[] args, string key)
		{
			var value = ExtractKeyValue(args, key);

			return (T)Convert.ChangeType(value, typeof(T));
		}

		public string ExtractKeyValue(string[] args, string key)
		{
			for (int i = 0; i < args.Length; i++)
			{
				var arg = args[i];
				var kvp = ExtractKeyValuePair(arg);

				if (kvp.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
				{
					return kvp.Value;
				}
			}

			return null;
		}
	}
}