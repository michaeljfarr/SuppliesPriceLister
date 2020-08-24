using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using buildxact_supplies.Model;

namespace buildxact_supplies.Interfaces
{
    public interface ISupplyPriceItemProvider
    {
        public Currency Currency { get; }
        /// <summary>
        /// This should parse the file from the provided file and return the list of items as requested.
        /// </summary>
        /// <param name="fileStream">FileStream</param>
        /// <returns>All of the supply price items as an async enumerable</returns>
        Task<IOrderedEnumerable<SupplyPriceItem>> GetItemsFromFile(Stream fileStream);
    }
}
