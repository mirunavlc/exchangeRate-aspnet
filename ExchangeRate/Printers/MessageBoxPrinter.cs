using System.Windows.Forms;

namespace ExchangeRate.Printers.Factory
{
    public class MessageBoxPrinter : IPrinter
    {
        private readonly double _value;

        public MessageBoxPrinter(double value)
        {
            _value = value;
        }

        public void Print()
        {
            MessageBox.Show(_value.ToString());
        }
    }
}