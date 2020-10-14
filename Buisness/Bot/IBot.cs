using stock.Buisness.APIRead;
using stockDataEF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Bot
{
    public interface IBot
    {
        public void Buy(Product product, double quantity);
        public void Sell(Product product, double quantity);
        public void Analize();
        
    }
}
