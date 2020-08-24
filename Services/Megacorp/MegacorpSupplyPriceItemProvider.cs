using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using buildxact_supplies.Interfaces;
using buildxact_supplies.Model;

namespace buildxact_supplies.Services.Humphries
{
    class MegacorpSupplyPriceItemProvider : ISupplyPriceItemProvider
    {
        private readonly MegacorpJsonReader _reader;

        public MegacorpSupplyPriceItemProvider(MegacorpJsonReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        ///  All price sources are in USD
        /// </summary>
        public Currency Currency => Currency.USD;

        public async Task<IOrderedEnumerable<SupplyPriceItem>> GetItemsFromFile(Stream fileStream)
        {
            var productInfoEnumerable = await _reader.ReadFileStream(fileStream);
            //this method of handling async streams isn't particularly efficient, so depending on file sizes we
            //could optimize this etc.
            var productInfos = productInfoEnumerable.Partners.SelectMany(a => a.Supplies).Select(a =>
                new SupplyPriceItem
                {
                    Id = a.Id.ToString(),
                    Price = a.PriceInCents / 100m,
                    ItemName = a.Description
                }).OrderBy(a => a.Price);
            return productInfos;
        }
    }
}
