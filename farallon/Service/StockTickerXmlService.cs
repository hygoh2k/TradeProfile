using Farallon.Datamodel;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Farallon.Service
{
    public class StockTickerXmlService : IService, ITickerService
    {
        string xmlFile = AppDomain.CurrentDomain.BaseDirectory + "../../../working_dir/ticker.xml";
        
        public bool Run()
        {
            LoadTicker();
            return true;
        }
        public StockTicker[] LoadTicker()
        {
            var transList = new List<StockTicker>();

            XmlDocument xml = new XmlDocument();
            xml.Load(xmlFile);
            var transNodes = xml.SelectNodes(@"/tickers/ticker");
            foreach (XmlNode node in transNodes)
            {
                var ticker = node.Attributes.GetNamedItem("name").InnerText;
                var tradedate = node.SelectSingleNode(@"datetime").InnerText;
                var price = node.SelectSingleNode(@"price").InnerText;
                var prevClosePrice = node.SelectSingleNode(@"prev_close").InnerText;

                transList.Add(
                    new StockTicker
                    {
                        Ticker = ticker,
                        DateTime = DateTime.Parse(tradedate),
                        Price = Decimal.Parse(price),
                        PrevClosePrice = Decimal.Parse(prevClosePrice),
                    }

                    );

            }

            return transList.ToArray();
        }
    }




}
