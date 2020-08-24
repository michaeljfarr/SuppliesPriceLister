
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using buildxact_supplies.Constants;
using buildxact_supplies.Interfaces;
using buildxact_supplies.Services;
using buildxact_supplies.Services.Humphries;
using Microsoft.Extensions.DependencyInjection;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("SuppliesPriceLister.Tests")]

namespace SuppliesPriceLister
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            if (args.Length != 2 || args[0] != FormatNames.Humpries && args[0] != FormatNames.Humpries)
            {
                Console.WriteLine("Usage: SPL <Format> <pathToFile>");
                Console.WriteLine($" Formats: {FormatNames.Humpries}, {FormatNames.Megacorp}");
                return -1;
            }

            if (!File.Exists(args[1]))
            {
                Console.WriteLine($"Error, could not find file {Path.GetFullPath(args[1])}");
                return -1;
            }
            var serviceProvider = ServiceCollectionFactory.CreateServiceCollection().BuildServiceProvider();
            ISupplyPriceItemProvider itemProvider = null;
            if (args[0] == FormatNames.Humpries)
            {
                itemProvider = serviceProvider.GetRequiredService<HumpriesSupplyPriceItemProvider>();
            }
            else
            {
                itemProvider = serviceProvider.GetRequiredService<MegacorpSupplyPriceItemProvider>();
            }

            using (var fileStream = File.OpenRead(args[1]))
            {
                var items = await itemProvider.GetItemsFromFile(fileStream);
                foreach (var item in items)
                {
                    //example output 7f3c48c4-f8b6-453f-b2fa-83ec31dfa85c, Bobcat to Dig LM of Strip Footing, $800.00
                    Console.WriteLine($"{item.Id}, {item.ItemName}, {item.Price}");
                }
            }

            return 0;

        }
    }
}
