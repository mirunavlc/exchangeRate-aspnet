using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ExchangeRate
{
    class Program
    {
        internal enum PrintModes
        {
            [Description("Console")]
            Console,

            [Description("MessageBox")]
            MessageBox
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        static void Main(string[] args)
        {
            var printMode = ConfigurationManager.AppSettings["PrintMode"];
            if(printMode == GetEnumDescription(PrintModes.Console))
            {
                Console.WriteLine("Hello");
            }
            else
            {
                MessageBox.Show("Hello");
            }
        }
    }
}
