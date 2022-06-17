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
    public partial class MainForm : Form
    {
        protected AppContainer ParentTabs
        {
            get
            {
                return (ParentForm as AppContainer);
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParentTabs.AddNewTabToNewWindow(new MainForm());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ParentTabs.AddNewTab();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int myIdx = ParentTabs.SelectedTabIndex;
            ParentTabs.ReplaceTab(myIdx, new GreenScreen());
        }
    }
}
