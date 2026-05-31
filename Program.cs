using System.Diagnostics;

namespace StartupApplications
{
    public class Program
    {
        private const string FILE_NAME = "Applications.txt";

        private static void Main()
        {
            if (!File.Exists(FILE_NAME))
            {
                Console.WriteLine($"Файл {FILE_NAME} не был найден.");
                Console.ReadLine();
                return;
            }

            foreach (string line in File.ReadAllLines("Applications.txt"))
            {
                ProcessStartInfo startInfo = new()
                {
                    FileName = line,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    RedirectStandardInput = false,
                };

                Process.Start(startInfo);
            }
        }
    }
}
