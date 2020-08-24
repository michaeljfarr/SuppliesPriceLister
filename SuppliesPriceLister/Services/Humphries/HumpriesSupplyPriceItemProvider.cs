using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using buildxact_supplies.Interfaces;
using buildxact_supplies.Model;

namespace buildxact_supplies.Services.Humphries
{
    class HumpriesSupplyPriceItemProvider : ISupplyPriceItemProvider
    {
        private readonly HumpriesCsvReader _reader;

        public HumpriesSupplyPriceItemProvider(HumpriesCsvReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        ///  All price sources are in AUD
        /// </summary>
        public Currency Currency => Currency.AUD;

        /// <summary>
        /// Read the data stream as a HumpriesProductStream and convert it to 
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns></returns>
        /// <remarks>
        /// This implementation is simple but doesn't extract the true value out of the
        /// async enumerable because it is loading the entire file into memory.
        /// Depending on memory and other issues we could go in different directions on the
        /// implementation technique.
        /// </remarks>
        public async Task<IOrderedEnumerable<SupplyPriceItem>> GetItemsFromFile(Stream fileStream)
        {
            var productInfoEnumerable = _reader.ReadFileStream(fileStream);
            //this method of handling async streams isn't particularly efficient, so depending on file sizes we
            //could optimize this etc.
            var productInfos = await productInfoEnumerable.ToListAsync();
            return productInfos.Select(a=>new SupplyPriceItem()
            {
                Id = a.Identifier,
                ItemName = a.Desc,
                Price = a.CostAUD
            }).OrderBy(a => a.Price);
        }
    }
}
