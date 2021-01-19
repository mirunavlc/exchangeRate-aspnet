using System.Windows.Forms;

namespace ExchangeRate.Printers.Factory
{
    public class MessageBoxPrinter : IPrinter
    {
        public void Print(string text) => MessageBox.Show(text);
    }
}