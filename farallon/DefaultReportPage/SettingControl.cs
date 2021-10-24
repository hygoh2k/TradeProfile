using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace DefaultReportPage
{
    public partial class SettingControl : UserControl
    {
        public SettingControl()
        {
            InitializeComponent();
            LoadSetting();
        }

        public void LoadSetting()
        {
            var workingDir = Environment.GetEnvironmentVariable("FARALLON");
            this.workingDirTb.Text = workingDir != null ? workingDir :
                AppDomain.CurrentDomain.BaseDirectory + "../../../working_dir";
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            Environment.SetEnvironmentVariable("FARALLON", this.workingDirTb.Text);
            var workingDir = Environment.GetEnvironmentVariable("FARALLON");
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            //this.folderBrowserDialog1.SelectedPath = @"../../working_dir";
            if ( this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = this.folderBrowserDialog1.SelectedPath;
                
            }
        }
    }
}
