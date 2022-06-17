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
using Microsoft.WindowsAPICodePack.Taskbar;

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
            this.Tabs.Add(CreateTab(tabContent, this));
            this.SelectedTabIndex = this.Tabs.Count - 1;
            this.ResizeTabContents();
        }

        /// <summary>
        /// Opens the provided form in a new tab in a new window
        /// </summary>
        public void AddNewTabToNewWindow(Form tabContent)
        {
            AppContainer newWindow = new AppContainer();
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

        public void ReplaceTab(int tabIdx, Form tabContent)
        {
            TitleBarTab tabToClose = this.Tabs[tabIdx];
            this.Tabs.Insert(tabIdx, CreateTab(tabContent, this));
            this.SelectedTabIndex = tabIdx; // This allows the new tab to render properly
            tabToClose.Content.Close(); // This is the best way to close a tab because it also removes it from the taskbar AeroPeek
        }
    }
}
