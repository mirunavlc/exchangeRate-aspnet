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
            var printMode = ConfigurationManager.AppSettings["PrintMode"];
            IPrinter printer = PrinterFactory.Instance.Create(printMode);
       
        }
    }
}
