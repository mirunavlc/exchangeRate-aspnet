using ExchangeRate.Printers.Factory;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Forms;

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

           Printer.InitializeFactories().ExecuteCreation(printMode, 12.35).Print();

           
        }
    }
}
