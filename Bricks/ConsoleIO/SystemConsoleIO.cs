using System;

namespace Bricks.ConsoleIO
{
    /// <summary>
    /// Implementation using the default console
    /// </summary>
    public class SystemConsoleIO : IConsoleIO
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string value)
        {
            Console.Write(value);
        }

        public void WriteLine(string value = "")
        {
             Console.WriteLine(value);
        }
    }
}