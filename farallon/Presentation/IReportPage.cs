using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Farallon.UI
{
    /// <summary>
    /// an interface of the Report.
    /// All types ReportPage classes should implement this interface.
    /// </summary>
    public interface IReportPage
    {
        string TabName { get; }
        void Load();

    }

    /// <summary>
    /// a base class of ReportPage
    /// all pages to be injected into the Application should be derived from this base class.
    /// </summary>
    public abstract class ReportPage : TabPage, IReportPage
    {
        public abstract string TabName { get; }

        public abstract void Load();

        public abstract Task LoadAsync();
    }


    /// <summary>
    /// a collection class of ReportPage
    /// </summary>
    public class ReportPageCollection
    {
        public IReadOnlyDictionary<string, ReportPage> Items { get; private set; }

        public ReportPageCollection(ReportPage[] reportPage)
        {
            Dictionary<string, ReportPage> pageCollection = new Dictionary<string, ReportPage>();
            foreach (var page in reportPage)
            {
                if (pageCollection.ContainsKey(page.TabName))
                {
                    throw new DuplicateNameException(string.Format("Duplicated key {0} detected in pages", page.TabName));
                }

                pageCollection[page.TabName] = page;
            }

            Items = new ReadOnlyDictionary<string, ReportPage>(pageCollection);

        }
    }
}
