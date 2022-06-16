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
            return CreateTab(new MainForm() { Text = "New Tab" }, this);
        }

        public TitleBarTab CreateTab(Form form, AppContainer parent)
        {
            return new TitleBarTab(parent)
            {
                Content = form
            };
        }

        private void AppContainer_Load(object sender, EventArgs e)
        {
            // Ensures that the Window is not in the maximized state
            WindowState = FormWindowState.Normal;
        }

        /// <summary>
        /// Opens the provided form in a new tab in this window
        /// </summary>
        public void AddNewTab(Form tabContent)
        {
            AddNewTab(tabContent, this);
        }

        /// <summary>
        /// Opens the provided form in a new tab in the provided window
        /// </summary>
        public void AddNewTab(Form tabContent, AppContainer newWindow)
        {
            TitleBarTab _tornTab = CreateTab(tabContent, newWindow);  // in leu of calling _tornTab.Parent = newWindow;

            Screen screen = Screen.AllScreens.First(s => s.WorkingArea.Contains(Cursor.Position));

            newWindow.StartPosition = FormStartPosition.Manual;

            if (this.WindowState == FormWindowState.Maximized)
            {
                newWindow.WindowState = this.WindowState;
                newWindow.Left = screen.WorkingArea.Left;
                newWindow.Top = screen.WorkingArea.Top;
            }
            else
            {
                newWindow.Width = this.Size.Width;
                newWindow.Height = this.Size.Height;
                newWindow.Left = this.Location.X + 40;
                newWindow.Top = this.Location.Y + 40;
            }

            this.ApplicationContext.OpenWindow(newWindow);

            newWindow.Show();
            newWindow.Tabs.Add(_tornTab);
            newWindow.SelectedTabIndex = 0;
            newWindow.ResizeTabContents();
        }
    }
}
