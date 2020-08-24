using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace buildxact_supplies.Services.Humphries
{
    class HumpriesCsvReader
    {
        public async IAsyncEnumerable<CsvRowModel> ReadFileStream(Stream fileStream)
        {
            using (var streamReader = new StreamReader(fileStream))
            {
                //read and discard the header
                var line = await streamReader.ReadLineAsync();
                //todo: should we validate header or check encoding etc?
                line = await streamReader.ReadLineAsync();

                //todo: might they want blank lines before EOF?  
                while (!string.IsNullOrWhiteSpace(line))
                {
                    var lineParts = line.Split(",", StringSplitOptions.None);
                    yield return new CsvRowModel
                    {
                        Identifier = lineParts[0],
                        Desc = lineParts[1],
                        Unit = lineParts[2],
                        //todo: I'd have some sort of per line error handling here so we can 
                        //have detailed handling of error messages in production.
                        CostAUD = decimal.Parse(lineParts[3]),
                    };
                    line = await streamReader.ReadLineAsync();
                }
            }
        }
        
    }
}
