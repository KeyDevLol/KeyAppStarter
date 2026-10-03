using System.Diagnostics;
using WindowsShortcutFactory;

namespace KeyAppStarter
{
    public class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments has been provided");
                Console.WriteLine("Example use: StartupApplications \"C:\\Path\\To\\Instruction.txt\"");
                Console.WriteLine("Help:");
                Console.WriteLine(" -s <path create shortcut> <path to instruction>  |   Creates a shortcut/bash with the specified instruction");
                return;
            }

            if (args[0] == "-s")
            {
                if (args.Length == 3)
                {
                    CreateShortcut(args[1], args[2]);
                }
                else
                {
                    Console.WriteLine("Not enough arguments has been provided");
                    Console.WriteLine("Help:");
                    Console.WriteLine(" -s <path create shortcut> <path to instruction>  |   Creates a shortcut with the specified instruction");
                }

                return;
            }

            string filePath = args[0];

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"The instruction file at path {filePath} was not found.");
                Console.ReadKey();
                return;
            }

            bool hasErrors = false;

            foreach (string line in File.ReadAllLines(filePath))
            {
                if (IsStringUrl(line))
                {
                    bool successfully = StartUrl(line);

                    if (!hasErrors)
                        hasErrors = !successfully;
                }
                else
                {
                    bool successfully = StartApplication(line);

                    if (!hasErrors)
                        hasErrors = !successfully;
                }
            }

            if (hasErrors)
                Console.ReadKey();
        }

        private static bool StartApplication(string rawLine)
        {
            string executablePath = rawLine.Replace("\"", string.Empty);

            ProcessStartInfo startInfo = new()
            {
                FileName = executablePath,
                WorkingDirectory = Path.GetDirectoryName(executablePath),
                RedirectStandardOutput = false,
                RedirectStandardError = false,
                RedirectStandardInput = false,
                UseShellExecute = true
            };

            try
            {
                Process.Start(startInfo);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Failed to start process {executablePath} with error: {exc}.");
                return false;
            }

            return true;
        }

        private static bool StartUrl(string rawLine)
        {
            ProcessStartInfo startInfo = new()
            {
                FileName = rawLine,
                UseShellExecute = true
            };
            try
            {
                Process.Start(startInfo);
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Failed to open URL {rawLine} with error: {exc}.");
                return false;
            }

            return true;
        }

        private static void CreateShortcut(string lnkPath, string instructionPath)
        {
            if (OperatingSystem.IsWindows())
            {
                using WindowsShortcut shortcut = new()
                {
                    Path = Environment.ProcessPath,
                    Arguments = $"\"{instructionPath}\""
                };

                shortcut.Save(lnkPath.EndsWith(".lnk") ? lnkPath : lnkPath + ".lnk");
            }

            if (OperatingSystem.IsLinux())
            {
                var targetPath = lnkPath.EndsWith(".sh") ? lnkPath : lnkPath + ".sh";
                var processPath = Environment.ProcessPath;

                var script = $"""
                    #!/bin/bash
                    exec "{processPath}" "{instructionPath}" "$@"
                    """;

                File.WriteAllText(targetPath, script.Replace("\r\n", "\n"));
                File.SetUnixFileMode(targetPath,
                    UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute |
                    UnixFileMode.GroupRead | UnixFileMode.GroupExecute |
                    UnixFileMode.OtherRead | UnixFileMode.OtherExecute);
            }
        }

        private static bool IsStringUrl(string urlString)
        {
            if (string.IsNullOrWhiteSpace(urlString))
                return false;

            bool isUrl = Uri.TryCreate(urlString, UriKind.Absolute, out Uri? result)
                           && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);

            return isUrl;
        }
    }
}