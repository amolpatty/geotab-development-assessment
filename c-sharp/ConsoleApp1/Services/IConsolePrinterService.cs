namespace JokeGenerator.Services
{
    public interface IConsolePrinterService
    {
        string ToString();
        IConsolePrinterService Value(string value);
    }
}