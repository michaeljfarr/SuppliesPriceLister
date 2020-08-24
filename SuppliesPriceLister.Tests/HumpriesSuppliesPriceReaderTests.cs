using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using buildxact_supplies.Services.Humphries;
using FluentAssertions;
using Xunit;

namespace SuppliesPriceLister.Tests
{
    public class HumpriesSuppliesPriceReaderTests
    {
        [Fact]
        public async Task ReadExample()
        {
            //given some example data
            var example1 = @"identifier,desc,unit,costAUD
586e0bd4-a84c-4c39-a696-1fafdf85e5bb,Suspended Slab Formwork per m2,m2,23.59";
            byte[] byteArray = Encoding.UTF8.GetBytes(example1);
            var stringReader = new MemoryStream(byteArray);
            var reader = new HumpriesCsvReader();

            //when the data is processed
            var csvItemEnumerable = reader.ReadFileStream(stringReader);
            var csvItems = await csvItemEnumerable.ToListAsync();
            csvItems.Should().ContainSingle();

            //then values match input
            var onlyItem = csvItems[0];
            onlyItem.Identifier.Should().Be("586e0bd4-a84c-4c39-a696-1fafdf85e5bb");
            onlyItem.CostAUD.Should().Be(23.59m);
            onlyItem.Unit.Should().Be("m2");
        }

        [Fact]
        public async Task GetPricesFromExampleCsv()
        {
            //given some example data
            var example1 = @"identifier,desc,unit,costAUD
586e0bd4-a84c-4c39-a696-1fafdf85e5bb,Suspended Slab Formwork per m2,m2,23.59";

            byte[] byteArray = Encoding.UTF8.GetBytes(example1);
            var stringReader = new MemoryStream(byteArray);
            var reader = new HumpriesCsvReader();
            var itemProvider = new HumpriesSupplyPriceItemProvider(reader);
            //when the data is processed
            var priceListing = await itemProvider.GetItemsFromFile(stringReader);
            priceListing.Should().ContainSingle();
            var priceItem = priceListing.First();


            priceItem.Id.Should().Be("586e0bd4-a84c-4c39-a696-1fafdf85e5bb");
            priceItem.Price.Should().Be(23.59m);
        }
    }
}
