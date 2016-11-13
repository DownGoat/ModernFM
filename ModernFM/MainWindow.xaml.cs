using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;


namespace ModernFM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Context context;

        public MainWindow()
        {
            InitializeComponent();
            string homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                    Environment.OSVersion.Platform == PlatformID.MacOSX)
                    ? Environment.GetEnvironmentVariable("HOME")
                    : Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

            context = new Context(homePath);
            AddressBar.Text = context.Path;

            Browse();

            // Add douvle click event to listview.
            SetupListView();
        }

        private void SetupListView()
        {
            listView.Focus();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void test(object sender, RoutedEventArgs e)
        {
            context.ChangeContext(AddressBar.Text);

            
        }

        private void changeContext(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                context.ChangeContext(AddressBar.Text);
                Browse();
            }
        }

        private void Browse()
        {
            AddressBar.Text = context.Path;
            List<FSEntry> files = null;
            try
            {
                files = context.BrowseCurrentContext();
            } catch (Exception e)
            {
                textBlock.Text = e.Message;
                context.Back();
                AddressBar.Text = context.Path;
                Browse();
                return;
            }

            listView.Items.Clear();
            FSEntry dd = new FSEntry(true, context.Path);
            listView.Items.Add(dd);
            foreach (FSEntry f in files)
            {
                listView.Items.Add(f);
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = listView.SelectedItems.Count;
            int itemsCount = listView.Items.Count;
            long selection_size = 0;

            foreach (FSEntry f in listView.SelectedItems)
            {
                try
                {
                    selection_size += f.info.Length;
                } catch (Exception exc)
                {

                }
            }


            textBlock.Text = String.Format("{0} items.  {1} items selected. {2} size.", itemsCount, selected, selection_size);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (context.Back())
            {
                AddressBar.Text = context.Path;
                Browse();
            }
        }

        private void forwards_Click(object sender, RoutedEventArgs e)
        {
            if (context.Forward())
            {
                AddressBar.Text = context.Path;
                Browse();
            }  
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            var a = listView.SelectedItems;
        }

        private void listViewDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((FrameworkElement)e.OriginalSource).DataContext as FSEntry;
            if (item != null)
            {
                if (!item.IsDir() && !item.DoubleDot)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(item.path);
                    } catch (Exception exc)
                    {
                        textBlock.Text = exc.Message;
                    }
                }

                else
                {
                    if (item.DoubleDot)
                    {
                        context.Parent();
                    }

                    else
                    {
                        context.ChangeContext(context.Path, item.Name);
                    }
                    Browse();
                }
            }
        }

        public void listViewSelectEvent()
        {
            bool dir = false;
            bool file = false;
            foreach (FSEntry f in listView.SelectedItems)
            {
                if (f.IsDir() || f.DoubleDot)
                {
                    dir = true;
                }

                else
                {
                    file = true;
                }
            }

            // Both files and dirs are slected, do nothing.
            if (dir && file)
            {
                return;
            }

            if (file)
            {
                foreach (FSEntry f in listView.SelectedItems)
                {
                    System.Diagnostics.Process.Start(f.path);
                }
            }

            if (dir)
            {
                if (listView.SelectedItems.Count == 1)
                {
                    var item = (FSEntry) listView.SelectedItem;
                    if (item.DoubleDot)
                    {
                        context.Parent();
                    }
                    else
                    {
                        context.ChangeContext(context.Path, item.Name);
                    }
                    Browse();
                }
            }
        }

        public void Parent()
        {

        }

        private void listView_KeyUp(object sender, KeyEventArgs e)
        {
            if (Key.Enter == e.Key)
            {
                listViewSelectEvent();
            }
        }

        private void SelectAddressBarExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            AddressBar.Focus();
            AddressBar.SelectAll();
        }

        private void BackExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (context.Back())
            {
                AddressBar.Text = context.Path;
                Browse();
            }
        }

        private void ForwardExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (context.Forward())
            {
                AddressBar.Text = context.Path;
                Browse();
            }
        }
    }
}
