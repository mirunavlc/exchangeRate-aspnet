using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ExchangeRate.Printers.Factory
{
    public enum PrintModes
    {
        [Description("Console")]
        Console,

        [Description("MessageBox")]
        MessageBox
    }

    public abstract class PrinterFactory
    {
        public abstract IPrinter Create(double valueToPrint);
    }

    public class ConsoleFactory: PrinterFactory
    {
        public override IPrinter Create(double valueToPrint) => new ConsolePrinter(valueToPrint);
    }

    public class MessageBoxFactory : PrinterFactory
    {
        public override IPrinter Create(double valueToPrint) => new MessageBoxPrinter(valueToPrint);
    }
}
