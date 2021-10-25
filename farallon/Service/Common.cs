using Farallon.Datamodel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Farallon.Service
{
    /// <summary>
    /// interface of a generic service
    /// </summary>
    public interface IService
    {
        bool Run();
    }

    /// <summary>
    /// interface of a transaction loader service
    /// </summary>
    public interface ITransactionLoaderService
    {
        StockTransaction[] LoadTransaction();
    }


    /// <summary>
    /// interface of a ticker loader service
    /// </summary>
    public interface ITickerService
    {
        StockTicker[] LoadTicker();
    }




}
