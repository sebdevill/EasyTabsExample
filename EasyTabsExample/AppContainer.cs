using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyTabs;

namespace EasyTabsExample
{
    public partial class AppContainer : TitleBarTabs
    {
        public AppContainer()
        {
            InitializeComponent();

            AeroPeekEnabled = true;
            TabRenderer = new ChromeTabRenderer(this);

            // Ensures that the Window is sized correctly
            WindowState = FormWindowState.Normal;
        }

        public override TitleBarTab CreateTab()
        {
            return new TitleBarTab(this)
            {
                Content = new MainForm
                {
                    Text = "New Tab"
                }
            };
        }
        private void AppContainer_Load(object sender, EventArgs e)
        {
            // Ensures that the Window is not in the maximized state
            WindowState = FormWindowState.Normal;
        }
    }
}
