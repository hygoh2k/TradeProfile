using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farallon.Datamodel
{
    public interface ITransaction
    {
        /// <summary>
        /// Transaction ID
        /// </summary>
        string Id { get; set; }
        
        /// <summary>
        /// Ticker Symbol
        /// </summary>
        string Ticker { get; set; }
        

        /// <summary>
        /// Action: BUY/SELL
        /// </summary>
        string Action { get; set; }

        /// <summary>
        ///transaction QTY
        /// </summary>

        int Quantity { get; set; }

        /// <summary>
        /// transaction price
        /// </summary>

        decimal Price { get; set; }

        /// <summary>
        /// transaction date
        /// </summary>
        DateTime DateTime { get; set; }

    }


    public interface ITicker
    {
        /// <summary>
        /// ticker symbol
        /// </summary>
        string Ticker { get; set; }

        /// <summary>
        /// ticker price
        /// </summary>
        decimal Price { get; set; }

        /// <summary>
        /// time
        /// </summary>
        DateTime DateTime { get; set; }

    }


    public class StockTransaction : ITransaction
    {
        public string Id { get; set; }
        public string Ticker { get; set; }
        public string Action { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
    }



    public class StockTicker : ITicker
    {
        public string Ticker { get; set; }
        public decimal Price { get; set; }

        public decimal PrevClosePrice { get; set; }
        public DateTime DateTime { get; set; }
    }
}
