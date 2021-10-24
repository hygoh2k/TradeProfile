using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportTabControl
{
    public partial class ProfitLossControl : UserControl
    {
        public ProfitLossControl(DataTable dt)
        {
            InitializeComponent();

            this.dataGridView2.DataSource = dt;
        }
    }
}
