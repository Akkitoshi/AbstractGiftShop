using System;
using System.Collections.Generic;

namespace AbstractGiftShopServiceDAL.ViewModel
{
    public class StocksLoadViewModel
    {
        public string StockName { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<Tuple<string, int>> Materialss { get; set; }
    }
}
