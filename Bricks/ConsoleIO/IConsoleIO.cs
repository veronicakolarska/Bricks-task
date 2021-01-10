namespace Bricks.ConsoleIO
{
    public interface IConsoleIO
    {
        string ReadLine();
        void Write(string value);
        void WriteLine(string value = "");
    }

}