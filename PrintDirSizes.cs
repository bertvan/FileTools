using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileTools
{
    public class PrintDirSizes : ToolBase
    {
        public PrintDirSizes(string[] args): base(args) { }

        Dictionary<string, long> directorySizes = new Dictionary<string, long>();

        public override string ToolName
        {
            get { return "printdirsizes"; }
        }

        public override void Run()
        {
            var root = new ArgsReader().ExtractKeyValue(Args, "root");
            var depth = new ArgsReader().ExtractKeyValue<int>(Args, "depth");

            if (string.IsNullOrWhiteSpace(root))
            {
                Console.Write("Please specify root");
                Console.Read();
                return;
            }

            var directory = new DirectoryInfo(root);

            if (!directory.Exists)
            {
                Console.WriteLine("Root does not exist");
                Console.Read();
                return;
            }

            GetSize(directory);

            PrintSize(directory, 0, depth);

            Console.WriteLine("Finished! Press any key");
            Console.Read();
        }

        private void PrintSize(DirectoryInfo directory, int currentDepth, int requestedDepth)
        {
            var directoryFullName = directory.FullName;
            if (!directorySizes.ContainsKey(directoryFullName))
            {
                return;
            }

            var size = directorySizes[directoryFullName];

            var sizeText = (size / 1024).ToString("#.##");
            sizeText = sizeText.PadLeft(10);
            var line = string.Format("[{0} KB] {1}", sizeText, directoryFullName);

            Console.WriteLine(line);

            if (requestedDepth == 0 || requestedDepth > currentDepth + 1)
            {
                currentDepth++;

                foreach (var subdir in directory.GetDirectories())
                {
                    PrintSize(subdir, currentDepth, requestedDepth);
                }
            }
        }

        private long GetSize(DirectoryInfo directory)
        {
            var size = 0L;

            foreach (FileInfo file in directory.GetFiles())
            {
                size += file.Length;
            }

            foreach (var subdir in directory.GetDirectories())
            {
                var subdirSize = GetSize(subdir);
                size += subdirSize;
            }

            directorySizes.Add(directory.FullName, size);

            return size;
        }

        private double size = 0;

        private double GetDirectorySize(string directory)
        {
            foreach (string dir in Directory.GetDirectories(directory))
            {
                GetDirectorySize(dir);
            }

            foreach (FileInfo file in new DirectoryInfo(directory).GetFiles())
            {
                size += file.Length;
            }

            return size;
        }
    }
}
