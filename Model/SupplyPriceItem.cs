using System;
using System.Collections.Generic;
using System.Text;

namespace buildxact_supplies.Model
{
    /// <summary>
    /// The information printed should be the ID, item name and price.
    /// </summary>
    public class SupplyPriceItem
    {
        /// <summary>
        /// The Id of the component as expressed by the origin service.  
        /// </summary>
        /// <remarks>
        /// There is a chance that this might not be unique across services, so we should consider namespacing the id somehow.
        /// </remarks>
        public string Id { get; set; }
        public string ItemName { get; set; }
        public Decimal Price { get; set; }
    }
}
