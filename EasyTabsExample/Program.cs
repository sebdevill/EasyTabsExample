using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyTabs;

namespace EasyTabsExample
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppContainer container = new AppContainer();

            // add initial tab
            container.Tabs.Add(
                new TitleBarTab(container) {
                    Content = new MainForm {
                        Text = "New Tab"
                    }
                }
            );

            // select initial tab
            container.SelectedTabIndex = 0;

            // create containing view and start application
            TitleBarTabsApplicationContext appContext = new TitleBarTabsApplicationContext();
            appContext.Start(container);
            Application.Run(appContext);
        }
    }
}
