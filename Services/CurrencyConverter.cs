using System;
using buildxact_supplies.Model;
using Microsoft.Extensions.Options;

namespace buildxact_supplies.Services
{
    class CurrencyConverter
    {
        private readonly IOptionsMonitor<PriceListerOptions> _options;

        public CurrencyConverter(IOptionsMonitor<PriceListerOptions> options)
        {
            _options = options;
            if (_options.CurrentValue.audUsdExchangeRate <= 0)
            {
                throw new ArgumentException($"Config Error: audUsdExchangeRate was {_options.CurrentValue.audUsdExchangeRate}" );
            }
        }

        /// <summary>
        /// I would typically ask a few more questions about this, but I suppose
        /// this is an acceptable calculation for a Demo.
        /// </summary>
        public decimal AudToUsd(decimal aud)
        {
            return aud / _options.CurrentValue.audUsdExchangeRate;
        }

        public decimal UsdToAud(decimal usd)
        {
            return usd * _options.CurrentValue.audUsdExchangeRate;
        }
    }
}