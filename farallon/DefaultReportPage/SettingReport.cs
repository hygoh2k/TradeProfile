using DefaultReportPage;
using Farallon.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportTabPage
{
    /// <summary>
    /// a page to display application settings
    /// </summary>
    public class AppSettingPage : ReportPage
    {
        public AppSettingPage()
        {
            this.Text = TabName;
        }
        public override string TabName
        {
            get { return "Default Page"; }
        }

        public override void Load()
        {
            //throw new NotImplementedException();
            this.Controls.Clear();
            this.Controls.Add(new SettingControl() { Dock = DockStyle.Fill });

            

        }

        public override Task LoadAsync()
        {
            return  Task.Run(() => {
                Load();
            });
        }
    }
}
