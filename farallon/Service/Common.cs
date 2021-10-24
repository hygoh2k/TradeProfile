using Farallon.Datamodel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Farallon.Service
{
    public interface IService
    {
        bool Run();
    }

    public interface ITransactionLoaderService
    {
        StockTransaction[] LoadTransaction();
    }


    public interface ITickerService
    {
        StockTicker[] LoadTicker();
    }




}
