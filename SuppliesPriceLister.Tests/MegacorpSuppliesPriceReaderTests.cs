using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using buildxact_supplies.Services.Humphries;
using FluentAssertions;
using Xunit;

namespace SuppliesPriceLister.Tests
{
    public class MegacorpSuppliesPriceReaderTests
    {
        [Fact]
        public async Task ReadExampleCsv()
        {
            //given some example data
            var example1 = @"{
  ""partners"": [
    {
      ""name"": ""Megacorp Southeast"",
      ""partnerType"": ""INTERNAL"",
      ""partnerAddress"": ""14 Park Crescent, Clayton"",
      ""supplies"": [
        {
          ""id"": 1,
          ""description"": ""100 x 200 x 20mpa Internal Beam"",
          ""uom"": ""lm"",
          ""priceInCents"": 4000,
          ""providerId"": ""907d853f-dbe7-45c0-8e59-9dff4044cf80"",
          ""materialType"": ""Steel""
        }]}]}";
            byte[] byteArray = Encoding.UTF8.GetBytes(example1);
            var stringReader = new MemoryStream(byteArray);
            var reader = new MegacorpJsonReader();

            //when the data is processed
            var partners = await reader.ReadFileStream(stringReader);
            partners.Partners.Should().ContainSingle();
            var southEast = partners.Partners.Single();
            southEast.Name.Should().Be("Megacorp Southeast");
            southEast.Supplies.Should().ContainSingle();
            var supply = southEast.Supplies.Single();
            supply.Id.Should().Be(1);
            supply.ProviderId.Should().Be("907d853f-dbe7-45c0-8e59-9dff4044cf80");
            supply.PriceInCents.Should().Be(4000);
        }

        [Fact]
        public async Task GetPricesFromExampleCsv()
        {
            //given some example data
            var example1 = @"{
  ""partners"": [
    {
      ""name"": ""Megacorp Southeast"",
      ""partnerType"": ""INTERNAL"",
      ""partnerAddress"": ""14 Park Crescent, Clayton"",
      ""supplies"": [
        {
          ""id"": 1,
          ""description"": ""100 x 200 x 20mpa Internal Beam"",
          ""uom"": ""lm"",
          ""priceInCents"": 4000,
          ""providerId"": ""907d853f-dbe7-45c0-8e59-9dff4044cf80"",
          ""materialType"": ""Steel""
        }]}]}";
            byte[] byteArray = Encoding.UTF8.GetBytes(example1);
            var stringReader = new MemoryStream(byteArray);
            var reader = new MegacorpJsonReader();
            var itemProvider = new MegacorpSupplyPriceItemProvider(reader);
            //when the data is processed
            var priceListing = await itemProvider.GetItemsFromFile(stringReader);
            priceListing.Should().ContainSingle();
            var priceItem = priceListing.First();
            priceItem.Id.Should().Be("1");
            priceItem.ItemName.Should().Be("100 x 200 x 20mpa Internal Beam");
            priceItem.Price.Should().Be(40m);
        }
    }
}