using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using buildxact_supplies.Interfaces;

namespace buildxact_supplies.Services
{
    class SuppliesPriceListWriter
    {
        private readonly CurrencyConverter _currencyConverter;

        public SuppliesPriceListWriter(CurrencyConverter currencyConverter)
        {
            _currencyConverter = currencyConverter;
        }

        public async Task WriteSuppliesList(ISupplyPriceItemProvider itemProvider, TextWriter streamToWriteTo, Stream streamToReadFrom)
        {
            var items = await itemProvider.GetItemsFromFile(streamToReadFrom);
            foreach (var item in items)
            {
                //example output 7f3c48c4-f8b6-453f-b2fa-83ec31dfa85c, Bobcat to Dig LM of Strip Footing, $800.00
                //All prices must be shown to the nearest cent in AUD based on the exchange rate.
                var price = item.Price;
                if (itemProvider.Currency == Currency.USD)
                {
                    price = _currencyConverter.UsdToAud(price);
                }
                //todo: check requirements for culture output on currency here.
                streamToWriteTo.WriteLine($"{item.Id}, {item.ItemName}, {price:C2}");
            }
        }
    }
}
