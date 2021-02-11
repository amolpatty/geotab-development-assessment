namespace JokeGenerator.Services
{
    public interface IConsolePrinterService
    {
        void PrintNewline();

        void PrintValue(string value);
        void PrettyPrintResults(string[] results);
    }
}