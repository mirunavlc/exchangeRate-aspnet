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

    public sealed class PrinterFactory
    {
        private static readonly Lazy<PrinterFactory> lazy = new Lazy<PrinterFactory>(() => new PrinterFactory());

        public static PrinterFactory Instance { get { return lazy.Value; } }

        private PrinterFactory() { }

        private string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public IPrinter Create(string printMode)
        {
            IPrinter printer = null;

            if (printMode == GetEnumDescription(PrintModes.Console))
            {
                printer = new ConsolePrinter();
            }

            if (printMode == GetEnumDescription(PrintModes.MessageBox))
            {
                printer = new MessageBoxPrinter();
            }

            return printer;
        }
    }
}
