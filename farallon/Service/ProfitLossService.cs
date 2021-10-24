using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farallon.Service
{
    public class ProfitLossReportItem
    {
        public string Ticker { get; set; }
        public DateTime AsOfDate { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal MarketValue { get; set; }

        public decimal PrevClose { get; set; }

        public decimal DailyPL { get; set; }

        public decimal InceptionPL { get; set; }

        
    }
    public class ProfitLossService : IService
    {
        private ITickerService _tickerService;
        private ITransactionLoaderService _transactionService;

        public ProfitLossReportItem[] ReportItems
        {
            get
            {
                return _reportItemList.ToArray();
            }
        }

        private List<ProfitLossReportItem> _reportItemList;
        public ProfitLossService(ITickerService tickerService, ITransactionLoaderService transactionService)
        {
            _tickerService = tickerService;
            _transactionService = transactionService;
            _reportItemList = new List<ProfitLossReportItem>();
        }

        

        public bool Run()
        {
            var tickerList = _tickerService.LoadTicker();
            var transactionList = _transactionService.LoadTransaction();

            _reportItemList.Clear();

            //analyze transaction
            var buyList = transactionList
                .Where(t => t.Action.Equals("BUY"))
                .GroupBy(t => t.Ticker)
                .ToDictionary(x => x.Key, x => x.ToList());
            //var sellList = transactionList.Where(t => t.Action.Equals("SELL"));

            var tickerMap = tickerList
                .GroupBy(x => x.Ticker)
                .ToDictionary(x => x.Key, x => x.FirstOrDefault());


            

            foreach (var stockName in buyList.Keys)
            {
                

                var trans = buyList[stockName];
                var totalCost = trans.Sum(t => t.Price * t.Quantity);
                var totalQty = trans.Sum(t => t.Quantity);

                var marketUnitPrice = tickerMap.ContainsKey(stockName) ? tickerMap[stockName].Price  : -1;
                var marketPrice = tickerMap.ContainsKey(stockName) ? marketUnitPrice*totalQty : -1;
                var prevClose = tickerMap.ContainsKey(stockName) ? tickerMap[stockName].PrevClosePrice: -1;
                var dailyPL = tickerMap.ContainsKey(stockName) ? (marketPrice - prevClose) * totalQty : -1;
                var inceptionPL = tickerMap.ContainsKey(stockName) ? marketPrice - totalCost : -1;
                var asOfDate = tickerMap.ContainsKey(stockName) ? tickerMap[stockName].DateTime : DateTime.MinValue;

                ProfitLossReportItem reportItem = new ProfitLossReportItem()
                {
                    Ticker = stockName,
                    Quantity = totalQty,
                    AsOfDate = asOfDate,
                    Cost = totalCost,
                    MarketValue = marketPrice,
                    DailyPL = dailyPL,
                    InceptionPL = inceptionPL,
                    PrevClose = prevClose,
                    Price = marketUnitPrice
                };

                _reportItemList.Add(reportItem);



            }

            




            

            


            return true;
        }
    }
}
