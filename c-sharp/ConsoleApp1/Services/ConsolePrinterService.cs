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

                int rowCount = 0;
                foreach (string r in results)
                {
                    // print new line prior to the text only if it is the second item or above
                    if (rowCount > 0)
                    {
                        PrintMessage(Environment.NewLine);
                        PrintMessage(Constants.ScreenSeparator);
                    }

                    PrintMessage(r);

                    rowCount++;
                }

                PrintMessage(Constants.ScreenSeparator);
                PrintMessage(Environment.NewLine);
            }
        }

        public void PrettyPrintCategories(string[] results)
        {
            if (results != null)
            {
                PrintMessage(Constants.ScreenSeparator);

                PrintMessage("[" + string.Join(",", results) + "]");

                PrintMessage(Constants.ScreenSeparator);
                PrintMessage(Environment.NewLine);
            }
        }
    }
}
