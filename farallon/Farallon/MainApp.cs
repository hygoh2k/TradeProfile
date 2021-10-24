using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Farallon.Service;
using Farallon.UI;
using ReportTabControl;
using System;
using System.Windows.Forms;
using ReportTabPage;
using System.Threading.Tasks;

namespace farallon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InitEnvironment()
        {
            
        }

        private async void Load_Click(object sender, EventArgs e)
        {
            //this.toolStripStatusLabel1.Text = "Loading...";
            //await Task.Delay(10000);
            //this.TabContainer.Controls.Add(new ProfitLossTabPage());
        }

        //private string WorkingDir = string.Empty;
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //DialogResult result = folderBrowserDialog1.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    WorkingDir = folderBrowserDialog1.SelectedPath;
            //}
        }

        private static Castle.Windsor.WindsorContainer _container;
        private async void Reload_Click(object sender, EventArgs e)
        {
            try
            {
                this.StatusBar.Text = "Loading...";
                SetupContainer();
                RegisterComponents();

                this.StatusBar.Text = "Loading Plugin...";
                var pageCollection = await RunResolveAsync();

                // _container.Resolve<ReportPageCollection>();

                this.StatusBar.Text = "Loading Reports...";
                this.TabContainer.TabPages.Clear();
                await Task.Run(() =>
                {
                    foreach (var report in pageCollection.Items)
                    {
                        report.Value.Load();

                    }
                });

                foreach (var report in pageCollection.Items)
                {
                    this.TabContainer.TabPages.Add(report.Value);
                }

                this.StatusBar.Text = "Ready";

            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    string.Format("Error encountered during loading, try reload. Error: {0}", ex.Message),
                    caption:"Error during loading",
                    buttons:MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Exclamation
                    );
            }
        }


        private async Task<ReportPageCollection> RunResolveAsync()
        {
            return await Task.Run( () => { 
                var reportPageCollection = _container.Resolve<ReportPageCollection>();
                return reportPageCollection;
            } );
        }



        /// <summary>
        /// setup ioc container
        /// </summary>
        static void SetupContainer()
        {
            _container = new WindsorContainer();
            //Allow windsor to resolve constructor that has an ICollection as parameter
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel));
        }

        static void RegisterComponents()
        {

            _container.Register(
                Castle.MicroKernel.Registration.Component
                .For<ITransactionLoaderService>()
                .ImplementedBy<StockTransactionXmlService>()
            );

            //_container.Register(
            //    Castle.MicroKernel.Registration.Component
            //    .For<ITickerService>()
            //    .ImplementedBy<StockTickerXmlService>()
            //);

            _container.Register(
            Castle.MicroKernel.Registration.Component
            .For<ITickerService>()
            .ImplementedBy<StockTickerAvapiService>()
        );

            _container.Register(
                Castle.MicroKernel.Registration.Component
                .For<ReportPage>()
                .ImplementedBy<AppSettingPage>()
            );

            _container.Register(
                Castle.MicroKernel.Registration.Component
                .For<ReportPage>()
                .ImplementedBy<ProfitLossTabPage>()
            );

            //register the ReportPageCollection that hold the Commands Instances
            //Command Instances will be injected into ReportPageCollection by IoC Container
            _container.Register(Castle.MicroKernel.Registration.Component.For<ReportPageCollection>());






        }
    }
}
