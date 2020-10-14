using Microsoft.Extensions.Logging;
using stock.Buisness.APIRead;
using stockDataEF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Bot
{
    class YobitBot : IBot
    {
        private readonly IStockRead stockRead;
        private readonly ILogger<YobitBot> logger;

        public YobitBot(IStockRead stockRead, ILogger<YobitBot> logger)
        {
            this.stockRead = stockRead;
            this.logger = logger;
        }

        public void Analize()
        {
            throw new NotImplementedException();
        }

        public void Buy(Product product, double quantity)
        {
            
            logger.LogInformation($"Bought{product.Name} {quantity}");
        }

        public void Sell(Product product, double quantity)
        {
            logger.LogInformation($"Sold{product.Name} {quantity}");
        }
    }
}
