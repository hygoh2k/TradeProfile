using Farallon.Service;
using Farallon.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportTabControl
{
    public class ProfitLossTabPage : ReportPage
    {
        ProfitLossService _service = null;
        public ProfitLossTabPage(ITransactionLoaderService transactionSercice, ITickerService ticketService)
        {
            this.Text = TabName;
            _service = new ProfitLossService(ticketService, transactionSercice);
        }

        public override string TabName {
            get {
                return "Profit & Loss";
            } 
        }

        public override void Load()
        {

            _service.Run();
            var items = _service.ReportItems;

            var dt = new DataTable();
            string[] headers = new string[] { 
                "Ticker", "Cost", "Quantity", "Price", "MarketValue", "PrevClose", "InceptionPL", "AsOfDate"
            };

            headers.ToList().ForEach(h=>dt.Columns.Add(h));



            foreach (var item in items)
            {
                var dr = dt.NewRow().ItemArray = new object[] {
                    item.Ticker, item.Cost, item.Quantity, item.Price, item.MarketValue,
                    item.PrevClose, item.InceptionPL, item.AsOfDate
                };
                dt.Rows.Add(dr);
            }

            ProfitLossControl lc = new ProfitLossControl(dt);
            this.Controls.Clear();
            this.Controls.Add(lc);
            lc.Dock = DockStyle.Fill;
        }
    }
}
