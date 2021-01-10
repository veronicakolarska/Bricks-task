namespace Bricks.ConsoleIO
{
    /// <summary>
    /// used to abstract working with the console
    /// so methods can be more easily testable
    /// </summary>
    public interface IConsoleIO
    {
        string ReadLine();
        void Write(string value);
        void WriteLine(string value = "");
    }

}