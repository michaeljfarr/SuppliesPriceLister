using System;
using System.Collections.Generic;
using System.Text;
using buildxact_supplies.Services.Humphries;
using Microsoft.Extensions.DependencyInjection;

namespace buildxact_supplies.Services
{
    static class ServiceCollectionFactory
    {
        
        public static ServiceCollection CreateServiceCollection()
        {
            var serviceCollection = new ServiceCollection();
            //I chose to do explicit inclusion here because it helps with clarity
            //this might get annoying on a big project
            serviceCollection.AddSingleton<HumpriesCsvReader>();
            serviceCollection.AddSingleton<HumpriesSupplyPriceItemProvider>();
            serviceCollection.AddSingleton<MegacorpJsonReader>();
            serviceCollection.AddSingleton<MegacorpSupplyPriceItemProvider>();
            return serviceCollection;
        }
    }
}
