﻿using System;
using System.Collections.Generic;

namespace ExchangeRate.Printers.Factory
{
    public class Printer
    {
        private readonly Dictionary<PrintModes, PrinterFactory> _factories;

        private Printer()
        {
            _factories = new Dictionary<PrintModes, PrinterFactory>();

            foreach (PrintModes printMode in Enum.GetValues(typeof(PrintModes)))
            {
                var type = "ExchangeRate.Printers.Factory." + Enum.GetName(typeof(PrintModes), printMode) + "Factory";
                var factory = (PrinterFactory)Activator.CreateInstance
                              (Type.GetType(type));
                _factories.Add(printMode, factory);
            }
        }
        public static Printer InitializeFactories() => new Printer();
        public IPrinter ExecuteCreation(PrintModes printMode) => _factories[printMode].Create();
    }
}