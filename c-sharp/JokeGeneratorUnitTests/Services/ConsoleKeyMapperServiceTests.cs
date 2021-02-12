using JokeGenerator.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JokeGeneratorUnitTests
{
    [TestClass]
    public class ConsoleKeyMapperServiceTests
    {
        [TestMethod]
        public void TestGetEnteredKey_R()
        {
            // Arrange            
            ConsoleKeyInfo cki = new ConsoleKeyInfo('r', ConsoleKey.R, false, false, false);

            // Act
            IConsoleKeyMapperService ckms = new ConsoleKeyMapperService();
            char key = ckms.GetEnteredKey(cki);

            // Assert
            Assert.AreEqual(key, 'r');
        }
    }
}
