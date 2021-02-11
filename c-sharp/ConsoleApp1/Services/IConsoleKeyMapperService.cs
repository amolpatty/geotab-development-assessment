using System;

namespace JokeGenerator.Services
{
    public interface IConsoleKeyMapperService
    {
        char GetEnteredKey(ConsoleKeyInfo consoleKeyInfo);
    }
}
