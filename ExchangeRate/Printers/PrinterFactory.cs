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

    public interface PrinterFactory
    {
        IPrinter Create();
    }

    public class ConsoleFactory: PrinterFactory
    {
        public IPrinter Create() => new ConsolePrinter();
    }

    public class MessageBoxFactory : PrinterFactory
    {
        public IPrinter Create() => new MessageBoxPrinter();
    }
}
