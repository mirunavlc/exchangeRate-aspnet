using ExchangeRate.Printers.Factory;
using System;
using System.Configuration;

namespace ExchangeRate
{
    class Program
    {
        static void Main(string[] args)
        {
            var printModeStr = ConfigurationManager.AppSettings["PrintMode"];
            PrintModes printMode;
            bool success = Enum.TryParse(printModeStr, out printMode);

            if(!success)
            {
                //Log smth
                return;
            }

           Printer.InitializeFactories().ExecuteCreation(printMode).Print(12.35.ToString());

            var link2 = ConfigurationManager.AppSettings["JSONSource2"];

            //
            //HTtpConnector.Get("link1") -> get property value
            // la fel pt linkul 2
            //comparatie, printer.print(val)

            //if (printMode == PrinterFactory.GetEnumDescription(PrinterFactory.PrintModes.Console))
            //{
            //    Console.WriteLine("Hello");
            //}
            //else
            //{
            //    MessageBox.Show("Hello");
            //}
        }
    }
}
