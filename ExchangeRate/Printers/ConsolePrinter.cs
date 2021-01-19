using System;

namespace ExchangeRate.Printers.Factory
{
    public class ConsolePrinter : IPrinter
    {
        private readonly double _value;

        public ConsolePrinter(double value)
        {
            _value = value;
        }

        public void Print()
        {
            Console.WriteLine(_value);
        }
    }
}
