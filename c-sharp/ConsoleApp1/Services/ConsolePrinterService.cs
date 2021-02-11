using System;
// removed unused usings

namespace JokeGenerator.Services
{
    /// <summary>
    /// This class is responsible for printing the user interaction messages to the Console
    /// </summary>
    public class ConsolePrinterService : IConsolePrinterService
    {
        private static string printValue;

        public IConsolePrinterService Value(string value)
        {
            printValue = value;
            return this;
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(printValue))
            {
                Console.WriteLine(Environment.NewLine + printValue);
            }
            return null;
        }
    }
}
