using System;
// removed unused usings

namespace JokeGenerator.Services
{
    /// <summary>
    /// This class is responsible for printing the user interaction messages to the Console
    /// </summary>
    public class ConsolePrinterService : IConsolePrinterService
    {        
        public void PrintValue(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine(value);
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
                PrintValue(Constants.ScreenSeparator);

                foreach (string r in results)
                {
                    PrintValue(Environment.NewLine + r);
                }

                PrintValue(Constants.ScreenSeparator);
            }
        }        
    }
}
