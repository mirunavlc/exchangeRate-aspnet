using System.ComponentModel;

namespace ExchangeRate.Printers.Factory
{
    public enum PrintModes
    {
        [Description("Console")]
        Console,

        [Description("MessageBox")]
        MessageBox
    }

    public interface IPrinterFactory
    {
        IPrinter Create();
    }

    public class ConsoleFactory: IPrinterFactory
    {
        public IPrinter Create() => new ConsolePrinter();
    }

    public class MessageBoxFactory : IPrinterFactory
    {
        public IPrinter Create() => new MessageBoxPrinter();
    }
}
