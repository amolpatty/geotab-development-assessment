using System;
// removed unused usings

namespace JokeGenerator.Services
{
    /// <summary>
    /// This class is responsible for printing the user interaction messages to the Console
    /// </summary>
    public class ConsolePrinterService : IConsolePrinterService
    {        
        public void PrintMessage(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine(value);
            }
        }

        public void PrintErrorMessage(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                PrettyPrintResults(new string[] { value });
            }
        }

        public void PrintNewline()
        {
            Console.WriteLine(Environment.NewLine);
        }

        public void PrettyPrintResults(string[] results)
        {            
            if (results != null)
            {
                PrintMessage(Constants.ScreenSeparator);

                foreach (string r in results)
                {
                    PrintMessage(Environment.NewLine + r);
                }

                PrintMessage(Constants.ScreenSeparator);
            }
        }

        public void PrettyPrintCategories(string[] results)
        {
            if (results != null)
            {
                PrintMessage(Constants.ScreenSeparator);

                PrintMessage("[" + string.Join(",", results) + "]");

                PrintMessage(Constants.ScreenSeparator);
            }
        }
    }
}
