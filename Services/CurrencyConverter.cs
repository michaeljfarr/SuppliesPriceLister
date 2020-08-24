﻿using buildxact_supplies.Model;
using Microsoft.Extensions.Options;

namespace buildxact_supplies.Services
{
    class CurrencyConverter
    {
        private readonly IOptionsMonitor<PriceListerOptions> _options;

        public CurrencyConverter(IOptionsMonitor<PriceListerOptions> options)
        {
            _options = options;
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
            return usd * 0.7m;//todo:fix configure line: _options.CurrentValue.audUsdExchangeRate;
        }
    }
}