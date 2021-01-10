using System.Collections.Generic;

namespace Bricks.ConsoleIO
{
    public class TestConsoleIO : IConsoleIO
    {
        public IList<string> TestOutput { get; set; }

        public int CurrentOutputLineIndex { get; set; }

        public int CurrentInputLineIndex { get; set; }

        public IList<string> TestInput { get; set; }

        public TestConsoleIO()
        {
            this.TestOutput = new List<string>();
        }

        public TestConsoleIO(IList<string> input)
        {
            this.TestInput = input;
        }

        public string ReadLine()
        {
            throw new System.NotImplementedException();
        }

        public void Write(string value)
        {
            throw new System.NotImplementedException();
        }

        public void WriteLine(string value)
        {
            this.TestOutput.Add(value);
        }
    }
}