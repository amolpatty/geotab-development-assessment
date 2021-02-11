namespace JokeGenerator.Services
{
    public interface IConsolePrinterService
    {
        void PrintNewline();

        void PrintMessage(string value);
        void PrintErrorMessage(string value);
        void PrettyPrintResults(string[] results);
        void PrettyPrintCategories(string[] results);
    }
}