using System;
// removed unused usings

namespace ConsoleApp1
{
    /// <summary>
    /// This class is responsible for printing the user interaction messages to the Console
    /// </summary>
    public class ConsolePrinter
    {
        private static string printValue;

        public ConsolePrinter Value(string value)
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
