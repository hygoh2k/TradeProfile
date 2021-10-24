using Farallon.Service;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Farallon.Datamodel;

namespace Farallon.Service.Tests
{
    [TestClass()]
    public class ProfitLossServiceTestClass
    {
        [TestMethod("ProfitLossServiceOneTickerTwoTransactionTest")]
        public void ProfitLossServiceOneTickerTwoTransactionTest()
        {
            Mock<ITickerService> tickerService = new Mock<ITickerService>();
            tickerService.Setup(
                x => x.LoadTicker())
                .Returns(new StockTicker[] {
                    new StockTicker()
                    { 
                        Ticker = "AAA",
                        Price = 10.00m,
                        PrevClosePrice = 12.00m,
                        DateTime = DateTime.Parse("10/24/2021 00:00")
                    }
                
                });

            Mock<ITransactionLoaderService> transactionService = new Mock<ITransactionLoaderService>();
            transactionService.Setup(
                x => x.LoadTransaction())
                .Returns(new StockTransaction[] { 
                    new StockTransaction()
                    {
                        Ticker = "AAA",
                        Price = 8.0m,
                        Action = "BUY",
                        Quantity = 10,
                        DateTime = DateTime.Parse("01/01/2021 00:00")
                    },

                    new StockTransaction()
                    {
                        Ticker = "AAA",
                        Price = 9.0m,
                        Action = "BUY",
                        Quantity = 20,
                        DateTime = DateTime.Parse("02/01/2021 00:00")
                    }

                });

            ProfitLossService target = new ProfitLossService(tickerService.Object, transactionService.Object);
            target.Run();
            
            var resultList = target.ReportItems;
            Assert.IsTrue(resultList.Length == 1, "Count should be 1");
            Assert.AreEqual(resultList[0].Quantity, 30, "Quantity != 20+10");
            Assert.AreEqual(resultList[0].Cost, 260.0m, "Cost != 20*9.0m + 10*8.0m");
            Assert.AreEqual(resultList[0].MarketValue, 300.0m, "MarketValue != 10*30");
            Assert.AreEqual(resultList[0].PrevClose, 12.0m, "PrevClose != 12");
            Assert.AreEqual(resultList[0].InceptionPL, 40.0m, "InceptionPL != 300 - 260");

        }


    }
}


