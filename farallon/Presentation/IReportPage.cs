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
    public interface IReportPage
    {
        string TabName { get; }
        void Load();

    }

    public abstract class ReportPage : TabPage, IReportPage
    {
        public abstract string TabName { get; }

        public abstract void Load();
        
    }


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
