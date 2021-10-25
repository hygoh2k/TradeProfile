using Farallon.Datamodel;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Farallon.Service
{
    /// <summary>
    /// the Stock Transaction service that loads data from XML
    /// </summary>
    public class StockTransactionXmlService : ITransactionLoaderService //IService, ITransactionLoaderService
    {
        string xmlFile = AppDomain.CurrentDomain.BaseDirectory + "../../../working_dir/transaction.xml"; //hardcode for now


        /// <summary>
        /// load transactions
        /// </summary>
        /// <returns></returns>
        public StockTransaction[] LoadTransaction()
        {
            var transList = new List<StockTransaction>();

            XmlDocument xml = new XmlDocument();
            xml.Load(xmlFile);
            var transNodes = xml.SelectNodes(@"/transactions/transaction");
            foreach(XmlNode node in transNodes)
            {
                var ticker = node.SelectSingleNode(@"ticker").InnerText.Trim();
                var tradedate = node.SelectSingleNode(@"tradedate").InnerText.Trim();
                var action = node.SelectSingleNode(@"action").InnerText.Trim();
                var quantity = node.SelectSingleNode(@"quantity").InnerText.Trim();
                var price = node.SelectSingleNode(@"price").InnerText.Trim();

                transList.Add(
                    new StockTransaction {
                       Ticker = ticker,
                       DateTime = DateTime.Parse(tradedate),
                       Action = action,
                       Quantity = int.Parse(quantity),
                       Price = Decimal.Parse(price)
                    }

                    );

            }

            return transList.ToArray();

        }

        //public bool Run()
        //{
        //    LoadTransaction();
        //    return true;
        //}
    }




}
