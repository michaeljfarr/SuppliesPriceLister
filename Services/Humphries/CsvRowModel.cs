using System;
using System.Collections.Generic;
using System.Text;

namespace buildxact_supplies.Services.Humphries
{
    class CsvRowModel
    {
        public string Identifier { get; set; }
        public string Desc{ get; set; }
        public string Unit{ get; set; }
        public decimal CostAUD { get; set; }
    }
}
