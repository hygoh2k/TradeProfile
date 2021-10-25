using Farallon.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    /// <summary>
    /// a console apps as a playground for API/service test run
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ITransactionLoaderService s = new StockTransactionXmlService();
            var items = s.LoadTransaction();

            //ITickerService t = new StockTickerXmlService();
            //var tickers = t.LoadTicker();

            ITickerService t = new StockTickerAvapiService() { TransactionService = s };
            var tickers = t.LoadTicker();

            ProfitLossService pl = new ProfitLossService(t,s);
            pl.Run();

            var reportItems = pl.ReportItems;

        }
    }
}
