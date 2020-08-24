using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using buildxact_supplies.Services.Megacorp;

namespace buildxact_supplies.Services.Humphries
{
    class MegacorpJsonReader
    {
        public async Task<MegacorpDataModel> ReadFileStream(Stream fileStream)
        {
            return await System.Text.Json.JsonSerializer.DeserializeAsync<MegacorpDataModel>(fileStream);
        }
    }
}