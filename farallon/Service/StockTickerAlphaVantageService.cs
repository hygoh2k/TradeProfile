using Farallon.Datamodel;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using Avapi;
using Avapi.AvapiTIME_SERIES_DAILY;
using Avapi.AvapiTIME_SERIES_INTRADAY;

namespace Farallon.Service
{
    /// <summary>
    /// a ticker service based on Avapi. the service requires ITransactionLoaderService to be injected
    /// </summary>
    public class StockTickerAvapiService : ITickerService //IService, ITickerService
    {
        string _key = @"WIN7PU26SG9B072T";
        //string _apiUrl = @"https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol={0}&interval=5min&apikey=WIN7PU26SG9B072T";



        public ITransactionLoaderService TransactionService { get; set; }


        //public bool Run()
        //{
        //    LoadTicker();
        //    return true;
        //}
        public StockTicker[] LoadTicker()
        {
            StockTransaction[] transList =
                TransactionService == null ? new StockTransaction[] { }
                : TransactionService.LoadTransaction();

            var symbols = transList.Select(t => t.Ticker).Distinct();
            //IDictionary<string,string> symbolUrlMap = transList.Select(t => t.Ticker).Distinct()
            //    .Select(s => new KeyValuePair<string, string>(s, string.Format(apiUrl, s)))
            //    .ToDictionary(x => x.Key, x => x.Value);

            List<StockTicker> tickers = new List<StockTicker>();

            IAvapiConnection connection = AvapiConnection.Instance;
            connection.Connect(_key);
            foreach (var symbol in symbols)
            {

                //get latest data
                Int_TIME_SERIES_INTRADAY intraday = connection.GetQueryObject_TIME_SERIES_INTRADAY();

                var todateResult = intraday.Query( symbol,
                     Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_interval.n_5min,
                     Const_TIME_SERIES_INTRADAY.TIME_SERIES_INTRADAY_outputsize.compact
                     );
                var todateData = todateResult.Data.TimeSeries.OrderByDescending(t => t.DateTime).FirstOrDefault();

                //get previous day data
                var dailyApi = connection.GetQueryObject_TIME_SERIES_DAILY();
                var dailyResult = dailyApi.Query(symbol, Const_TIME_SERIES_DAILY.TIME_SERIES_DAILY_outputsize.compact);
                var dataByDay = dailyResult.Data.TimeSeries.OrderByDescending(t => t.DateTime);
                //var todateData = dataByDate.FirstOrDefault();
                var prevDayData = dataByDay.Skip(1).FirstOrDefault();

                tickers.Add(
                    new StockTicker
                    {
                        Ticker = symbol,
                        DateTime = DateTime.Parse(todateData.DateTime),
                        Price = Decimal.Parse(todateData.close),
                        PrevClosePrice = Decimal.Parse(prevDayData.close),
                    });
            }
            return tickers.ToArray();
        }
    }




}
